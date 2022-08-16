// <copyright file="AdvancedBingSingletonTests.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using SingletonDesignPattern.Core;
using SingletonDesignPattern.Pages.BingMainPage;

namespace SingletonDesignPattern;

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
        var bingMainPage = new BingMainPage();
        bingMainPage.Navigate();
        bingMainPage.Search("Automate The Planet");
        bingMainPage.Validate().ResultsCount(",000 RESULTS");
    }

    [TestMethod]
    public void SearchTextInBing_Advanced_PageObjectPattern_Singleton()
    {
        Pages.BingMainPageSingletonDerived.BingMainPage.Instance.Navigate();
        Pages.BingMainPageSingletonDerived.BingMainPage.Instance.Search("Automate The Planet");
        Pages.BingMainPageSingletonDerived.BingMainPage.Instance.Validate().ResultsCount(",000 RESULTS");
    }
}