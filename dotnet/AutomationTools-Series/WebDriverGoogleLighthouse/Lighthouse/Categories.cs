using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class Categories
    {
        [JsonProperty("performance")]
        public Performance Performance { get; set; }
    }
}