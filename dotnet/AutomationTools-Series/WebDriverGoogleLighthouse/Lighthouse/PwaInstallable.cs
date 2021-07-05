using Newtonsoft.Json;
namespace WebDriverGoogleLighthouse
{
    public class PwaInstallable
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}