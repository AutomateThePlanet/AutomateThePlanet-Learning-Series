using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Node
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("lhId")]
        public string LhId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("selector")]
        public string Selector { get; set; }

        [JsonProperty("boundingRect")]
        public BoundingRect BoundingRect { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        [JsonProperty("nodeLabel")]
        public string NodeLabel { get; set; }
    }
}