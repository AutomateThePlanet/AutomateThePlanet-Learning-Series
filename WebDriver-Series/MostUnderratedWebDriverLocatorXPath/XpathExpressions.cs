// <copyright file="XpathExpressions.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace MostUnderratedWebDriverLocatorXPath
{
    [TestClass]
    public class XpathExpressions
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            this.Driver = new FirefoxDriver();
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        [TestMethod]
        public void Find_Column_Table_XPath()
        {
            this.Driver.Navigate().GoToUrl(@"http://www.tutorialspoint.com/html/html_tables.htm");
            var expression = By.XPath("/html/body/div[1]/div/div/div/div[2]/div[1]/div/div[6]/table/tbody");
            this.Wait.Until(x => x.FindElement(expression));
            var element = this.Driver.FindElement(By.XPath("//td[contains(text(), '5000')]/preceding-sibling::td[1]"));
            Assert.AreEqual<string>("Ramesh Raman", element.Text);
        }
    }
}
