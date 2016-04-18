// <copyright file="AdvancedBingSingletonTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P = PatternsInAutomatedTests.Advanced.BingMainPage;
using S = PatternsInAutomatedTests.Advanced.BingMainPageSingletonDerived;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced
{
    [TestClass]
    public class AdvancedBingSingletonTests
    {       
        [TestInitialize]
        public void SetupTest()
        {
            Driver.StartBrowser();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.StopBrowser();
        }

        [TestMethod]
        public void SearchTextInBing_Advanced_PageObjectPattern_NoSingleton()
        {
            // Usage without Singleton
            P.BingMainPage bingMainPage = new P.BingMainPage();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.Validate().ResultsCount(",000 RESULTS");
        }

        [TestMethod]
        public void SearchTextInBing_Advanced_PageObjectPattern_Singleton()
        {
            S.BingMainPage.Instance.Navigate();
            S.BingMainPage.Instance.Search("Automate The Planet");
            S.BingMainPage.Instance.Validate().ResultsCount(",000 RESULTS");
        }
    }
}