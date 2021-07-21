using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class ScreenEmulation
    {
        [JsonProperty("mobile")]
        public bool Mobile { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("deviceScaleFactor")]
        public double DeviceScaleFactor { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}