using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Request
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("startTime")]
        public double StartTime { get; set; }

        [JsonProperty("endTime")]
        public double EndTime { get; set; }

        [JsonProperty("responseReceivedTime")]
        public double ResponseReceivedTime { get; set; }

        [JsonProperty("transferSize")]
        public double TransferSize { get; set; }
    }
}