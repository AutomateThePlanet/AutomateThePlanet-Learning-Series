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

using HybridTestFramework.Core.Behaviours.Contracts;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Behaviours;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.SecondVersion;
using HybridTestFramework.UITests.Core.Utilities;
using HybridTestFramework.UITests.Selenium;
using ImprovedConfigureExecutionEngine.Pages.BingMain;
using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ImprovedConfigureExecutionEngine
{
    [TestClass,
    WebDriverExecutionEngine(Browser = Browsers.Chrome)]
    public class BingTests : HybridTestFramework.UITests.Core.Behaviours.SecondVersion.BaseTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var container = UnityContainerFactory.GetContainer();
            UnityContainerFactory.GetContainer().RegisterType<ITestBehaviorObserver, ExecutionEngineBehaviorObserver>(Guid.NewGuid().ToString());
            UnityContainerFactory.GetContainer().RegisterType<ITestExecutionEngine, TestExecutionEngine>(Guid.NewGuid().ToString());
            UnityContainerFactory.GetContainer().RegisterInstance(container);
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            var bingMainPage = Container.Resolve<BingMainPage>();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.AssertResultsCountIsAsExpected("422,000 results");
        }
    }
}