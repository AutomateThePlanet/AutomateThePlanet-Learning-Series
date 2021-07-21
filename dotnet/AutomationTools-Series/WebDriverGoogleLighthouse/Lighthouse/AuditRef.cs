using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class AuditRef
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("acronym")]
        public string Acronym { get; set; }

        [JsonProperty("relevantAudits")]
        public List<string> RelevantAudits { get; set; }
    }
}