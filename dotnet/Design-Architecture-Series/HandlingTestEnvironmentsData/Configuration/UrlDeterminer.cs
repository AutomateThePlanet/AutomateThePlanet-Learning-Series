// <copyright file="UrlDeterminer.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using Flurl;

namespace HandlingTestEnvironmentsData
{
    public static class UrlDeterminer
    {
        public static string GetEbayUrl(string urlPart)
        {
            return Url.Combine(ConfigurationService.Instance.GetUrlSettings().EbayUrl, urlPart).ToString();
        }

        public static string GetAmazonUrl(string urlPart)
        {
            return Url.Combine(ConfigurationService.Instance.GetUrlSettings().AmazonUrl, urlPart).ToString();
        }

        public static string GetKindleUrl(string urlPart)
        {
            return Url.Combine(ConfigurationService.Instance.GetUrlSettings().KindleUrl, urlPart).ToString();
        }
    }
}
