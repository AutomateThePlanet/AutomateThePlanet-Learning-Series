// <copyright file="element.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Selenium.Engine;
using Unity;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using Unity;

namespace HybridTestFramework.UITests.Selenium.Controls
{
    public class Element : IElement
    {
        protected readonly IWebElement WebElement;
        protected readonly IWebDriver Driver;
        protected readonly ElementFinderService ElementFinderService;

        public Element(IWebDriver driver, IWebElement webElement, IUnityContainer container)
        {
            this.Driver = driver;
            this.WebElement = webElement;
            ElementFinderService = new ElementFinderService(container);
        }

        public string GetAttribute(string name)
        {
            return WebElement.GetAttribute(name);
        }

        public void WaitForExists()
        {
            throw new NotImplementedException();
        }

        public void WaitForNotExists()
        {
            throw new NotImplementedException();
        }

        public void Click()
        {
            WebElement.Click();
        }

        public void MouseClick()
        {
            var builder = new Actions(Driver);
            builder.MoveToElement(WebElement).Click().Build().Perform();
        }

        public bool IsVisible
        {
            get
            {
                return WebElement.Displayed;
            }
        }

        public int Width
        {
            get
            {
                return WebElement.Size.Width;
            }
        }

        public string CssClass
        {
            get
            {
                return WebElement.GetAttribute("className");
            }
        }

        public string Content
        {
            get
            {
                return WebElement.Text;
            }
        }

        public TElement Find<TElement>(Core.By by) where TElement : class, IElement
        {
            return ElementFinderService.Find<TElement>(WebElement, by);
        }

        public IEnumerable<TElement> FindAll<TElement>(Core.By by) where TElement : class, IElement
        {
            return ElementFinderService.FindAll<TElement>(WebElement, by);
        }

        public bool IsElementPresent(Core.By by)
        {
            return ElementFinderService.IsElementPresent(WebElement, by);
        }
    }
}