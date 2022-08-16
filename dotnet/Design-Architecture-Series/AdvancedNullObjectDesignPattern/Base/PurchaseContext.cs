// <copyright file="PurchaseContext.cs" company="Automate The Planet Ltd.">
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

using AdvancedNullObjectDesignPattern.Data;
using AdvancedNullObjectDesignPattern.Pages.ItemPage;
using AdvancedNullObjectDesignPattern.Pages.PlaceOrderPage;
using AdvancedNullObjectDesignPattern.Pages.PreviewShoppingCartPage;
using AdvancedNullObjectDesignPattern.Pages.ShippingAddressPage;
using AdvancedNullObjectDesignPattern.Pages.ShippingPaymentPage;
using AdvancedNullObjectDesignPattern.Pages.SignInPage;

namespace AdvancedNullObjectDesignPattern.Base;

public class PurchaseContext
{
    private readonly IPurchasePromotionalCodeStrategy _purchasePromotionalCodeStrategy;
    private readonly ItemPage _itemPage;
    private readonly PreviewShoppingCartPage _previewShoppingCartPage;
    private readonly SignInPage _signInPage;
    private readonly ShippingAddressPage _shippingAddressPage;
    private readonly ShippingPaymentPage _shippingPaymentPage;
    private readonly PlaceOrderPage _placeOrderPage;

    public PurchaseContext(
        IPurchasePromotionalCodeStrategy purchasePromotionalCodeStrategy,
        ItemPage itemPage,
        PreviewShoppingCartPage previewShoppingCartPage,
        SignInPage signInPage,
        ShippingAddressPage shippingAddressPage,
        ShippingPaymentPage shippingPaymentPage,
        PlaceOrderPage placeOrderPage)
    {
        _purchasePromotionalCodeStrategy = purchasePromotionalCodeStrategy;
        _itemPage = itemPage;
        _previewShoppingCartPage = previewShoppingCartPage;
        _signInPage = signInPage;
        _shippingAddressPage = shippingAddressPage;
        _shippingPaymentPage = shippingPaymentPage;
        _placeOrderPage = placeOrderPage;
    }

    public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
    {
        _itemPage.Navigate(itemUrl);
        _itemPage.ClickBuyNowButton();
        _previewShoppingCartPage.ClickProceedToCheckoutButton();
        _signInPage.Login(clientLoginInfo.Email, clientLoginInfo.Password);
        _shippingAddressPage.FillShippingInfo(clientPurchaseInfo);
        _shippingAddressPage.ClickDifferentBillingCheckBox(clientPurchaseInfo);
        _shippingAddressPage.ClickContinueButton();
        _shippingPaymentPage.ClickBottomContinueButton();
        _shippingAddressPage.FillBillingInfo(clientPurchaseInfo);
        _shippingAddressPage.ClickContinueButton();
        _shippingPaymentPage.ClickTopContinueButton();
        _purchasePromotionalCodeStrategy.AssertPromotionalCodeDiscount();
        var couponDiscount = _purchasePromotionalCodeStrategy.GetPromotionalCodeDiscountAmount();
        var totalPrice = double.Parse(itemPrice);
        _placeOrderPage.AssertOrderTotalPrice(totalPrice, couponDiscount);
        _purchasePromotionalCodeStrategy.AssertPromotionalCodeDiscount();
    }
}