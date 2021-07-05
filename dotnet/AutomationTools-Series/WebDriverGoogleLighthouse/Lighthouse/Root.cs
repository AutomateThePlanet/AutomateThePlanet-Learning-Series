using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Root
    {
        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }

        [JsonProperty("environment")]
        public Environment Environment { get; set; }

        [JsonProperty("lighthouseVersion")]
        public string LighthouseVersion { get; set; }

        [JsonProperty("fetchTime")]
        public DateTime FetchTime { get; set; }

        [JsonProperty("requestedUrl")]
        public string RequestedUrl { get; set; }

        [JsonProperty("finalUrl")]
        public string FinalUrl { get; set; }

        [JsonProperty("runWarnings")]
        public List<object> RunWarnings { get; set; }

        [JsonProperty("audits")]
        public Audits Audits { get; set; }
    }
}