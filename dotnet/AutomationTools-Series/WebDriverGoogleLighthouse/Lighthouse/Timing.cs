using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Timing
    {
        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }
}