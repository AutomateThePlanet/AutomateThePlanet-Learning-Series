using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebDriver.Tools.Redirects.Core
{
    public class RedirectService : IDisposable
    {
        readonly IRedirectStrategy redirectEngine;
        private Sites sites;

        public RedirectService(IRedirectStrategy redirectEngine)
        {
            this.redirectEngine = redirectEngine;
            this.redirectEngine.Initialize();
            this.InitializeRedirectUrls();
        }

        public void TestRedirects()
        {
            bool shouldFail = false;

            foreach (var currentSite in this.sites.Site)
            {
                Uri baseUri = new Uri(currentSite.Url);

                foreach (var currentRedirect in currentSite.Redirects.Redirect)
                {
                    Uri currentFromUrl = new Uri(baseUri, currentRedirect.From);
                    Uri currentToUrl = new Uri(baseUri, currentRedirect.To);

                    string currentSitesUrl = this.redirectEngine.NavigateToFromUrl(currentFromUrl.AbsoluteUri);
                    try
                    {
                        Assert.AreEqual<string>(currentToUrl.AbsoluteUri, currentSitesUrl);
                        Console.WriteLine(string.Format("{0} \n OK", currentFromUrl));
                    }
                    catch (Exception)
                    {
                        shouldFail = true;
                        Console.WriteLine(string.Format("{0} \n was NOT redirected to \n {1}", currentFromUrl, currentToUrl));
                    }
                }
            }
            if (shouldFail)
            {
                throw new Exception("There were incorrect redirects!");
            }
        }

        public void Dispose()
        {
            redirectEngine.Dispose();
        }

        private void InitializeRedirectUrls()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Sites));
            TextReader reader = new StreamReader(@"redirect-URLs.xml");
            this.sites = (Sites)deserializer.Deserialize(reader);
            reader.Close();
        }
    }
}