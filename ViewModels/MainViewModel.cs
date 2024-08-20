using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JsonFilterCase.Models;
using JsonFilterCase.Properties;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonFilterCase.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILogger _logger;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(ProcessFileCommand))]
        private string _selectedFileName;

        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger;
        }

        public bool SelectedFileExists => !string.IsNullOrEmpty(SelectedFileName) && File.Exists(SelectedFileName);

        [RelayCommand]
        private void SelectFile()
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                CheckFileExists = true,
                Filter = "JSON-файлы (*.json)|*.json",
                Title = "Выберите файл формата JSON для очистки от дубликатов"
            };

            var result = fileDialog.ShowDialog();
            if (!result.GetValueOrDefault())
                return;
            SelectedFileName = fileDialog.FileName;
        }

        [RelayCommand(CanExecute = nameof(SelectedFileExists))]
        private void ProcessFile()
        {
            _logger.LogInformation($"Запущена обработка файла {SelectedFileName} - {DateTime.Now}");
            try
            {
                var jsonData = File.ReadAllText(SelectedFileName);
                var records = JsonConvert.DeserializeObject<List<Record>>(jsonData);
                var groupings = records
                    .GroupBy(x => new { x.RecId, x.Timestamp });

                var uniqueItems = new List<Record>();
                var duplicatesCount = 0;

                foreach (var grouping in groupings)
                {
                    if (grouping.Count() > 1)
                        foreach (var record in grouping.Skip(1))
                        {
                            _logger.LogInformation(
                                $"Удаление записи-дубликата RecordId:{record.RecId}, Timestamp = {record.Timestamp}");
                            duplicatesCount++;
                        }

                    uniqueItems.Add(grouping.First());
                }

                var newJsonData = JsonConvert.SerializeObject(uniqueItems, Formatting.Indented);
                File.WriteAllText(SelectedFileName, newJsonData);
                _logger.LogInformation($"Удалено дубликатов: {duplicatesCount}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при обработке");
            }

            _logger.LogInformation($"Закончена обработка файла {SelectedFileName} - {DateTime.Now}");
            MessageBox.Show("Закончена обработка файла", "Обработка файла");
        }

        [RelayCommand]
        private void OpenLogFile()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), Settings.Default.LogPath)),
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"Не удалось открыть файл: {e.Message}", "Ошибка открытия файла");
            }
        }
    }
}
