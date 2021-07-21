using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Screenshot
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }
    }
}