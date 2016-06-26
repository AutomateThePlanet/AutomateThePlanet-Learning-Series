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