using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Item
    {
        [JsonProperty("timing")]
        public int Timing { get; set; }

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
        public int NumRequests { get; set; }

        [JsonProperty("numScripts")]
        public int NumScripts { get; set; }

        [JsonProperty("numStylesheets")]
        public int NumStylesheets { get; set; }

        [JsonProperty("numFonts")]
        public int NumFonts { get; set; }

        [JsonProperty("numTasks")]
        public int NumTasks { get; set; }

        [JsonProperty("numTasksOver10ms")]
        public int NumTasksOver10ms { get; set; }

        [JsonProperty("numTasksOver25ms")]
        public int NumTasksOver25ms { get; set; }

        [JsonProperty("numTasksOver50ms")]
        public int NumTasksOver50ms { get; set; }

        [JsonProperty("numTasksOver100ms")]
        public int NumTasksOver100ms { get; set; }

        [JsonProperty("numTasksOver500ms")]
        public int NumTasksOver500ms { get; set; }

        [JsonProperty("rtt")]
        public double Rtt { get; set; }

        [JsonProperty("throughput")]
        public double Throughput { get; set; }

        [JsonProperty("maxRtt")]
        public double MaxRtt { get; set; }

        [JsonProperty("maxServerLatency")]
        public double MaxServerLatency { get; set; }

        [JsonProperty("totalByteWeight")]
        public int TotalByteWeight { get; set; }

        [JsonProperty("totalTaskTime")]
        public double TotalTaskTime { get; set; }

        [JsonProperty("mainDocumentTransferSize")]
        public int MainDocumentTransferSize { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("endTime")]
        public double EndTime { get; set; }

        [JsonProperty("finished")]
        public bool Finished { get; set; }

        [JsonProperty("transferSize")]
        public int TransferSize { get; set; }

        [JsonProperty("resourceSize")]
        public int ResourceSize { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("serverResponseTime")]
        public double ServerResponseTime { get; set; }

        [JsonProperty("firstContentfulPaint")]
        public int FirstContentfulPaint { get; set; }

        [JsonProperty("firstContentfulPaintTs")]
        public long FirstContentfulPaintTs { get; set; }

        [JsonProperty("firstContentfulPaintAllFrames")]
        public int FirstContentfulPaintAllFrames { get; set; }

        [JsonProperty("firstContentfulPaintAllFramesTs")]
        public long FirstContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("firstMeaningfulPaint")]
        public int FirstMeaningfulPaint { get; set; }

        [JsonProperty("firstMeaningfulPaintTs")]
        public long FirstMeaningfulPaintTs { get; set; }

        [JsonProperty("largestContentfulPaint")]
        public int LargestContentfulPaint { get; set; }

        [JsonProperty("largestContentfulPaintTs")]
        public long LargestContentfulPaintTs { get; set; }

        [JsonProperty("largestContentfulPaintAllFrames")]
        public int LargestContentfulPaintAllFrames { get; set; }

        [JsonProperty("largestContentfulPaintAllFramesTs")]
        public long LargestContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("interactive")]
        public int Interactive { get; set; }

        [JsonProperty("interactiveTs")]
        public long InteractiveTs { get; set; }

        [JsonProperty("speedIndex")]
        public int SpeedIndex { get; set; }

        [JsonProperty("speedIndexTs")]
        public long SpeedIndexTs { get; set; }

        [JsonProperty("totalBlockingTime")]
        public int TotalBlockingTime { get; set; }

        [JsonProperty("maxPotentialFID")]
        public int MaxPotentialFID { get; set; }

        [JsonProperty("cumulativeLayoutShift")]
        public double CumulativeLayoutShift { get; set; }

        [JsonProperty("observedTimeOrigin")]
        public int ObservedTimeOrigin { get; set; }

        [JsonProperty("observedTimeOriginTs")]
        public long ObservedTimeOriginTs { get; set; }

        [JsonProperty("observedNavigationStart")]
        public int ObservedNavigationStart { get; set; }

        [JsonProperty("observedNavigationStartTs")]
        public long ObservedNavigationStartTs { get; set; }

        [JsonProperty("observedFirstPaint")]
        public int ObservedFirstPaint { get; set; }

        [JsonProperty("observedFirstPaintTs")]
        public long ObservedFirstPaintTs { get; set; }

        [JsonProperty("observedFirstContentfulPaint")]
        public int ObservedFirstContentfulPaint { get; set; }

        [JsonProperty("observedFirstContentfulPaintTs")]
        public long ObservedFirstContentfulPaintTs { get; set; }

        [JsonProperty("observedFirstContentfulPaintAllFrames")]
        public int ObservedFirstContentfulPaintAllFrames { get; set; }

        [JsonProperty("observedFirstContentfulPaintAllFramesTs")]
        public long ObservedFirstContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("observedFirstMeaningfulPaint")]
        public int ObservedFirstMeaningfulPaint { get; set; }

        [JsonProperty("observedFirstMeaningfulPaintTs")]
        public long ObservedFirstMeaningfulPaintTs { get; set; }

        [JsonProperty("observedLargestContentfulPaint")]
        public int ObservedLargestContentfulPaint { get; set; }

        [JsonProperty("observedLargestContentfulPaintTs")]
        public long ObservedLargestContentfulPaintTs { get; set; }

        [JsonProperty("observedLargestContentfulPaintAllFrames")]
        public int ObservedLargestContentfulPaintAllFrames { get; set; }

        [JsonProperty("observedLargestContentfulPaintAllFramesTs")]
        public long ObservedLargestContentfulPaintAllFramesTs { get; set; }

        [JsonProperty("observedTraceEnd")]
        public int ObservedTraceEnd { get; set; }

        [JsonProperty("observedTraceEndTs")]
        public long ObservedTraceEndTs { get; set; }

        [JsonProperty("observedLoad")]
        public int ObservedLoad { get; set; }

        [JsonProperty("observedLoadTs")]
        public long ObservedLoadTs { get; set; }

        [JsonProperty("observedDomContentLoaded")]
        public int ObservedDomContentLoaded { get; set; }

        [JsonProperty("observedDomContentLoadedTs")]
        public long ObservedDomContentLoadedTs { get; set; }

        [JsonProperty("observedCumulativeLayoutShift")]
        public double ObservedCumulativeLayoutShift { get; set; }

        [JsonProperty("observedCumulativeLayoutShiftMainFrame")]
        public double ObservedCumulativeLayoutShiftMainFrame { get; set; }

        [JsonProperty("observedTotalCumulativeLayoutShift")]
        public double ObservedTotalCumulativeLayoutShift { get; set; }

        [JsonProperty("observedFirstVisualChange")]
        public int ObservedFirstVisualChange { get; set; }

        [JsonProperty("observedFirstVisualChangeTs")]
        public long ObservedFirstVisualChangeTs { get; set; }

        [JsonProperty("observedLastVisualChange")]
        public int ObservedLastVisualChange { get; set; }

        [JsonProperty("observedLastVisualChangeTs")]
        public long ObservedLastVisualChangeTs { get; set; }

        [JsonProperty("observedSpeedIndex")]
        public int ObservedSpeedIndex { get; set; }

        [JsonProperty("observedSpeedIndexTs")]
        public long ObservedSpeedIndexTs { get; set; }

        [JsonProperty("lcpInvalidated")]
        public bool? LcpInvalidated { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("requestCount")]
        public int RequestCount { get; set; }

        [JsonProperty("mainThreadTime")]
        public double MainThreadTime { get; set; }

        [JsonProperty("blockingTime")]
        public int BlockingTime { get; set; }

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
        public int CacheLifetimeMs { get; set; }

        [JsonProperty("cacheHitProbability")]
        public double CacheHitProbability { get; set; }

        [JsonProperty("totalBytes")]
        public int TotalBytes { get; set; }

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
        public int Value { get; set; }
    }
}