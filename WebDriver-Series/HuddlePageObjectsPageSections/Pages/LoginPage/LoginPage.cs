﻿// <copyright file="LoginPage.cs" company="Automate The Planet Ltd.">
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
using HuddlePageObjectsPageSections.Pages.Sections;
using OpenQA.Selenium;

namespace HuddlePageObjectsPageSections.Pages.LoginPage
{
    public partial class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"https://www.telerik.com/login";

        public LoginPage(IWebDriver browser)
        {
            _driver = browser;
            LoginSection = new LoginSection(_driver);
            MainNavigationSection = new MainNavigationSection(_driver);
            ConnectWithSection = new ConnectWithSection(_driver);
        }

        public LoginSection LoginSection { get; private set; }
        public MainNavigationSection MainNavigationSection { get; private set; }
        public ConnectWithSection ConnectWithSection { get; private set; }

        public void Navigate() => _driver.Navigate().GoToUrl(_url);
    }
}