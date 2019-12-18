// <copyright file="RunTestsInSelenoid.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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
// <site>https://automatetheplanet.com/</site>

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace WebDriverTestsDockerSelenoid
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class RunTestsInSelenoid
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetupTest()
        {
            var driverOptions = new ChromeOptions();
         
            var runName = GetType().Assembly.GetName().Name;
            var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";

            driverOptions.AddAdditionalCapability("name", runName, true);
            driverOptions.AddAdditionalCapability("videoName", $"{runName}.{timestamp}.mp4", true);
            driverOptions.AddAdditionalCapability("logName", $"{runName}.{timestamp}.log", true);
            driverOptions.AddAdditionalCapability("enableVNC", true, true);
            driverOptions.AddAdditionalCapability("enableVideo", true, true);
            driverOptions.AddAdditionalCapability("enableLog", true, true);
            driverOptions.AddAdditionalCapability("screenResolution", "1920x1080x24", true);

            _driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), driverOptions);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [Test]
        public void FillAllTextFields()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
            var textBoxes = _driver.FindElements(By.Name("fname"));
            foreach (var textBox in textBoxes)
            {
                textBox.SendKeys(Guid.NewGuid().ToString());
            }
        }

        [Test]
        public void FillAllSelects()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);

            var selects = _driver.FindElements(By.TagName("select"));
            foreach (var select in selects)
            {
                var selectElement = new SelectElement(select);
                selectElement.SelectByText("Mercedes");
            }
        }

        [Test]
        public void FillAllColors()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
            var colors = _driver.FindElements(By.XPath("//input[@type='color']"));
            foreach (var color in colors)
            {
                SetValueAttribute(_driver, color, "#000000");
            }
        }

        [Test]
        public void SetAllDates()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
            var dates = _driver.FindElements(By.XPath("//input[@type='date']"));
            foreach (var date in dates)
            {
                SetValueAttribute(_driver, date, "2020-06-01");
            }
        }

        [Test]
        public void ClickAllRadioButtons()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
            var radioButtons = _driver.FindElements(By.XPath("//input[@type='radio']"));
            foreach (var radio in radioButtons)
            {
                radio.Click();
            }
        }

        private void SetValueAttribute(IWebDriver driver, IWebElement element, string value)
        {
            SetAttribute(driver, element, "value", value);
        }

        private void SetAttribute(IWebDriver driver, IWebElement element, string attributeName, string attributeValue)
        {
            driver.ExecuteJavaScript
            (
                "arguments[0].setAttribute(arguments[1], arguments[2]);",
                element,
                attributeName,
                attributeValue);
        }
    }
}