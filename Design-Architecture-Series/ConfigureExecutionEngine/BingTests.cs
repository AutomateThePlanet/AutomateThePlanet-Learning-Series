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

using ConfigureExecutionEngine.Pages;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Behaviours;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Enums;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigureExecutionEngine
{
    [TestClass,
    ExecutionEngineAttribute(ExecutionEngineType.TestStudio, Browsers.Firefox)]
    public class BingTests : BaseTest
    {
        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            var bingMainPage = this.Container.Resolve<BingMainPage>();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.AssertResultsCountIsAsExpected(264);
        }

        /*
        - BaseTest second version register new observers. Istead of using InitializeTestExecutionBehaviorObservers. No Dependency to concrete behaviours.
         * */
    }
}