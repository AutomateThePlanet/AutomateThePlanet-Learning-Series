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
using HybridTestFramework.UITests.Core.Extensions;
using HybridTestFramework.UITests.TestingFramework.Controls;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Engine = HybridTestFramework.UITests.TestingFramework.Engine;

namespace TestingFrameworkDriver
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
            this.container.RegisterType<IDriver, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<INavigationService, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<IBrowser, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<ICookieService, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<IDialogService, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<IElementFinder, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<IJavaScriptInvoker, Engine.TestingFrameworkDriver>();
            this.container.RegisterType<IAnchor, Anchor>();
            this.container.RegisterInstance<IUnityContainer>(this.container);
            this.container.RegisterInstance<BrowserSettings>(BrowserSettings.DefaultInternetExplorerSettings);
            this.driver = this.container.Resolve<IDriver>();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void NavigateToAutomateThePlanet()
        {
            this.driver.NavigateByAbsoluteUrl(@"http://automatetheplanet.com/");
            var blogButton = this.driver.Find<IAnchor>(
                AdvancedBy.Xpath("//*[@id='tve_editor']/div[2]/div[4]/div/div/div/div/div/a"));
            blogButton.Hover();
            Console.WriteLine(blogButton.Content);
            this.driver.NavigateByAbsoluteUrl(
                @"http://automatetheplanet.com/download-source-code/");
            this.driver.ClickBackButton();
            Console.WriteLine(this.driver.Title);
        }
    }
}