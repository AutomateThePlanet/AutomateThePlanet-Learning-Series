﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="RedirectService.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
        readonly IRedirectStrategy _redirectEngine;
        private Sites _sites;

        public RedirectService(IRedirectStrategy redirectEngine)
        {
            this._redirectEngine = redirectEngine;
            this._redirectEngine.Initialize();
            InitializeRedirectUrls();
        }

        public void TestRedirects()
        {
            var shouldFail = false;

            foreach (var currentSite in _sites.Site)
            {
                var baseUri = new Uri(currentSite.Url);

                foreach (var currentRedirect in currentSite.Redirects.Redirect)
                {
                    var currentFromUrl = new Uri(baseUri, currentRedirect.From);
                    var currentToUrl = new Uri(baseUri, currentRedirect.To);

                    var currentSitesUrl = _redirectEngine.NavigateToFromUrl(currentFromUrl.AbsoluteUri);
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
            _redirectEngine.Dispose();
        }

        private void InitializeRedirectUrls()
        {
            var deserializer = new XmlSerializer(typeof(Sites));
            TextReader reader = new StreamReader(@"redirect-URLs.xml");
            _sites = (Sites)deserializer.Deserialize(reader);
            reader.Close();
        }
    }
}