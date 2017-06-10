// <copyright file="WebRequestRedirectStrategy.cs" company="Automate The Planet Ltd.">
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
using System.Net;

namespace TestUrlRedirectsWebDriverHttpWebRequest.Redirects.Core
{
    public class WebRequestRedirectStrategy : IRedirectStrategy
    {
        public void Initialize()
        {
        }

        public string NavigateToFromUrl(string fromUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fromUrl);
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
