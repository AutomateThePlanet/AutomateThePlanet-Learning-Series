// <copyright file="SignInPage.cs" company="Automate The Planet Ltd.">
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

using BehavioursDesignPattern.Base;
using OpenQA.Selenium;

namespace BehavioursDesignPattern.Pages.SignInPage
{
    public partial class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public void Login(string email, string password)
        {
            this.EmailInput.SendKeys(email);
            this.PasswordInput.SendKeys(password);
            this.SignInButton.Click();
        }

        public void WaitForPageToLoad()
        {
            // wait for a specific element to show up.
        }
    }
}