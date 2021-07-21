using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Item
    {
        [JsonProperty("timing")]
        public double Timing { get; set; }

        [JsonProperty("timestamp")]
        public object Timestamp { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("cumulativeLayoutShiftMainFrame")]
        public double CumulativeLayoutShiftMainFrame { get; set; }

        [JsonProperty("totalCumulativeLayoutShift")]
        public double TotalCumulativeLayoutShift { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("responseTime")]
        public double ResponseTime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startTime")]
        public double StartTime { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("timingType")]
        public string TimingType { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("groupLabel")]
        public string GroupLabel { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("scripting")]
        public double Scripting { get; set; }

        [JsonProperty("scriptParseCompile")]
        public double ScriptParseCompile { get; set; }

        [JsonProperty("wastedMs")]
        public double WastedMs { get; set; }

        [JsonProperty("numRequests")]
        public double NumRequests { get; set; }

        [JsonProperty("numScripts")]
        public double NumScripts { get; set; }

        [JsonProperty("numStylesheets")]
        public double NumStylesheets { get; set; }

        [JsonProperty("numFonts")]
        public double NumFonts { get; set; }

        [JsonProperty("numTasks")]
        public double NumTasks { get; set; }

        [JsonProperty("numTasksOver10ms")]
        public double NumTasksOver10ms { get; set; }

        [JsonProperty("numTasksOver25ms")]
        public double NumTasksOver25ms { get; set; }

        [JsonProperty("numTasksOver50ms")]
        public double NumTasksOver50ms { get; set; }

        [JsonProperty("numTasksOver100ms")]
        public double NumTasksOver100ms { get; set; }

        [JsonProperty("numTasksOver500ms")]
        public double NumTasksOver500ms { get; set; }

        [JsonProperty("rtt")]
        public double Rtt { get; set; }

        [JsonProperty("throughput")]
        public double Throughput { get; set; }

        [JsonProperty("maxRtt")]
        public double MaxRtt { get; set; }

        [JsonProperty("maxServerLatency")]
        public double MaxServerLatency { get; set; }

        [JsonProperty("totalByteWeight")]
        public double TotalByteWeight { get; set; }

        [JsonProperty("totalTaskTime")]
        public double TotalTaskTime { get; set; }

        [JsonProperty("mainDocumentTransferSize")]
        public double MainDocumentTransferSize { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("endTime")]
        public double EndTime { get; set; }

        [JsonProperty("finished")]
        public bool Finished { get; set; }

        [JsonProperty("transferSize")]
        public double TransferSize { get; set; }

        [JsonProperty("resourceSize")]
        public double ResourceSize { get; set; }

        [JsonProperty("statusCode")]
        public double StatusCode { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("serverResponseTime")]
        public double ServerResponseTime { get; set; }

        [JsonProperty("firstContentfulPaint")]
        public double FirstContentfulPaint { get; set; }

        [JsonProperty("firstContentfulPaintTs")]
        public long FirstContentfulPaintTs { get; set; }

        [JsonProperty("firstContentfulPaintAllFrames")]
        public double FirstContentfulPaintAllFrames { get; set; }

        [JsonProperty("firstContentfulPaintAllFramesTs")]
        public long FirstContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("firstMeaningfulPaint")]
        public double FirstMeaningfulPaint { get; set; }

        [JsonProperty("firstMeaningfulPaintTs")]
        public long FirstMeaningfulPaintTs { get; set; }

        [JsonProperty("largestContentfulPaint")]
        public double LargestContentfulPaint { get; set; }

        [JsonProperty("largestContentfulPaintTs")]
        public long LargestContentfulPaintTs { get; set; }

        [JsonProperty("largestContentfulPaintAllFrames")]
        public double LargestContentfulPaintAllFrames { get; set; }

        [JsonProperty("largestContentfulPaintAllFramesTs")]
        public long LargestContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("interactive")]
        public double Interactive { get; set; }

        [JsonProperty("interactiveTs")]
        public long InteractiveTs { get; set; }

        [JsonProperty("speedIndex")]
        public double SpeedIndex { get; set; }

        [JsonProperty("speedIndexTs")]
        public long SpeedIndexTs { get; set; }

        [JsonProperty("totalBlockingTime")]
        public double TotalBlockingTime { get; set; }

        [JsonProperty("maxPotentialFID")]
        public double MaxPotentialFID { get; set; }

        [JsonProperty("cumulativeLayoutShift")]
        public double CumulativeLayoutShift { get; set; }

        [JsonProperty("observedTimeOrigin")]
        public double ObservedTimeOrigin { get; set; }

        [JsonProperty("observedTimeOriginTs")]
        public long ObservedTimeOriginTs { get; set; }

        [JsonProperty("observedNavigationStart")]
        public double ObservedNavigationStart { get; set; }

        [JsonProperty("observedNavigationStartTs")]
        public long ObservedNavigationStartTs { get; set; }

        [JsonProperty("observedFirstPaint")]
        public double ObservedFirstPaint { get; set; }

        [JsonProperty("observedFirstPaintTs")]
        public long ObservedFirstPaintTs { get; set; }

        [JsonProperty("observedFirstContentfulPaint")]
        public double ObservedFirstContentfulPaint { get; set; }

        [JsonProperty("observedFirstContentfulPaintTs")]
        public long ObservedFirstContentfulPaintTs { get; set; }

        [JsonProperty("observedFirstContentfulPaintAllFrames")]
        public double ObservedFirstContentfulPaintAllFrames { get; set; }

        [JsonProperty("observedFirstContentfulPaintAllFramesTs")]
        public long ObservedFirstContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("observedFirstMeaningfulPaint")]
        public double ObservedFirstMeaningfulPaint { get; set; }

        [JsonProperty("observedFirstMeaningfulPaintTs")]
        public long ObservedFirstMeaningfulPaintTs { get; set; }

        [JsonProperty("observedLargestContentfulPaint")]
        public double ObservedLargestContentfulPaint { get; set; }

        [JsonProperty("observedLargestContentfulPaintTs")]
        public long ObservedLargestContentfulPaintTs { get; set; }

        [JsonProperty("observedLargestContentfulPaintAllFrames")]
        public double ObservedLargestContentfulPaintAllFrames { get; set; }

        [JsonProperty("observedLargestContentfulPaintAllFramesTs")]
        public long ObservedLargestContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("observedTraceEnd")]
        public double ObservedTraceEnd { get; set; }

        [JsonProperty("observedTraceEndTs")]
        public long ObservedTraceEndTs { get; set; }

        [JsonProperty("observedLoad")]
        public double ObservedLoad { get; set; }

        [JsonProperty("observedLoadTs")]
        public long ObservedLoadTs { get; set; }

        [JsonProperty("observedDomContentLoaded")]
        public double ObservedDomContentLoaded { get; set; }

        [JsonProperty("observedDomContentLoadedTs")]
        public long ObservedDomContentLoadedTs { get; set; }

        [JsonProperty("observedCumulativeLayoutShift")]
        public double ObservedCumulativeLayoutShift { get; set; }

        [JsonProperty("observedCumulativeLayoutShiftMainFrame")]
        public double ObservedCumulativeLayoutShiftMainFrame { get; set; }

        [JsonProperty("observedTotalCumulativeLayoutShift")]
        public double ObservedTotalCumulativeLayoutShift { get; set; }

        [JsonProperty("observedFirstVisualChange")]
        public double ObservedFirstVisualChange { get; set; }

        [JsonProperty("observedFirstVisualChangeTs")]
        public long ObservedFirstVisualChangeTs { get; set; }

        [JsonProperty("observedLastVisualChange")]
        public double ObservedLastVisualChange { get; set; }

        [JsonProperty("observedLastVisualChangeTs")]
        public long ObservedLastVisualChangeTs { get; set; }

        [JsonProperty("observedSpeedIndex")]
        public double ObservedSpeedIndex { get; set; }

        [JsonProperty("observedSpeedIndexTs")]
        public long ObservedSpeedIndexTs { get; set; }

        [JsonProperty("lcpInvalidated")]
        public bool? LcpInvalidated { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("requestCount")]
        public double RequestCount { get; set; }

        [JsonProperty("mainThreadTime")]
        public double MainThreadTime { get; set; }

        [JsonProperty("blockingTime")]
        public double BlockingTime { get; set; }

        [JsonProperty("entity")]
        public Entity Entity { get; set; }

        [JsonProperty("subItems")]
        public SubItems SubItems { get; set; }

        [JsonProperty("node")]
        public Node Node { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("debugData")]
        public DebugData DebugData { get; set; }

        [JsonProperty("cacheLifetimeMs")]
        public double CacheLifetimeMs { get; set; }

        [JsonProperty("cacheHitProbability")]
        public double CacheHitProbability { get; set; }

        [JsonProperty("totalBytes")]
        public double TotalBytes { get; set; }

        [JsonProperty("wastedBytes")]
        public double WastedBytes { get; set; }

        [JsonProperty("wastedPercent")]
        public double WastedPercent { get; set; }

        [JsonProperty("fromProtocol")]
        public bool FromProtocol { get; set; }

        [JsonProperty("isCrossOrigin")]
        public bool IsCrossOrigin { get; set; }

        [JsonProperty("signal")]
        public string Signal { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("statistic")]
        public string Statistic { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}