// <copyright file="SignUpPage.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Security.Cryptography;
using HuddlePageObjectsPageSections.Pages.Sections;
using OpenQA.Selenium;

namespace HuddlePageObjectsPageSections.Pages
{
    public partial class SignUpPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"https://www.telerik.com/login/v2";

        public SignUpPage(IWebDriver browser)
        {
            _driver = browser;
            MainNavigationSection = new MainNavigationSection(_driver);
            ConnectWithSection = new ConnectWithSection(_driver);
        }

        public MainNavigationSection MainNavigationSection { get; private set; }
        public ConnectWithSection ConnectWithSection { get; private set; }

        public void Navigate() => _driver.Navigate().GoToUrl(_url);

        public void SignUpDefault(string email, string password)
        {
            FirstName.SendKeys(Guid.NewGuid().ToString());
            LastName.SendKeys(Guid.NewGuid().ToString());
            Company.SendKeys("Automate The Planet");
            Country.SelectByText("Bulgaria");
            Phone.SendKeys("+44 13 4436 0444");
            Email.SendKeys(email);
            Password.SendKeys(password);
            LaunchButton.Click();
        }
    }
}
