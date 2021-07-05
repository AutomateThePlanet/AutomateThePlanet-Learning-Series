using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class SeoCrawl
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}