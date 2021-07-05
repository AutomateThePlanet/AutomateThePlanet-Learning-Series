using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class SubItems
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}