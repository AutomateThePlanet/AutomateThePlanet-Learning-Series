using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class CategoryGroups
    {
        [JsonProperty("metrics")]
        public Metrics Metrics { get; set; }

        [JsonProperty("load-opportunities")]
        public LoadOpportunities LoadOpportunities { get; set; }

        [JsonProperty("budgets")]
        public Budgets Budgets { get; set; }

        [JsonProperty("diagnostics")]
        public Diagnostics Diagnostics { get; set; }

        [JsonProperty("pwa-installable")]
        public PwaInstallable PwaInstallable { get; set; }

        [JsonProperty("pwa-optimized")]
        public PwaOptimized PwaOptimized { get; set; }

        [JsonProperty("seo-mobile")]
        public SeoMobile SeoMobile { get; set; }

        [JsonProperty("seo-content")]
        public SeoContent SeoContent { get; set; }

        [JsonProperty("seo-crawl")]
        public SeoCrawl SeoCrawl { get; set; }

        [JsonProperty("best-practices-trust-safety")]
        public BestPracticesTrustSafety BestPracticesTrustSafety { get; set; }

        [JsonProperty("best-practices-ux")]
        public BestPracticesUx BestPracticesUx { get; set; }

        [JsonProperty("best-practices-browser-compat")]
        public BestPracticesBrowserCompat BestPracticesBrowserCompat { get; set; }

        [JsonProperty("best-practices-general")]
        public BestPracticesGeneral BestPracticesGeneral { get; set; }
    }
}