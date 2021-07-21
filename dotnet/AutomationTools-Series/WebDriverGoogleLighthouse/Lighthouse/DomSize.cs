using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class DomSize
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("scoreDisplayMode")]
        public string ScoreDisplayMode { get; set; }

        [JsonProperty("numericValue")]
        public double NumericValue { get; set; }

        [JsonProperty("numericUnit")]
        public string NumericUnit { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }
    }
}