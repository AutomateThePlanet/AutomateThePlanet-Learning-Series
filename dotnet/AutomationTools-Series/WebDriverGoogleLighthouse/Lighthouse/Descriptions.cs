using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Descriptions
    {
        [JsonProperty("unused-css-rules")]
        public string UnusedCssRules { get; set; }

        [JsonProperty("modern-image-formats")]
        public string ModernImageFormats { get; set; }

        [JsonProperty("offscreen-images")]
        public string OffscreenImages { get; set; }

        [JsonProperty("total-byte-weight")]
        public string TotalByteWeight { get; set; }

        [JsonProperty("render-blocking-resources")]
        public string RenderBlockingResources { get; set; }

        [JsonProperty("unminified-css")]
        public string UnminifiedCss { get; set; }

        [JsonProperty("unminified-javascript")]
        public string UnminifiedJavascript { get; set; }

        [JsonProperty("efficient-animated-content")]
        public string EfficientAnimatedContent { get; set; }

        [JsonProperty("unused-javascript")]
        public string UnusedJavascript { get; set; }

        [JsonProperty("uses-long-cache-ttl")]
        public string UsesLongCacheTtl { get; set; }

        [JsonProperty("uses-optimized-images")]
        public string UsesOptimizedImages { get; set; }

        [JsonProperty("uses-text-compression")]
        public string UsesTextCompression { get; set; }

        [JsonProperty("uses-responsive-images")]
        public string UsesResponsiveImages { get; set; }

        [JsonProperty("server-response-time")]
        public string ServerResponseTime { get; set; }
    }
}