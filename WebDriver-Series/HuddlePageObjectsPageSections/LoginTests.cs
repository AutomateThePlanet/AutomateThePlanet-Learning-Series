// <copyright file="LoginTests.cs" company="Automate The Planet Ltd.">
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
// <site>https://automatetheplanet.com/</site>

using HuddlePageObjectsPageSections.Pages;
using HuddlePageObjectsPageSections.Pages.LoginPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace HuddlePageObjectsPageSections
{
    [TestClass]
    public class LoginTests
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
        public void SuccessfullyLogin_WhenLoginWithExistingUser()
        {
            var loginPage = new LoginPage(_driver);

            loginPage.Navigate();
            loginPage.LoginSection.Login("myemail@automatetheplanet.com", "somePassword");
        }

        [TestMethod]
        public void SuccessfullyLogin_WhenLoginWithExistingGoogleAccount()
        {
            var loginPage = new LoginPage(_driver);

            loginPage.Navigate();
            loginPage.ConnectWithSection.GoogleButton.Click();
        }

        [TestMethod]
        public void SignUpWithNewDefaultUser()
        {
            var signUpPage = new SignUpPage(_driver);

            signUpPage.Navigate();
            signUpPage.SignUpDefault("myemail@automatetheplanet.com", "somePassword");
        }
    }
}
