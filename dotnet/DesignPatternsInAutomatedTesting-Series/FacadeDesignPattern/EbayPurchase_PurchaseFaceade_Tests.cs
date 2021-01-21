﻿// <copyright file="EbayPurchase_PurchaseFaceade_Tests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

using FacadeDesignPattern.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Ebay;

namespace FacadeDesignPattern
{
    [TestClass]
    public class EbayPurchasePurchaseFaceadeTests
    { 
        [TestInitialize]
        public void SetupTest()
        {
            Core.Driver.StartBrowser();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Core.Driver.StopBrowser();
        }

        [TestMethod]
        public void Purchase_Casio_GShock()
        {
            var itemUrl = "Casio-G-Shock-Standard-GA-100-1A2-Mens-Watch-Brand-New-/161209550414?pt=LH_DefaultDomain_15&hash=item2588d6864e";
            var itemPrice = "AU $168.00";
            var currentClientInfo = new ClientInfo()
            {
                FirstName = "Anton",
                LastName = "Angelov",
                Country = "Bulgaria",
                Address1 = "33 Alexander Malinov Blvd.",
                City = "Sofia",
                Zip = "1729",
                Phone = "0035964644885",
                Email = "aangelov@yahoo.com"
            };
            new PurchaseFacade().PurchaseItem(itemUrl, itemPrice, currentClientInfo);
        }

        [TestMethod]
        public void Purchase_WhiteOpticalKeyboard()
        {
            var itemUrl = "Wireless-White-2-4G-Optical-Keyboard-and-Mouse-USB-Receiver-Kit-For-PC-/360649772948?pt=LH_DefaultDomain_2&hash=item53f866cf94";
            var itemPrice = "C $20.86";
            var currentClientInfo = new ClientInfo()
            {
                FirstName = "Anton",
                LastName = "Angelov",
                Country = "Bulgaria",
                Address1 = "33 Alexander Malinov Blvd.",
                City = "Stara Zagora",
                Zip = "6000",
                Phone = "0035964644885",
                Email = "aangelov@yahoo.com"
            };
            new PurchaseFacade().PurchaseItem(itemUrl, itemPrice, currentClientInfo);
        }
    }
}