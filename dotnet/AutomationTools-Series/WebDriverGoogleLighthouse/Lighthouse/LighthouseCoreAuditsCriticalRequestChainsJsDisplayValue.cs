using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class LighthouseCoreAuditsCriticalRequestChainsJsDisplayValue
    {
        [JsonProperty("values")]
        public Values Values { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}