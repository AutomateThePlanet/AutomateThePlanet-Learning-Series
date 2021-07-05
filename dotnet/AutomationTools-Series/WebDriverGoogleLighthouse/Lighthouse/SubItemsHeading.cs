using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class SubItemsHeading
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        [JsonProperty("valueType")]
        public string ValueType { get; set; }
    }
}