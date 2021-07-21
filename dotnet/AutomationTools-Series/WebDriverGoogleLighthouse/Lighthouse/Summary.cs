using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Summary
    {
        [JsonProperty("wastedMs")]
        public double WastedMs { get; set; }

        [JsonProperty("wastedBytes")]
        public double WastedBytes { get; set; }
    }
}