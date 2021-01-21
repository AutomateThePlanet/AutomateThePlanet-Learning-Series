// <copyright file="BingMainPageElementMap.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

using FluentPageObjectPattern.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FluentPageObjectPattern.Pages.BingMainPage
{
    public class BingMainPageElementMap : BasePageElementMap
    {
        public IWebElement SearchBox 
        {
            get
            {
                return Browser.FindElement(By.Id("sb_form_q"));
            }
        }

        public IWebElement GoButton 
        {
            get
            {
                return Browser.FindElement(By.Id("sb_form_go"));
            }
        }
       
        public IWebElement ResultsCountDiv
        {
            get
            {
                return Browser.FindElement(By.Id("b_tween"));
            }
        }

        public IWebElement ImagesLink
        {
            get
            {
                return Browser.FindElement(By.LinkText("Images"));
            }
        }

        public SelectElement Sizes
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Size']")));
            }
        }

        public SelectElement Color
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Color']")));
            }
        }

        public SelectElement Type
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Type']")));
            }
        }

        public SelectElement Layout
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Layout']")));
            }
        }

        public SelectElement People
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'People']")));
            }
        }

        public SelectElement Date
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Date']")));
            }
        }

        public SelectElement License
        {
            get
            {
                return new SelectElement(Browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'License']")));
            }
        }
    }
}