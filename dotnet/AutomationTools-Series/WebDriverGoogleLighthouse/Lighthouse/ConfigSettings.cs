using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class ConfigSettings
    {
        [JsonProperty("output")]
        public List<string> Output { get; set; }

        [JsonProperty("maxWaitForFcp")]
        public int MaxWaitForFcp { get; set; }

        [JsonProperty("maxWaitForLoad")]
        public int MaxWaitForLoad { get; set; }

        [JsonProperty("formFactor")]
        public string FormFactor { get; set; }

        [JsonProperty("throttling")]
        public Throttling Throttling { get; set; }

        [JsonProperty("throttlingMethod")]
        public string ThrottlingMethod { get; set; }

        [JsonProperty("screenEmulation")]
        public ScreenEmulation ScreenEmulation { get; set; }

        [JsonProperty("emulatedUserAgent")]
        public bool EmulatedUserAgent { get; set; }

        [JsonProperty("auditMode")]
        public bool AuditMode { get; set; }

        [JsonProperty("gatherMode")]
        public bool GatherMode { get; set; }

        [JsonProperty("disableStorageReset")]
        public bool DisableStorageReset { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("budgets")]
        public object Budgets { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("blockedUrlPatterns")]
        public object BlockedUrlPatterns { get; set; }

        [JsonProperty("additionalTraceCategories")]
        public object AdditionalTraceCategories { get; set; }

        [JsonProperty("extraHeaders")]
        public object ExtraHeaders { get; set; }

        [JsonProperty("precomputedLanternData")]
        public object PrecomputedLanternData { get; set; }

        [JsonProperty("onlyAudits")]
        public object OnlyAudits { get; set; }

        [JsonProperty("onlyCategories")]
        public List<string> OnlyCategories { get; set; }

        [JsonProperty("skipAudits")]
        public object SkipAudits { get; set; }
    }
}