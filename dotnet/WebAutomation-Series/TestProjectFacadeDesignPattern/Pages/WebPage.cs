using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TestProjectFacadeDesignPattern.Pages
{
    public abstract class WebPage<TElements>
        where TElements : WebElements
    {
        protected readonly IWebDriver Driver;

        protected WebPage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected TElements GetElements()
        {
            return (TElements)Activator.CreateInstance(typeof(TElements), new object[] { Driver });
        }
    }
}
