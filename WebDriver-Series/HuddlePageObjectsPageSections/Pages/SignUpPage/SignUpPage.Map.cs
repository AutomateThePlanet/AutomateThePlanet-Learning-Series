﻿// <copyright file="SignUpPage.Map.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Support.UI;

namespace HuddlePageObjectsPageSections.Pages
{
    public partial class SignUpPage
    {
        public IWebElement FirstName => _driver.FindElement(By.Id("tbFirstName"));
        public IWebElement LastName => _driver.FindElement(By.Id("tbLastName"));
        public IWebElement Company => _driver.FindElement(By.Id("tbCompany"));
        public IWebElement Email => _driver.FindElement(By.Id("tbEmail"));
        public IWebElement Phone => _driver.FindElement(By.Id("tbPhone"));
        public IWebElement Password => _driver.FindElement(By.Id("tbPassword"));
        public IWebElement LaunchButton => _driver.FindElement(By.Id("btnSubmit"));
        public SelectElement Country => new SelectElement(_driver.FindElement(By.Id("ddlCountry")));
    }
}
