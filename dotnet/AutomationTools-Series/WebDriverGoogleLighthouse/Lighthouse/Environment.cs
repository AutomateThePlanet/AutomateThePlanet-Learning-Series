using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Environment
    {
        [JsonProperty("networkUserAgent")]
        public string NetworkUserAgent { get; set; }

        [JsonProperty("hostUserAgent")]
        public string HostUserAgent { get; set; }

        [JsonProperty("benchmarkIndex")]
        public double BenchmarkIndex { get; set; }

        [JsonProperty("credits")]
        public Credits Credits { get; set; }
    }
}