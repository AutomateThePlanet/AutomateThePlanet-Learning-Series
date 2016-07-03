// <copyright file="elementfinderservice.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Selenium.Controls;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public class ElementFinderService
    {
        private readonly IUnityContainer container;

        public ElementFinderService(IUnityContainer container)
        {
            this.container = container;
        }

        public TElement Find<TElement>(ISearchContext searchContext, Core.By by) 
            where TElement : class, Core.Controls.IElement
        {
            var element = searchContext.FindElement(by.ToSeleniumBy());
            TElement result = this.ResolveElement<TElement>(searchContext, element);

            return result;
        }

        public IEnumerable<TElement> FindAll<TElement>(ISearchContext searchContext, Core.By by) 
            where TElement : class, Core.Controls.IElement
        {
            var elements = searchContext.FindElements(by.ToSeleniumBy());
            List<TElement> resolvedElements = new List<TElement>();
            foreach (var currentElement in elements)
            {
                TElement result = this.ResolveElement<TElement>(searchContext, currentElement);
                resolvedElements.Add(result);
            }

            return resolvedElements;
        }

        public bool IsElementPresent(ISearchContext searchContext, Core.By by)
        {
            var element = this.Find<Element>(searchContext, by);
            return element.IsVisible;
        }

        private TElement ResolveElement<TElement>(ISearchContext searchContext, IWebElement currentElement)
            where TElement : class, Core.Controls.IElement
        {
            TElement result = this.container.Resolve<TElement>(new ResolverOverride[]
            {
                new ParameterOverride("driver", searchContext),
                new ParameterOverride("webElement", currentElement),
                new ParameterOverride("container", this.container)
            });
            return result;
        }
    }
}