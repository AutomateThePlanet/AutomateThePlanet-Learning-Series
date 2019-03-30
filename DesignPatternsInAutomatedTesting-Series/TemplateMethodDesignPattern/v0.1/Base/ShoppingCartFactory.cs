// <copyright file="ShoppingCartFactory.cs" company="Automate The Planet Ltd.">
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

using TemplateMethodDesignPattern.Pages.Checkout.First;
using TemplateMethodDesignPattern.Pages.Item.First;
using TemplateMethodDesignPattern.Pages.ShippingAddress.First;
using TemplateMethodDesignPattern.Pages.SignIn.First;
using OpenQA.Selenium;

namespace TemplateMethodDesignPattern.Base.First
{
    public class ShoppingCartFactory : IFactory<ShoppingCart>
    {
        private readonly IWebDriver _driver;

        public ShoppingCartFactory(IWebDriver driver)
        {
            _driver = driver;
        }

        public ShoppingCart Create()
        {
            var itemPage = new ItemPage(_driver);
            var signInPage = new SignInPage(_driver);
            var checkoutPage = new CheckoutPage(_driver);
            var shippingAddressPage = new ShippingAddressPage(_driver);
            var purchaseFacade = new ShoppingCart(itemPage, signInPage, checkoutPage, shippingAddressPage);
            return purchaseFacade;
        }
    }
}