// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Selenium.Controls;
using HybridTestFramework.UITests.Selenium.Engine;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvancedElementFindExtensions.Pages.BingMain;

namespace AdvancedElementFindExtensions
{
    [TestClass]
    public class BingTests
    {
        private IDriver driver;
        private IUnityContainer container;

        [TestInitialize]
        public void SetupTest()
        {
            this.container = new UnityContainer();
            this.container.RegisterType<IDriver, SeleniumDriver>();
            this.container.RegisterType<INavigationService, SeleniumDriver>();
            this.container.RegisterType<IBrowser, SeleniumDriver>();
            this.container.RegisterType<ICookieService, SeleniumDriver>();
            this.container.RegisterType<IDialogService, SeleniumDriver>();
            this.container.RegisterType<IElementFinder, SeleniumDriver>();
            this.container.RegisterType<IJavaScriptInvoker, SeleniumDriver>();
            this.container.RegisterType<IElement, Element>();
            this.container.RegisterType<IButton, Button>();
            this.container.RegisterType<ITextBox, TextBox>();
            this.container.RegisterType<IDiv, Div>();
            this.container.RegisterType<IContentElement, ContentElement>();
            this.container.RegisterInstance<IUnityContainer>(this.container);
            this.container.RegisterInstance<BrowserSettings>(BrowserSettings.DefaultFirefoxSettings);
            this.driver = this.container.Resolve<IDriver>();
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            var bingMainPage = this.container.Resolve<BingMainPage>();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.AssertResultsCountIsAsExpected(264);
        }
    }
}