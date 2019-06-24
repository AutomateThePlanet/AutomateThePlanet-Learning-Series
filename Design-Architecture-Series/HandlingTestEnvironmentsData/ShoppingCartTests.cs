// <copyright file="ShoppingCartTests.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using HandlingTestEnvironmentsData.Base.Second;
using HandlingTestEnvironmentsData.Data.Second;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Reflection;

namespace HandlingTestEnvironmentsData.Second
{
    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCartFactory _shoppingCartFactory;
        private ShoppingCart _shoppingCart;
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _shoppingCartFactory = new ShoppingCartFactory(_driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Purchase_Book()
        {
            _shoppingCart = _shoppingCartFactory.CreateOldShoppingCart();
            _shoppingCart.PurchaseItem("The Hitchhiker's Guide to the Galaxy", 22.2, new ClientInfo());
        }

        // .NET Core does not support the DataSource attribute. If you try to access test data in this way in a .NET Core or UWP unit test project, 
        // you'll see an error similar to "'TestContext' does not contain a definition for 'DataRow' and no accessible extension method 'DataRow'
        // accepting a first argument of type 'TestContext' could be found (are you missing a using directive or an assembly reference?)".
        ////[TestMethod]
        ////[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "TestsData.csv", "TestsData#csv", DataAccessMethod.Sequential)]
        ////public void Purchase_Book_DataDriven()
        ////{
        ////    string item = TestContext.DataRow["item"];          
        ////    int expectedPrice = int.Parse(this.TestContext.DataRow["itemPrice"]);
        ////    _shoppingCart = _shoppingCartFactory.CreateOldShoppingCart();
        ////    _shoppingCart.PurchaseItem(item, expectedPrice, new ClientInfo());
        ////}
    }
}