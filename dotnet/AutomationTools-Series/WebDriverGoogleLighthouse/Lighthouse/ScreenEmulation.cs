using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class ScreenEmulation
    {
        [JsonProperty("mobile")]
        public bool Mobile { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("deviceScaleFactor")]
        public int DeviceScaleFactor { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}