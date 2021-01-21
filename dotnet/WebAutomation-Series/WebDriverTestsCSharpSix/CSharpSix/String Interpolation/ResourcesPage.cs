﻿// <copyright file="ResourcesPage.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace WebDriverTestsCSharpSix.CSharpSix.StringInterpolation
{
    public class ResourcesPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"https://automatetheplanet.com/resources/";

        public ResourcesPage(IWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        public string Url => _url;

        [FindsBy(How = How.Id, Using = "emailId")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.Id, Using = "nameId")]
        public IWebElement Name { get; set; }

        [FindsBy(How = How.Id, Using = "downloadBtnId")]
        public IWebElement DownloadButton { get; set; }

        [FindsBy(How = How.Id, Using = "successMessageId")]
        public IWebElement SuccessMessage { get; set; }

        public IWebElement GetGridElement(string productName, int rowNumber)
        {
            var xpathLocator = $"(//span[text()='{productName}'])[{rowNumber}]/ancestor::td[1]/following-sibling::td[7]/span";
            return _driver.FindElement(By.XPath(xpathLocator));
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_url);

        public void DownloadSourceCode(string email, string name)
        {
            Email.SendKeys(email);
            Name.SendKeys(name);
            DownloadButton.Click();
            var successMessage = $"Thank you for downloading {name}! An email was sent to {email}. Check your inbox.";
            var waitElem = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            waitElem.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("successMessageId"), successMessage));
        }

        public void AssertSuccessMessage(string name, string email)
        {
            var successMessage = $"Thank you for downloading {name}! An email was sent to {email}. Check your inbox.";
            Assert.AreEqual(successMessage, SuccessMessage.Text);
        }
    }
}
