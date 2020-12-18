// <copyright file="WebPage.cs" company="Automate The Planet Ltd.">
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
using TestProjectPageObjectsSingleton.Pages.SingletonBingMainPage;
using OpenQA.Selenium;

namespace TestProjectPageObjectsSingleton
{
    public abstract class WebPage<TPage>
        where TPage : new()
    {
        private static readonly Lazy<TPage> _lazyPage = new Lazy<TPage>(() => new TPage());

        protected readonly IWebDriver WrappedDriver;

        protected WebPage()
        {
            WrappedDriver = Driver.GetBrowser() ?? throw new ArgumentNullException("The wrapped IWebDriver instance is not initialized.");
        }

        public static TPage GetInstance()
        {
            return _lazyPage.Value;
        }
    }
}
