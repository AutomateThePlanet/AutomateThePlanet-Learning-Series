﻿// <copyright file="RegistrationPage.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using static WebDriverTestsCSharpSix.CSharpSix.StaticUsingSyntax.TimestampBuilder;
using static WebDriverTestsCSharpSix.CSharpSix.StaticUsingSyntax.UniqueEmailGenerator;

namespace WebDriverTestsCSharpSix.CSharpSix.StaticUsingSyntax
{
    public class RegistrationPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://www.automatetheplanet.com/register";

        public RegistrationPage(IWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "emailId")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.Id, Using = "passId")]
        public IWebElement Pass { get; set; }

        [FindsBy(How = How.Id, Using = "userNameId")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "registerBtnId")]
        public IWebElement RegisterButton { get; set; }

        public User RegisterUser(string email = null, string password = null, string userName = null)
        {
            var user = new User();
            _driver.Navigate().GoToUrl(_url);
            if (string.IsNullOrEmpty(email))
            {
                email = BuildUniqueEmailTimestamp();
            }

            user.Email = email;
            Email.SendKeys(email);
            if (string.IsNullOrEmpty(password))
            {
                password = GenerateUniqueText();
            }

            user.Pass = password;
            Pass.SendKeys(password);
            if (string.IsNullOrEmpty(userName))
            {
                userName = GenerateUniqueText();
            }

            user.UserName = userName;
            UserName.SendKeys(userName);
            RegisterButton.Click();
            return user;
        }
    }
}
