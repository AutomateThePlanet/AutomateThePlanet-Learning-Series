using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Screenshot
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}