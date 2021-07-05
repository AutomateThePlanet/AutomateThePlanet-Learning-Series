using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Entity
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}