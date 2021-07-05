using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Values
    {
        [JsonProperty("timeInMs")]
        public double TimeInMs { get; set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; set; }

        [JsonProperty("requestCount")]
        public int RequestCount { get; set; }

        [JsonProperty("byteCount")]
        public int ByteCount { get; set; }

        [JsonProperty("nodeCount")]
        public int NodeCount { get; set; }

        [JsonProperty("totalBytes")]
        public int TotalBytes { get; set; }

        [JsonProperty("wastedMs")]
        public int WastedMs { get; set; }

        [JsonProperty("wastedBytes")]
        public int WastedBytes { get; set; }
    }
}