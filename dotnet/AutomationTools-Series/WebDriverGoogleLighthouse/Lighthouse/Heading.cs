using Newtonsoft.Json;

namespace WebDriverGoogleLighthouse
{
    public class Heading
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("valueType")]
        public string ValueType { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("granularity")]
        public double? Granularity { get; set; }

        [JsonProperty("displayUnit")]
        public string DisplayUnit { get; set; }

        [JsonProperty("subItemsHeading")]
        public SubItemsHeading SubItemsHeading { get; set; }
    }
}