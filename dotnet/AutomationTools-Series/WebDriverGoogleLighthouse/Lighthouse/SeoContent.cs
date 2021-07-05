using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class SeoContent
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}