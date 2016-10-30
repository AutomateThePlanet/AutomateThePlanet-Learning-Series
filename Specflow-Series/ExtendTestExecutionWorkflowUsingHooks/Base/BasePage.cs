// <copyright file="BasePage.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExtendTestExecutionWorkflowUsingHooks.Base
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait driverWait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            // wait 30 seconds.
            this.driverWait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        }

        public virtual string Url 
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual void Open(string part = "")
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                throw new ArgumentException("The main URL cannot be null or empty.");
            }
            this.driver.Navigate().GoToUrl(string.Concat(this.Url, part));
        }
    }
}