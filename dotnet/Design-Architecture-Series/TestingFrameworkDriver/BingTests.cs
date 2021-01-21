// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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
using HybridTestFramework.UITests.Core.Extensions;
using HybridTestFramework.UITests.TestingFramework.Controls;
using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Engine = HybridTestFramework.UITests.TestingFramework.Engine;

namespace TestingFrameworkDriver
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
            _container.RegisterType<IDriver, Engine.TestingFrameworkDriver>();
            _container.RegisterType<INavigationService, Engine.TestingFrameworkDriver>();
            _container.RegisterType<IBrowser, Engine.TestingFrameworkDriver>();
            _container.RegisterType<ICookieService, Engine.TestingFrameworkDriver>();
            _container.RegisterType<IDialogService, Engine.TestingFrameworkDriver>();
            _container.RegisterType<IElementFinder, Engine.TestingFrameworkDriver>();
            _container.RegisterType<IJavaScriptInvoker, Engine.TestingFrameworkDriver>();
            _container.RegisterType<IAnchor, Anchor>();
            _container.RegisterInstance(_container);
            _container.RegisterInstance(BrowserSettings.DefaultInternetExplorerSettings);
            _driver = _container.Resolve<IDriver>();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void NavigateToAutomateThePlanet()
        {
            _driver.NavigateByAbsoluteUrl(@"http://automatetheplanet.com/");
            var blogButton = _driver.Find<IAnchor>(
                AdvancedBy.Xpath("//*[@id='tve_editor']/div[2]/div[4]/div/div/div/div/div/a"));
            blogButton.Hover();
            Console.WriteLine(blogButton.Content);
            _driver.NavigateByAbsoluteUrl(
                @"http://automatetheplanet.com/download-source-code/");
            _driver.ClickBackButton();
            Console.WriteLine(_driver.Title);
        }
    }
}