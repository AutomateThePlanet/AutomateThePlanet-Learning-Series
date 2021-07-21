using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class BoundingRect
    {
        [JsonProperty("top")]
        public double Top { get; set; }

        [JsonProperty("bottom")]
        public double Bottom { get; set; }

        [JsonProperty("left")]
        public double Left { get; set; }

        [JsonProperty("right")]
        public double Right { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }
    }
}