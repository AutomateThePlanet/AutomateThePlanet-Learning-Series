using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="RedirectService.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System;
using System.IO;
using System.Xml.Serialization;

namespace TestUrlRedirectsWebDriverHttpWebRequest.Redirects.Core
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