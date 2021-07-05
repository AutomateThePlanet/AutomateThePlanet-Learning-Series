using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class LongestChain
    {
        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("transferSize")]
        public int TransferSize { get; set; }
    }
}