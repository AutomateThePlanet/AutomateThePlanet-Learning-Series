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