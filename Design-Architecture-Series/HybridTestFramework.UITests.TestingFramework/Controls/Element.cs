// <copyright file="Element.cs" company="Automate The Planet Ltd.">
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

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.TestingFramework.Engine;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace HybridTestFramework.UITests.TestingFramework.Controls
{
    public class Element<TElementType> : IElement
        where TElementType : HtmlControl, new()
    {
        protected readonly TElementType htmlControl;
        protected readonly ElementFinderService elementFinderService;
        protected readonly IDriver driver;

        public Element(
            IDriver driver,
            ArtOfTest.WebAii.ObjectModel.Element element,
            IUnityContainer container)
        {
            this.driver = driver;
            this.htmlControl = element.As<TElementType>();
            this.elementFinderService = new ElementFinderService(container);
        }

        public string GetAttribute(string name)
        {
            var attribute = 
                this.htmlControl.Attributes.FirstOrDefault(x => x.Name == name);
            return attribute == null ? null : attribute.Value;
        }

        public void WaitForExists()
        {
            this.htmlControl.Wait.ForExists();
        }

        public void WaitForNotExists()
        {
            this.htmlControl.Wait.ForExistsNot();
        }

        public void Click()
        {
            this.htmlControl.Click();
        }

        public void MouseClick()
        {
            this.htmlControl.ScrollToVisible(
                ScrollToVisibleType.ElementTopAtWindowTop);
            this.htmlControl.MouseClick();
        }

        public bool IsVisible
        {
            get
            {
                return this.htmlControl.IsVisible();
            }
        }

        public int Width
        {
            get
            {
                return int.Parse(this.GetAttribute("width"));
            }
        }

        public string CssClass
        {
            get
            {
                return this.htmlControl.CssClass;
            }
        }

        public string Content
        {
            get
            {
                return this.htmlControl.BaseElement.InnerText;
            }
        }

        public TElement Find<TElement>(Core.By by)
            where TElement : class, Core.Controls.IElement
        {
            return this.elementFinderService.Find<TElement>(
                this.driver,
                this.htmlControl.Find,
                by);
        }

        public IEnumerable<TElement> FindAll<TElement>(Core.By by)
            where TElement : class, Core.Controls.IElement
        {
            return this.elementFinderService.FindAll<TElement>(
                this.driver,
                this.htmlControl.Find, by);
        }

        public bool IsElementPresent(Core.By by)
        {
            return this.elementFinderService.IsElementPresent(
                this.htmlControl.Find,
                by);
        }
    }
}