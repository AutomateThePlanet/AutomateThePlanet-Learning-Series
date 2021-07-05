using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class BoundingRect
    {
        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("bottom")]
        public int Bottom { get; set; }

        [JsonProperty("left")]
        public int Left { get; set; }

        [JsonProperty("right")]
        public int Right { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}