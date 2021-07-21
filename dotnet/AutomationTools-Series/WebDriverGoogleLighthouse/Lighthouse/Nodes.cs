using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Nodes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resourceBytes")]
        public double ResourceBytes { get; set; }

        [JsonProperty("unusedBytes")]
        public double? UnusedBytes { get; set; }
    }
}