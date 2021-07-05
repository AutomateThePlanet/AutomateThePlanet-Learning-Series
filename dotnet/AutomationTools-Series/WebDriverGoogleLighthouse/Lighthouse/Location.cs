using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Location
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("line")]
        public int Line { get; set; }

        [JsonProperty("column")]
        public int Column { get; set; }

        [JsonProperty("urlProvider")]
        public string UrlProvider { get; set; }
    }
}