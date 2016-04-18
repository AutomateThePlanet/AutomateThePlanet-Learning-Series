// <copyright file="HealthyDietMenuGeneratorTestsEdge.cs" company="Automate The Planet Ltd.">
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
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using WebDriver.Series.Tests.EdgeTests.Pages;

namespace WebDriver.Series.Tests
{
    [TestClass]
    public class HealthyDietMenuGeneratorTestsEdge
    {
        private IWebDriver driver;
        private string serverPath = "Microsoft Web Driver";

        [TestInitialize]
        public void SetupTest()
        {
            if (System.Environment.Is64BitOperatingSystem)
            {
                serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), serverPath);
            }
            else
            {
                serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles%"), serverPath);
            }
            EdgeOptions options = new EdgeOptions();
            options.PageLoadStrategy = EdgePageLoadStrategy.Eager;
            this.driver = new EdgeDriver(serverPath, options);

            //Set page load timeout to 5 seconds
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void FillAwsomeDietTest()
        {
            //this.Driver.Navigate().GoToUrl(@"http://automatetheplanet.com/healthy-diet-menu-generator/");
            this.driver.Url = @"http://automatetheplanet.com/healthy-diet-menu-generator/";
            var addAdditionalSugarCheckbox = this.driver.FindElement(By.Id("ninja_forms_field_18"));
            addAdditionalSugarCheckbox.Click();
            var ventiCoffeeRadioButton = this.driver.FindElement(By.Id("ninja_forms_field_19_1"));
            ventiCoffeeRadioButton.Click();
            //SelectElement selectElement = new SelectElement(this.Driver.FindElement(By.Id("ninja_forms_field_21")));
            //selectElement.SelectByText("7 x BBQ Ranch Burgers");
            var smotheredChocolateCakeCheckbox = this.driver.FindElement(By.Id("ninja_forms_field_27_2"));
            smotheredChocolateCakeCheckbox.Click();
            var addSomethingToDietTextArea = this.driver.FindElement(By.Id("ninja_forms_field_22"));
            addSomethingToDietTextArea.SendKeys(@"Goi cuon- This snack made from pork, shrimp, herbs, rice vermicelli and other ingredients wrapped in rice paper is served at room temperature. It’s “meat light,” with the flavors of refreshing herbs erupting in your mouth.");
            //var rockStarRating = this.Driver.FindElement(By.XPath("//*[@id='ninja_forms_field_20_div_wrap']/span/div[11]/a"));
            //rockStarRating.Click();
            var firstNameTextBox = this.driver.FindElement(By.Id("ninja_forms_field_23"));
            firstNameTextBox.SendKeys("Anton");
            var lastNameTextBox = this.driver.FindElement(By.Id("ninja_forms_field_24"));
            lastNameTextBox.SendKeys("Angelov");
            var emailTextBox = this.driver.FindElement(By.Id("ninja_forms_field_25"));
            emailTextBox.SendKeys("aangelov@yahoo.com");
            var awsomeDietSubmitButton = this.driver.FindElement(By.Id("ninja_forms_field_28"));
            awsomeDietSubmitButton.Click();
        }

        [TestMethod]
        public void FillAwsomeDietTest_ThroughPageObjects()
        {
            HealthyDietGeneratorPage healthyDietGeneratorPage = new HealthyDietGeneratorPage(this.driver);
            //this.Driver.Navigate().GoToUrl(healthyDietGeneratorPage.Url);
            this.driver.Url = healthyDietGeneratorPage.Url;
            healthyDietGeneratorPage.AddAdditionalSugarCheckbox.Click();
            healthyDietGeneratorPage.VentiCoffeeRadioButton.Click();
            //SelectElement selectElement = new SelectElement(healthyDietGeneratorPage.BurgersDropDown);
            //selectElement.SelectByText("7 x BBQ Ranch Burgers");
            healthyDietGeneratorPage.SmotheredChocolateCakeCheckbox.Click();
            healthyDietGeneratorPage.AddSomethingToDietTextArea.SendKeys(@"Goi cuon- This snack made from pork, shrimp, herbs, rice vermicelli and other ingredients wrapped in rice paper is served at room temperature. It’s “meat light,” with the flavors of refreshing herbs erupting in your mouth.");
            //healthyDietGeneratorPage.RockStarRating.Click();
            healthyDietGeneratorPage.FirstNameTextBox.SendKeys("Anton");
            healthyDietGeneratorPage.LastNameTextBox.SendKeys("Angelov");
            healthyDietGeneratorPage.EmailTextBox.SendKeys("aangelov@yahoo.com");
            healthyDietGeneratorPage.AwsomeDietSubmitButton.Click();
        }
    }
}