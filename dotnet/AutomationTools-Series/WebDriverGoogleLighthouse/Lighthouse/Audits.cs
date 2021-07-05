using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebDriverGoogleLighthouse
{
    public class Audits
    {
        [JsonProperty("first-contentful-paint")]
        public FirstContentfulPaint FirstContentfulPaint { get; set; }

        [JsonProperty("largest-contentful-paint")]
        public LargestContentfulPaint LargestContentfulPaint { get; set; }

        [JsonProperty("first-meaningful-paint")]
        public FirstMeaningfulPaint FirstMeaningfulPaint { get; set; }

        [JsonProperty("speed-index")]
        public SpeedIndex SpeedIndex { get; set; }

        [JsonProperty("screenshot-thumbnails")]
        public ScreenshotThumbnails ScreenshotThumbnails { get; set; }

        [JsonProperty("final-screenshot")]
        public FinalScreenshot FinalScreenshot { get; set; }

        [JsonProperty("total-blocking-time")]
        public TotalBlockingTime TotalBlockingTime { get; set; }

        [JsonProperty("max-potential-fid")]
        public MaxPotentialFid MaxPotentialFid { get; set; }

        [JsonProperty("cumulative-layout-shift")]
        public CumulativeLayoutShift CumulativeLayoutShift { get; set; }

        [JsonProperty("server-response-time")]
        public ServerResponseTime ServerResponseTime { get; set; }

        [JsonProperty("interactive")]
        public Interactive Interactive { get; set; }

        [JsonProperty("user-timings")]
        public UserTimings UserTimings { get; set; }

        [JsonProperty("critical-request-chains")]
        public CriticalRequestChains CriticalRequestChains { get; set; }

        [JsonProperty("redirects")]
        public Redirects Redirects { get; set; }

        [JsonProperty("mainthread-work-breakdown")]
        public MainthreadWorkBreakdown MainthreadWorkBreakdown { get; set; }

        [JsonProperty("bootup-time")]
        public BootupTime BootupTime { get; set; }

        [JsonProperty("uses-rel-preload")]
        public UsesRelPreload UsesRelPreload { get; set; }

        [JsonProperty("uses-rel-preconnect")]
        public UsesRelPreconnect UsesRelPreconnect { get; set; }

        [JsonProperty("font-display")]
        public FontDisplay FontDisplay { get; set; }

        [JsonProperty("diagnostics")]
        public Diagnostics Diagnostics { get; set; }

        [JsonProperty("network-requests")]
        public NetworkRequests NetworkRequests { get; set; }

        [JsonProperty("network-rtt")]
        public NetworkRtt NetworkRtt { get; set; }

        [JsonProperty("network-server-latency")]
        public NetworkServerLatency NetworkServerLatency { get; set; }

        [JsonProperty("main-thread-tasks")]
        public MainThreadTasks MainThreadTasks { get; set; }

        [JsonProperty("metrics")]
        public Metrics Metrics { get; set; }

        [JsonProperty("performance-budget")]
        public PerformanceBudget PerformanceBudget { get; set; }

        [JsonProperty("timing-budget")]
        public TimingBudget TimingBudget { get; set; }

        [JsonProperty("resource-summary")]
        public ResourceSummary ResourceSummary { get; set; }

        [JsonProperty("third-party-summary")]
        public ThirdPartySummary ThirdPartySummary { get; set; }

        [JsonProperty("third-party-facades")]
        public ThirdPartyFacades ThirdPartyFacades { get; set; }

        [JsonProperty("largest-contentful-paint-element")]
        public LargestContentfulPaintElement LargestContentfulPaintElement { get; set; }

        [JsonProperty("layout-shift-elements")]
        public LayoutShiftElements LayoutShiftElements { get; set; }

        [JsonProperty("long-tasks")]
        public LongTasks LongTasks { get; set; }

        [JsonProperty("non-composited-animations")]
        public NonCompositedAnimations NonCompositedAnimations { get; set; }

        [JsonProperty("unsized-images")]
        public UnsizedImages UnsizedImages { get; set; }

        [JsonProperty("preload-lcp-image")]
        public PreloadLcpImage PreloadLcpImage { get; set; }

        [JsonProperty("full-page-screenshot")]
        public FullPageScreenshot FullPageScreenshot { get; set; }

        [JsonProperty("script-treemap-data")]
        public ScriptTreemapData ScriptTreemapData { get; set; }

        [JsonProperty("uses-long-cache-ttl")]
        public UsesLongCacheTtl UsesLongCacheTtl { get; set; }

        [JsonProperty("total-byte-weight")]
        public TotalByteWeight TotalByteWeight { get; set; }

        [JsonProperty("offscreen-images")]
        public OffscreenImages OffscreenImages { get; set; }

        [JsonProperty("render-blocking-resources")]
        public RenderBlockingResources RenderBlockingResources { get; set; }

        [JsonProperty("unminified-css")]
        public UnminifiedCss UnminifiedCss { get; set; }

        [JsonProperty("unminified-javascript")]
        public UnminifiedJavascript UnminifiedJavascript { get; set; }

        [JsonProperty("unused-css-rules")]
        public UnusedCssRules UnusedCssRules { get; set; }

        [JsonProperty("unused-javascript")]
        public UnusedJavascript UnusedJavascript { get; set; }

        [JsonProperty("modern-image-formats")]
        public ModernImageFormats ModernImageFormats { get; set; }

        [JsonProperty("uses-optimized-images")]
        public UsesOptimizedImages UsesOptimizedImages { get; set; }

        [JsonProperty("uses-text-compression")]
        public UsesTextCompression UsesTextCompression { get; set; }

        [JsonProperty("uses-responsive-images")]
        public UsesResponsiveImages UsesResponsiveImages { get; set; }

        [JsonProperty("efficient-animated-content")]
        public EfficientAnimatedContent EfficientAnimatedContent { get; set; }

        [JsonProperty("duplicated-javascript")]
        public DuplicatedJavascript DuplicatedJavascript { get; set; }

        [JsonProperty("legacy-javascript")]
        public LegacyJavascript LegacyJavascript { get; set; }

        [JsonProperty("dom-size")]
        public DomSize DomSize { get; set; }

        [JsonProperty("no-document-write")]
        public NoDocumentWrite NoDocumentWrite { get; set; }

        [JsonProperty("uses-http2")]
        public UsesHttp2 UsesHttp2 { get; set; }

        [JsonProperty("uses-passive-event-listeners")]
        public UsesPassiveEventListeners UsesPassiveEventListeners { get; set; }
    }
}