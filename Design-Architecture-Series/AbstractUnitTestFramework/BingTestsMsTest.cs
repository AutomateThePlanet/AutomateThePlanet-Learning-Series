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

using AbstractUnitTestFramework.Pages.BingMain;
using HybridTestFramework.Core.Asserts;
using HybridTestFramework.Core.MSTest.Asserts;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Utilities;
using HybridTestFramework.UITests.TestingFramework.Controls;
using HybridTestFramework.UITests.TestingFramework.Engine;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbstractUnitTestFramework
{
    [TestClass]
    public class BingTestsMsTest
    {
        private IDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            UnityContainerFactory.GetContainer().RegisterType<IDriver, TestingFrameworkDriver>();
            UnityContainerFactory.GetContainer().RegisterType<INavigationService, TestingFrameworkDriver>();
            UnityContainerFactory.GetContainer().RegisterType<IBrowser, TestingFrameworkDriver>();
            UnityContainerFactory.GetContainer().RegisterType<ICookieService, TestingFrameworkDriver>();
            UnityContainerFactory.GetContainer().RegisterType<IDialogService, TestingFrameworkDriver>();
            UnityContainerFactory.GetContainer().RegisterType<IElementFinder, TestingFrameworkDriver>();
            UnityContainerFactory.GetContainer().RegisterType<IJavaScriptInvoker, TestingFrameworkDriver>();

            UnityContainerFactory.GetContainer().RegisterType<IInputSubmit, InputSubmit>();
            UnityContainerFactory.GetContainer().RegisterType<ISearch, Search>();
            UnityContainerFactory.GetContainer().RegisterType<IDiv, Div>();
            UnityContainerFactory.GetContainer().RegisterType<BingMainPage>();

            UnityContainerFactory.GetContainer().RegisterInstance<IUnityContainer>(UnityContainerFactory.GetContainer());
            UnityContainerFactory.GetContainer().RegisterInstance<BrowserSettings>(BrowserSettings.DefaultChomeSettings);
            this.driver = UnityContainerFactory.GetContainer().Resolve<IDriver>();

            UnityContainerFactory.GetContainer().RegisterInstance<IDriver>(this.driver);
            UnityContainerFactory.GetContainer().RegisterInstance<IBrowser>(this.driver);
            UnityContainerFactory.GetContainer().RegisterInstance<ICookieService>(this.driver);
            UnityContainerFactory.GetContainer().RegisterInstance<IDialogService>(this.driver);
            UnityContainerFactory.GetContainer().RegisterInstance<IJavaScriptInvoker>(this.driver);
            UnityContainerFactory.GetContainer().RegisterInstance<INavigationService>(this.driver);
            UnityContainerFactory.GetContainer().RegisterInstance<IElementFinder>(this.driver);

            // Register the concrete Unit Testing Framework.
            UnityContainerFactory.GetContainer().RegisterType<IAssert, MSTestAssert>();
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            var bingMainPage = UnityContainerFactory.GetContainer().Resolve<BingMainPage>();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.AssertResultsCountIsAsExpected("422,000 results");
        }
    }
}