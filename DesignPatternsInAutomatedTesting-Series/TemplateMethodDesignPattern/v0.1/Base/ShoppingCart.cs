// <copyright file="ShoppingCart.cs" company="Automate The Planet Ltd.">
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

using TemplateMethodDesignPattern.Data.First;
using TemplateMethodDesignPattern.Pages.Checkout.First;
using TemplateMethodDesignPattern.Pages.Item.First;
using TemplateMethodDesignPattern.Pages.ShippingAddress.First;
using TemplateMethodDesignPattern.Pages.SignIn.First;

namespace TemplateMethodDesignPattern.Base.First
{
    public class ShoppingCart
    {
        private readonly IItemPage _itemPage;

        private readonly ISignInPage _signInPage;

        private readonly ICheckoutPage _checkoutPage;

        private readonly IShippingAddressPage _shippingAddressPage;

        public ShoppingCart(IItemPage itemPage, ISignInPage signInPage, ICheckoutPage checkoutPage, IShippingAddressPage shippingAddressPage)
        {
            _itemPage = itemPage;
            _signInPage = signInPage;
            _checkoutPage = checkoutPage;
            _shippingAddressPage = shippingAddressPage;
        }

        public void PurchaseItem(string item, double itemPrice, ClientInfo clientInfo)
        {
            _itemPage.Open(item);
            _itemPage.AssertPrice(itemPrice);
            _itemPage.ClickBuyNowButton();
            _signInPage.ClickContinueAsGuestButton();
            _shippingAddressPage.FillShippingInfo(clientInfo);
            _shippingAddressPage.AssertSubtotalAmount(itemPrice);
            _shippingAddressPage.ClickContinueButton();
            _checkoutPage.AssertSubtotal(itemPrice);
        }
    }
}