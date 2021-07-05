using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class DebugData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("max-age")]
        public int MaxAge { get; set; }

        [JsonProperty("s-maxage")]
        public string SMaxage { get; set; }
    }
}