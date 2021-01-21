// <copyright file="HealthyDietMenuGeneratorTestsEdge.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftEdgeWebDriverWhatEverybodyOughtKnowAbout.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace MicrosoftEdgeWebDriverWhatEverybodyOughtKnowAbout
{
    [TestClass]
    public class HealthyDietMenuGeneratorTestsEdge
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            var options = new EdgeOptions()
            {
                PageLoadStrategy = PageLoadStrategy.Eager
            };
            _driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void FillAwsomeDietTest()
        {
            _driver.Navigate().GoToUrl(@"https://automatetheplanet.com/healthy-diet-menu-generator/");
            var addAdditionalSugarCheckbox = _driver.FindElement(By.Id("ninja_forms_field_18"));
            addAdditionalSugarCheckbox.Click();
            var ventiCoffeeRadioButton = _driver.FindElement(By.Id("ninja_forms_field_19_1"));
            ventiCoffeeRadioButton.Click();
            var selectElement = new SelectElement(_driver.FindElement(By.Id("ninja_forms_field_21")));
            selectElement.SelectByText("7 x BBQ Ranch Burgers");
            var smotheredChocolateCakeCheckbox = _driver.FindElement(By.Id("ninja_forms_field_27_2"));
            smotheredChocolateCakeCheckbox.Click();
            var addSomethingToDietTextArea = _driver.FindElement(By.Id("ninja_forms_field_22"));
            addSomethingToDietTextArea.SendKeys(@"Goi cuon- This snack made from pork, shrimp, herbs, rice vermicelli and other ingredients wrapped in rice paper is served at room temperature. It’s “meat light,” with the flavors of refreshing herbs erupting in your mouth.");
            var rockStarRating = _driver.FindElement(By.XPath("//*[@id='ninja_forms_field_20_div_wrap']/span/div[11]/a"));
            rockStarRating.Click();
            var firstNameTextBox = _driver.FindElement(By.Id("ninja_forms_field_23"));
            firstNameTextBox.SendKeys("Anton");
            var lastNameTextBox = _driver.FindElement(By.Id("ninja_forms_field_24"));
            lastNameTextBox.SendKeys("Angelov");
            var emailTextBox = _driver.FindElement(By.Id("ninja_forms_field_25"));
            emailTextBox.SendKeys("aangelov@yahoo.com");
            var awsomeDietSubmitButton = _driver.FindElement(By.Id("ninja_forms_field_28"));
            awsomeDietSubmitButton.Click();
        }

        [TestMethod]
        public void FillAwsomeDietTest_ThroughPageObjects()
        {
            var healthyDietGeneratorPage = new HealthyDietGeneratorPage(_driver);
            _driver.Navigate().GoToUrl(healthyDietGeneratorPage.Url);
            healthyDietGeneratorPage.AddAdditionalSugarCheckbox.Click();
            healthyDietGeneratorPage.VentiCoffeeRadioButton.Click();
            var selectElement = new SelectElement(healthyDietGeneratorPage.BurgersDropDown);
            selectElement.SelectByText("7 x BBQ Ranch Burgers");
            healthyDietGeneratorPage.SmotheredChocolateCakeCheckbox.Click();
            healthyDietGeneratorPage.AddSomethingToDietTextArea.SendKeys(@"Goi cuon- This snack made from pork, shrimp, herbs, rice vermicelli and other ingredients wrapped in rice paper is served at room temperature. It’s “meat light,” with the flavors of refreshing herbs erupting in your mouth.");
            healthyDietGeneratorPage.RockStarRating.Click();
            healthyDietGeneratorPage.FirstNameTextBox.SendKeys("Anton");
            healthyDietGeneratorPage.LastNameTextBox.SendKeys("Angelov");
            healthyDietGeneratorPage.EmailTextBox.SendKeys("aangelov@yahoo.com");
            healthyDietGeneratorPage.AwsomeDietSubmitButton.Click();
        }
    }
}