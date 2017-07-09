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
        private IDriver _driver;
        private IUnityContainer _container;

        [TestInitialize]
        public void SetupTest()
        {
            _container = new UnityContainer();
            _container.RegisterType<IDriver, SeleniumDriver>();
            _container.RegisterType<INavigationService, SeleniumDriver>();
            _container.RegisterType<IBrowser, SeleniumDriver>();
            _container.RegisterType<ICookieService, SeleniumDriver>();
            _container.RegisterType<IDialogService, SeleniumDriver>();
            _container.RegisterType<IElementFinder, SeleniumDriver>();
            _container.RegisterType<IJavaScriptInvoker, SeleniumDriver>();
            _container.RegisterType<IElement, Element>();
            _container.RegisterType<IButton, Button>();
            _container.RegisterType<ITextBox, TextBox>();
            _container.RegisterType<IDiv, Div>();
            _container.RegisterType<IContentElement, ContentElement>();
            _container.RegisterType<BingMainPage>();
            _container.RegisterInstance(_container);
            _container.RegisterInstance(BrowserSettings.DefaultInternetExplorerSettings);
            _driver = _container.Resolve<IDriver>();
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            var bingMainPage = _container.Resolve<BingMainPage>();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.AssertResultsCountIsAsExpected(264);
        }
    }
}