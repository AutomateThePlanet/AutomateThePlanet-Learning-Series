// <copyright file="ShoppingCart.cs" company="Automate The Planet Ltd.">
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
using PatternsInAutomatedTests.Conference.Data;
using PatternsInAutomatedTests.Conference.Pages.Checkout;
using PatternsInAutomatedTests.Conference.Pages.ShippingAddress;
using PatternsInAutomatedTests.Conference.Pages.Item;

namespace PatternsInAutomatedTests.Conference.Base
{
    public class ShoppingCart
    {
        private readonly IItemPage itemPage;

        private readonly ISignInPage signInPage;

        private readonly ICheckoutPage checkoutPage;

        private readonly IShippingAddressPage shippingAddressPage;

        public ShoppingCart(IItemPage itemPage, ISignInPage signInPage, ICheckoutPage checkoutPage, IShippingAddressPage shippingAddressPage)
        {
            this.itemPage = itemPage;
            this.signInPage = signInPage;
            this.checkoutPage = checkoutPage;
            this.shippingAddressPage = shippingAddressPage;
        }

        public void PurchaseItem(string item, double itemPrice, ClientInfo clientInfo)
        {
            this.itemPage.Open(item);
            this.itemPage.AssertPrice(itemPrice);
            this.itemPage.ClickBuyNowButton();
            this.signInPage.ClickContinueAsGuestButton();
            this.shippingAddressPage.FillShippingInfo(clientInfo);
            this.shippingAddressPage.AssertSubtotalAmount(itemPrice);
            this.shippingAddressPage.ClickContinueButton();
            this.checkoutPage.AssertSubtotal(itemPrice);
        }
    }
}
