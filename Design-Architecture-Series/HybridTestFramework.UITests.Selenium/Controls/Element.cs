// <copyright file="element.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Selenium.Engine;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Selenium.Controls
{
    public class Element : IElement
    {
        protected readonly IWebElement webElement;
        protected readonly IWebDriver driver;
        protected readonly ElementFinderService elementFinderService;

        public Element(IWebDriver driver, IWebElement webElement, IUnityContainer container)
        {
            this.driver = driver;
            this.webElement = webElement;
            this.elementFinderService = new ElementFinderService(container);
        }

        public string GetAttribute(string name)
        {
            return this.webElement.GetAttribute(name);
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
            this.webElement.Click();
        }

        public void MouseClick()
        {
            Actions builder = new Actions(this.driver);
            builder.MoveToElement(this.webElement).Click().Build().Perform();
        }

        public bool IsVisible
        {
            get
            {
                return this.webElement.Displayed;
            }
        }

        public int Width
        {
            get
            {
                return this.webElement.Size.Width;
            }
        }

        public string CssClass
        {
            get
            {
                return webElement.GetAttribute("className");
            }
        }

        public string Content
        {
            get
            {
                return this.webElement.Text;
            }
        }

        public TElement Find<TElement>(Core.By by) where TElement : class, Core.Controls.IElement
        {
            return this.elementFinderService.Find<TElement>(this.webElement, by);
        }

        public IEnumerable<TElement> FindAll<TElement>(Core.By by) where TElement : class, Core.Controls.IElement
        {
            return this.elementFinderService.FindAll<TElement>(this.webElement, by);
        }

        public bool IsElementPresent(Core.By by)
        {
            return this.elementFinderService.IsElementPresent(this.webElement, by);
        }
    }
}