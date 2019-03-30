// <copyright file="PurchaseContext.cs" company="Automate The Planet Ltd.">
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

using DecoratorDesignPattern.Pages.ItemPage;
using DecoratorDesignPattern.Pages.PreviewShoppingCartPage;
using DecoratorDesignPattern.Pages.ShippingAddressPage;
using DecoratorDesignPattern.Pages.ShippingPaymentPage;
using DecoratorDesignPattern.Pages.SignInPage;

namespace DecoratorDesignPattern.Advanced.Base
{
    public class PurchaseContext
    {
        private readonly Strategies.OrderPurchaseStrategy _orderPurchaseStrategy;

        public PurchaseContext(Strategies.OrderPurchaseStrategy orderPurchaseStrategy)
        {
            _orderPurchaseStrategy = orderPurchaseStrategy;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, Data.ClientLoginInfo clientLoginInfo, Data.ClientPurchaseInfo clientPurchaseInfo)
        {
            ItemPage.Instance.Navigate(itemUrl);
            ItemPage.Instance.ClickBuyNowButton();
            PreviewShoppingCartPage.Instance.ClickProceedToCheckoutButton();
            SignInPage.Instance.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            ShippingAddressPage.Instance.FillShippingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickDifferentBillingCheckBox(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickBottomContinueButton();
            ShippingAddressPage.Instance.FillBillingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickTopContinueButton();
            var expectedTotalPrice = _orderPurchaseStrategy.CalculateTotalPrice();
            _orderPurchaseStrategy.ValidateOrderSummary(expectedTotalPrice);
        }
    }
}