// <copyright file="ShoppingCart.cs" company="Automate The Planet Ltd.">
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

using TemplateMethodDesignPattern.Data.Second;
using TemplateMethodDesignPattern.Pages.Checkout.Second;
using TemplateMethodDesignPattern.Pages.Item.Second;
using TemplateMethodDesignPattern.Pages.ShippingAddress.Second;
using TemplateMethodDesignPattern.Pages.SignIn.Second;

namespace TemplateMethodDesignPattern.Base.Second;

public class OldShoppingCart : ShoppingCart
{
    private readonly ItemPage _itemPage;

    private readonly SignInPage _signInPage;

    private readonly CheckoutPage _checkoutPage;

    private readonly ShippingAddressPage _shippingAddressPage;

    public OldShoppingCart(ItemPage itemPage, SignInPage signInPage, CheckoutPage checkoutPage, ShippingAddressPage shippingAddressPage)
    {
        _itemPage = itemPage;
        _signInPage = signInPage;
        _checkoutPage = checkoutPage;
        _shippingAddressPage = shippingAddressPage;
    }

    protected override void AssertPrice(double itemPrice) => _itemPage.AssertPrice(itemPrice);
    protected override void AssertSubtotal(double itemPrice) => _checkoutPage.AssertSubtotal(itemPrice);
    protected override void AssertSubtotalAmount(double itemPrice) => _shippingAddressPage.AssertSubtotalAmount(itemPrice);
    protected override void ClickBuyNowButton() => _itemPage.ClickBuyNowButton();
    protected override void ClickContinueAsGuestButton() => _signInPage.ClickContinueAsGuestButton();
    protected override void ClickContinueButton() => _shippingAddressPage.ClickContinueButton();
    protected override void FillShippingInfo(ClientInfo clientInfo) => _shippingAddressPage.FillShippingInfo(clientInfo);
    protected override void OpenItem(string item) => _itemPage.Open(item);
}