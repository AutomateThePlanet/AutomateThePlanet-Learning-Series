// <copyright file="AutomateThePlanetFacadeTests.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>

using AutomationTestDay.Facades.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationTestDay.Facades
{
    /*
     * Online Assessment System - https://www.surveymonkey.com/r/5XYT3QX
     * 1. Refactor tests so that category page background is asserted only for the 'Design Pattern' category
     * 2. Create a new test for 'Design And Architecture' category, article- 'Create Hybrid Test Framework – Advanced Element Find Extensions'
     * 3. Remove Find How button assert from all tests
     * 4. Modify the code to use Enums instead of plain text for the categories' names
     */
    [TestFixture]
    public class AutomateThePlanetFacadeTests
    {
        private IWebDriver _driver;
        private AutomateThePlanetFacade _facade;

        [OneTimeSetUp]
        public void ClassInit()
        {
            _driver = new ChromeDriver();
            _facade = new AutomateThePlanetFacade(_driver);
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _driver.Dispose();
        }

        [Test]
        public void DownloadSourceCode_DesignPatterns_Singleton()
        {
            _facade.DownloadSourceCode("Design Patterns", "Singleton Design Pattern in Automated Testing");
        }

        [Test]
        public void DownloadSourceCode_DesignPatterns_Facade()
        {
            _facade.DownloadSourceCode("Design Patterns", "Facade Design Pattern in Automated Testing");
        }

        [Test]
        public void DownloadSourceCode_Jenkins_Outpout()
        {
            _facade.DownloadSourceCode("DevOps and CI", "Output MSTest Tests Logs To Jenkins Console Log");
        }

        [Test]
        public void DownloadSourceCode_AutomationTools_MultipleFiles()
        {
            _facade.DownloadSourceCode("Automation Tools", "Create Multiple Files Page Objects with Visual Studio Item Templates");
        }
    }
}
