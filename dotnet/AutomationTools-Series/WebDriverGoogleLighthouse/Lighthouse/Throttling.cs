using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Throttling
    {
        [JsonProperty("rttMs")]
        public int RttMs { get; set; }

        [JsonProperty("throughputKbps")]
        public int ThroughputKbps { get; set; }

        [JsonProperty("requestLatencyMs")]
        public int RequestLatencyMs { get; set; }

        [JsonProperty("downloadThroughputKbps")]
        public int DownloadThroughputKbps { get; set; }

        [JsonProperty("uploadThroughputKbps")]
        public int UploadThroughputKbps { get; set; }

        [JsonProperty("cpuSlowdownMultiplier")]
        public int CpuSlowdownMultiplier { get; set; }
    }
}