// <copyright file="RegistrationPage.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebDriverTestsCSharpSix.CSharpFive.StaticUsingSyntax
{
    public class RegistrationPage
    {
        private readonly IWebDriver driver;
        private readonly string url = @"http://www.automatetheplanet.com/register";

        public RegistrationPage(IWebDriver browser)
        {
            this.driver = browser;
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
            this.driver.Navigate().GoToUrl(this.url);
            if (string.IsNullOrEmpty(email))
            {
                email = UniqueEmailGenerator.BuildUniqueEmailTimestamp();
            }
            user.Email = email;
            this.Email.SendKeys(email);
            if (string.IsNullOrEmpty(password))
            {
                password = TimestampBuilder.GenerateUniqueText();
            }
            user.Pass = password;
            this.Pass.SendKeys(password);
            if (string.IsNullOrEmpty(userName))
            {
                userName = TimestampBuilder.GenerateUniqueText();
            }
            user.UserName = userName;
            this.UserName.SendKeys(userName);
            this.RegisterButton.Click();
            return user;
        }
    }
}
