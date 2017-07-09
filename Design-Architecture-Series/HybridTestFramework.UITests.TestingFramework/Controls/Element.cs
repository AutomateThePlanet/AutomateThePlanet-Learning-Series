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
        protected readonly TElementType HtmlControl;
        protected readonly ElementFinderService ElementFinderService;
        protected readonly IDriver Driver;

        public Element(
            IDriver driver,
            ArtOfTest.WebAii.ObjectModel.Element element,
            IUnityContainer container)
        {
            this.Driver = driver;
            HtmlControl = element.As<TElementType>();
            ElementFinderService = new ElementFinderService(container);
        }

        public string GetAttribute(string name)
        {
            var attribute = 
                HtmlControl.Attributes.FirstOrDefault(x => x.Name == name);
            return attribute == null ? null : attribute.Value;
        }

        public void WaitForExists()
        {
            HtmlControl.Wait.ForExists();
        }

        public void WaitForNotExists()
        {
            HtmlControl.Wait.ForExistsNot();
        }

        public void Click()
        {
            HtmlControl.Click();
        }

        public void MouseClick()
        {
            HtmlControl.ScrollToVisible(
                ScrollToVisibleType.ElementTopAtWindowTop);
            HtmlControl.MouseClick();
        }

        public bool IsVisible
        {
            get
            {
                return HtmlControl.IsVisible();
            }
        }

        public int Width
        {
            get
            {
                return int.Parse(GetAttribute("width"));
            }
        }

        public string CssClass
        {
            get
            {
                return HtmlControl.CssClass;
            }
        }

        public string Content
        {
            get
            {
                return HtmlControl.BaseElement.InnerText;
            }
        }

        public TElement Find<TElement>(By by)
            where TElement : class, IElement
        {
            return ElementFinderService.Find<TElement>(
                Driver,
                HtmlControl.Find,
                by);
        }

        public IEnumerable<TElement> FindAll<TElement>(By by)
            where TElement : class, IElement
        {
            return ElementFinderService.FindAll<TElement>(
                Driver,
                HtmlControl.Find, by);
        }

        public bool IsElementPresent(By by)
        {
            return ElementFinderService.IsElementPresent(
                HtmlControl.Find,
                by);
        }
    }
}