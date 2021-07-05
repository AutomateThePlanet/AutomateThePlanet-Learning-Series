using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class StackPack
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("iconDataURL")]
        public string IconDataURL { get; set; }

        [JsonProperty("descriptions")]
        public Descriptions Descriptions { get; set; }
    }
}