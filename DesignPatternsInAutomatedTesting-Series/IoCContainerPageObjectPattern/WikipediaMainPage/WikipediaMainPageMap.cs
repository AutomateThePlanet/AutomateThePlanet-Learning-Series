// <copyright file="WikipediaMainPageMap.cs" company="Automate The Planet Ltd.">
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

using IoCContainerPageObjectPattern.Base;
using OpenQA.Selenium;

namespace IoCContainerPageObjectPattern.WikipediaMainPage
{
    public class WikipediaMainPageMap : BasePageElementMap
    {
        public IWebElement SearchBox 
        {
            get
            {
                return this.browser.FindElement(By.Id("searchInput"));
            }
        }

        public IWebElement SearchButton 
        {
            get
            {
                return this.browser.FindElement(By.Id("searchButton"));
            }
        }

        public IWebElement ContentsToggleLink
        {
            get
            {
                return this.browser.FindElement(By.Id("togglelink"));
            }
        }

        public IWebElement ContentsList
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='toc']/ul"));
            }
        }
    }
}