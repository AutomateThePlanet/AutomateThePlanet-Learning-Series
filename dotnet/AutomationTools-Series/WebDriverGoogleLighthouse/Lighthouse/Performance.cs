using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Performance
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("auditRefs")]
        public List<AuditRef> AuditRefs { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }
}