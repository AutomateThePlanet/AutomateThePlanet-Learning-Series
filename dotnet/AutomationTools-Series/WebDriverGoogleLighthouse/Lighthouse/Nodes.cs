using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Nodes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resourceBytes")]
        public int ResourceBytes { get; set; }

        [JsonProperty("unusedBytes")]
        public int? UnusedBytes { get; set; }
    }
}