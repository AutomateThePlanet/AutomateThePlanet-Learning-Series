using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Values
    {
        [JsonProperty("timeInMs")]
        public double TimeInMs { get; set; }

        [JsonProperty("itemCount")]
        public double ItemCount { get; set; }

        [JsonProperty("requestCount")]
        public double RequestCount { get; set; }

        [JsonProperty("byteCount")]
        public double ByteCount { get; set; }

        [JsonProperty("nodeCount")]
        public double NodeCount { get; set; }

        [JsonProperty("totalBytes")]
        public double TotalBytes { get; set; }

        [JsonProperty("wastedMs")]
        public double WastedMs { get; set; }

        [JsonProperty("wastedBytes")]
        public double WastedBytes { get; set; }
    }
}