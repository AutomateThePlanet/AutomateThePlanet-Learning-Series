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

using TemplateMethodDesignPattern.Data.Second;

namespace TemplateMethodDesignPattern.Base.Second
{
    public abstract class ShoppingCart
    {
        public void PurchaseItem(string item, double itemPrice, ClientInfo clientInfo)
        {
            OpenItem(item);
            AssertPrice(itemPrice);
            ClickBuyNowButton();
            ClickContinueAsGuestButton();
            FillShippingInfo(clientInfo);
            AssertSubtotalAmount(itemPrice);
            ClickContinueButton();
            AssertSubtotal(itemPrice);
        }

        protected abstract void OpenItem(string item);
        protected abstract void AssertPrice(double itemPrice);
        protected abstract void ClickBuyNowButton();
        protected abstract void ClickContinueAsGuestButton();
        protected abstract void FillShippingInfo(ClientInfo clientInfo);
        protected abstract void AssertSubtotalAmount(double itemPrice);
        protected abstract void ClickContinueButton();
        protected abstract void AssertSubtotal(double itemPrice);
    }
}