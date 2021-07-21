using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Throttling
    {
        [JsonProperty("rttMs")]
        public double RttMs { get; set; }

        [JsonProperty("throughputKbps")]
        public double ThroughputKbps { get; set; }

        [JsonProperty("requestLatencyMs")]
        public double RequestLatencyMs { get; set; }

        [JsonProperty("downloadThroughputKbps")]
        public double DownloadThroughputKbps { get; set; }

        [JsonProperty("uploadThroughputKbps")]
        public double UploadThroughputKbps { get; set; }

        [JsonProperty("cpuSlowdownMultiplier")]
        public double CpuSlowdownMultiplier { get; set; }
    }
}