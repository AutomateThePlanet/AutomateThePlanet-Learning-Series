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

using StrategyDesignPattern.Data;
using StrategyDesignPattern.Pages.ItemPage;
using StrategyDesignPattern.Pages.PreviewShoppingCartPage;
using StrategyDesignPattern.Pages.ShippingAddressPage;
using StrategyDesignPattern.Pages.ShippingPaymentPage;
using StrategyDesignPattern.Pages.SignInPage;

namespace StrategyDesignPattern.Base
{
    public class PurchaseContext
    {
        private readonly IOrderValidationStrategy _orderValidationStrategy;

        public PurchaseContext(IOrderValidationStrategy orderValidationStrategy)
        {
            _orderValidationStrategy = orderValidationStrategy;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            ItemPage.Instance.Navigate(itemUrl);
            ItemPage.Instance.ClickBuyNowButton();
            PreviewShoppingCartPage.Instance.ClickProceedToCheckoutButton();
            SignInPage.Instance.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            ShippingAddressPage.Instance.FillShippingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickBottomContinueButton();
            ShippingPaymentPage.Instance.ClickTopContinueButton();
            _orderValidationStrategy.ValidateOrderSummary(itemPrice, clientPurchaseInfo);
        }
    }
}