using System;
using System.Net;

namespace WebDriver.Tools.Redirects.Core
{
    public class WebRequestRedirectStrategy : IRedirectStrategy
    {
        public void Initialize()
        {
        }

        public string NavigateToFromUrl(string fromUrl)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fromUrl);
            request.Method = "HEAD";
            request.Timeout = (int)TimeSpan.FromHours(1).TotalMilliseconds;
            string currentSitesUrl = string.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    currentSitesUrl = response.ResponseUri.ToString();
                }
            }
            catch (WebException)
            {
                currentSitesUrl = null;
            }

            return currentSitesUrl;
        }
        public void Dispose()
        {
        }
    }
}
