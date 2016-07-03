// <copyright file="ByExtensions.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Enums;

namespace HybridTestFramework.UITests.Selenium
{
    public static class ByExtensions
    {
        public static OpenQA.Selenium.By ToSeleniumBy(this Core.By by)
        {
            switch (by.Type)
            {
                case SearchType.Id:
                    return OpenQA.Selenium.By.Id(by.Value);
                case SearchType.Tag:
                    return OpenQA.Selenium.By.TagName(by.Value);
                case SearchType.CssClass:
                    return OpenQA.Selenium.By.ClassName(by.Value);
                case SearchType.XPath:
                    return OpenQA.Selenium.By.XPath(by.Value);
                case SearchType.CssSelector:
                    return OpenQA.Selenium.By.CssSelector(by.Value);
                case SearchType.Name:
                    return OpenQA.Selenium.By.Name(by.Value);
                default:
                    throw new Exception(string.Format("Unknown search type: {0}", by.Type));
            }
        }
    }
}
