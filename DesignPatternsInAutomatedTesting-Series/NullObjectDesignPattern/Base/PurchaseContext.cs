// <copyright file="PurchaseContext.cs" company="Automate The Planet Ltd.">
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

using NullObjectDesignPattern.Data;
using NullObjectDesignPattern.Pages.ItemPage;
using NullObjectDesignPattern.Pages.PlaceOrderPage;
using NullObjectDesignPattern.Pages.PreviewShoppingCartPage;
using NullObjectDesignPattern.Pages.ShippingAddressPage;
using NullObjectDesignPattern.Pages.ShippingPaymentPage;
using NullObjectDesignPattern.Pages.SignInPage;

namespace NullObjectDesignPattern.Base
{
    public class PurchaseContext
    {
        private readonly IPurchasePromotionalCodeStrategy purchasePromotionalCodeStrategy;
        private readonly ItemPage itemPage;
        private readonly PreviewShoppingCartPage previewShoppingCartPage;
        private readonly SignInPage signInPage;
        private readonly ShippingAddressPage shippingAddressPage;
        private readonly ShippingPaymentPage shippingPaymentPage;
        private readonly PlaceOrderPage placeOrderPage;

        public PurchaseContext(
            IPurchasePromotionalCodeStrategy purchasePromotionalCodeStrategy,
            ItemPage itemPage,
            PreviewShoppingCartPage previewShoppingCartPage,
            SignInPage signInPage,
            ShippingAddressPage shippingAddressPage,
            ShippingPaymentPage shippingPaymentPage,
            PlaceOrderPage placeOrderPage)
        {
            this.purchasePromotionalCodeStrategy = purchasePromotionalCodeStrategy;
            this.itemPage = itemPage;
            this.previewShoppingCartPage = previewShoppingCartPage;
            this.signInPage = signInPage;
            this.shippingAddressPage = shippingAddressPage;
            this.shippingPaymentPage = shippingPaymentPage;
            this.placeOrderPage = placeOrderPage;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            this.itemPage.Navigate(itemUrl);
            this.itemPage.ClickBuyNowButton();
            this.previewShoppingCartPage.ClickProceedToCheckoutButton();
            this.signInPage.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            this.shippingAddressPage.FillShippingInfo(clientPurchaseInfo);
            this.shippingAddressPage.ClickDifferentBillingCheckBox(clientPurchaseInfo);
            this.shippingAddressPage.ClickContinueButton();
            this.shippingPaymentPage.ClickBottomContinueButton();
            this.shippingAddressPage.FillBillingInfo(clientPurchaseInfo);
            this.shippingAddressPage.ClickContinueButton();
            this.shippingPaymentPage.ClickTopContinueButton();
            this.purchasePromotionalCodeStrategy.AssertPromotionalCodeDiscount();
            double couponDiscount = this.purchasePromotionalCodeStrategy.GetPromotionalCodeDiscountAmount();
            double totalPrice = double.Parse(itemPrice);
            this.placeOrderPage.AssertOrderTotalPrice(totalPrice, couponDiscount);
            this.purchasePromotionalCodeStrategy.AssertPromotionalCodeDiscount();
        }
    }
}