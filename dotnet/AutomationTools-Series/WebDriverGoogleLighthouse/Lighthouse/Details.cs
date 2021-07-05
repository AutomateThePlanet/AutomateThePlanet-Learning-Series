using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Details
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("scale")]
        public int Scale { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("timing")]
        public int Timing { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("headings")]
        public List<Heading> Headings { get; set; }

        [JsonProperty("overallSavingsMs")]
        public double OverallSavingsMs { get; set; }

        [JsonProperty("longestChain")]
        public LongestChain LongestChain { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("screenshot")]
        public Screenshot Screenshot { get; set; }

        [JsonProperty("nodes")]
        public Nodes Nodes { get; set; }

        [JsonProperty("overallSavingsBytes")]
        public int OverallSavingsBytes { get; set; }
    }
}