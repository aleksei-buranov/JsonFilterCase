using Newtonsoft.Json;

namespace JsonFilterCase.Models
{
    internal class Record
    {
        [JsonProperty("cmd")] public int Cmd { get; set; }

        [JsonProperty("hdr1")] public int Hdr1 { get; set; }

        [JsonProperty("hdr2")] public int Hdr2 { get; set; }

        [JsonProperty("hdr3")] public int Hdr3 { get; set; }

        [JsonProperty("hdr4")] public int Hdr4 { get; set; }

        [JsonProperty("flags")] public int Flags { get; set; }

        [JsonProperty("steps")] public int Steps { get; set; }

        [JsonProperty("max_hr")] public int MaxHr { get; set; }

        [JsonProperty("min_hr")] public int MinHr { get; set; }

        [JsonProperty("sensor_id")] public string SensorId { get; set; }

        [JsonProperty("rec_id")] public int RecId { get; set; }

        [JsonProperty("add_date")] public string AddDate { get; set; }

        [JsonProperty("distance")] public int Distance { get; set; }

        [JsonProperty("energy_in")] public int EnergyIn { get; set; }

        [JsonProperty("precision")] public int Precision { get; set; }

        [JsonProperty("reserved1")] public int Reserved1 { get; set; }

        [JsonProperty("reserved2")] public int Reserved2 { get; set; }

        [JsonProperty("reserved3")] public int Reserved3 { get; set; }

        [JsonProperty("timestamp")] public int Timestamp { get; set; }

        [JsonProperty("energy_out")] public int EnergyOut { get; set; }

        [JsonProperty("heart_rate")] public int HeartRate { get; set; }

        [JsonProperty("utc_offset")] public int UtcOffset { get; set; }

        [JsonProperty("first_rec_id")] public int FirstRecId { get; set; }

        [JsonProperty("stress_level")] public int StressLevel { get; set; }

        [JsonProperty("battery_level")] public int BatteryLevel { get; set; }

        [JsonProperty("activity_water_loss")] public double ActivityWaterLoss { get; set; }

        [JsonProperty("metabolic_water_loss")] public double MetabolicWaterLoss { get; set; }
    }
}
