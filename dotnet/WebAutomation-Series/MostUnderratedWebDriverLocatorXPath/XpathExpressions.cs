// <copyright file="XpathExpressions.cs" company="Automate The Planet Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>
// <author>Anton Angelov</author>
// <site>https://automatetheplanet.com/</site>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MostUnderratedWebDriverLocatorXPath
{
    [TestClass]
    public class XpathExpressions
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Find_Column_Table_XPath()
        {
            _driver.Navigate().GoToUrl(@"http://www.tutorialspoint.com/html/html_tables.htm");
            var element = _driver.FindElement(By.XPath("//td[contains(text(), '5000')]/preceding-sibling::td[1]"));
            Assert.AreEqual("Ramesh Raman", element.Text);
        }
    }
}