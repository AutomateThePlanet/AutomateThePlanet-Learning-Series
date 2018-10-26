// <copyright file="ContentElement.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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

using HybridTestFramework.UITests.Core.Controls;
using Unity;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace HybridTestFramework.UITests.Selenium.Controls
{
    public class ContentElement : Element, IContentElement
    {
        public ContentElement(
            IWebDriver driver,
            IWebElement webElement,
            IUnityContainer container) : base(driver, webElement, container)
        {
        }

        public new string Content
        {
            get
            {
                return WebElement.Text;
            }
        }

        public void Hover()
        {
            var action = new Actions(Driver);
            action.MoveToElement(WebElement).Perform();
        }

        public bool IsEnabled
        {
            get
            {
                return WebElement.Enabled;
            }
        }

        public void Focus()
        {
            var action = new Actions(Driver);
            action.MoveToElement(WebElement).Perform();
        }
    }
}