// <copyright file="ShoppingCartFactory.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

using HandlingTestEnvironmentsData.Pages.Checkout.Second;
using HandlingTestEnvironmentsData.Pages.Item.Second;
using HandlingTestEnvironmentsData.Pages.ShippingAddress.Second;
using HandlingTestEnvironmentsData.Pages.SignIn.Second;
using OpenQA.Selenium;

namespace HandlingTestEnvironmentsData.Base.Second
{
    public class ShoppingCartFactory
    {
        private readonly IWebDriver _driver;

        public ShoppingCartFactory(IWebDriver driver) => _driver = driver;

        public ShoppingCart CreateOldShoppingCart()
        {
            var itemPage = new ItemPage(_driver);
            var signInPage = new SignInPage(_driver);
            var checkoutPage = new CheckoutPage(_driver);
            var shippingAddressPage = new ShippingAddressPage(_driver);
            var oldShoppingCart = new OldShoppingCart(itemPage, signInPage, checkoutPage, shippingAddressPage);
            return oldShoppingCart;
        }

        public ShoppingCart CreateNewShoppingCart()
        {
            var itemPage = new ItemPage(_driver);
            var signInPage = new SignInPage(_driver);
            var checkoutPage = new CheckoutPage(_driver);
            var shippingAddressPage = new ShippingAddressPage(_driver);
            var oldShoppingCart = new OldShoppingCart(itemPage, signInPage, checkoutPage, shippingAddressPage);
            return oldShoppingCart;
        }
    }
}