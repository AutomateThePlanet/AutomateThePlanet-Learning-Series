// <copyright file="PurchaseFacade.cs" company="Automate The Planet Ltd.">
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

using System;
using System.Linq;
using FacadeDesignPattern.Data;
using FacadeDesignPattern.Pages.CheckoutPage;
using FacadeDesignPattern.Pages.ItemPage;
using FacadeDesignPattern.Pages.ShippingAddressPage;
using FacadeDesignPattern.Pages.SignInPage;

namespace PatternsInAutomatedTests.Advanced.Ebay
{
    public class PurchaseFacade
    {
        private ItemPage itemPage;
        private CheckoutPage checkoutPage;
        private ShippingAddressPage shippingAddressPage;
        private SignInPage signInPage;

        public ItemPage ItemPage 
        {
            get
            {
                if (this.itemPage == null)
                {
                    this.itemPage = new ItemPage();
                }
                return this.itemPage;
            }
        }

        public SignInPage SignInPage
        {
            get
            {
                if (this.signInPage == null)
                {
                    this.signInPage = new SignInPage();
                }
                return this.signInPage;
            }
        }

        public CheckoutPage CheckoutPage
        {
            get
            {
                if (this.checkoutPage == null)
                {
                    this.checkoutPage = new CheckoutPage();
                }
                return this.checkoutPage;
            }
        }

        public ShippingAddressPage ShippingAddressPage
        {
            get
            {
                if (this.shippingAddressPage == null)
                {
                    this.shippingAddressPage = new ShippingAddressPage();
                }
                return this.shippingAddressPage;
            }
        }

        public void PurchaseItem(string item, string itemPrice, ClientInfo clientInfo)
        {
            this.ItemPage.Navigate(item);
            this.ItemPage.Validate().Price(itemPrice);
            this.ItemPage.ClickBuyNowButton();
            this.SignInPage.ClickContinueAsGuestButton();
            this.ShippingAddressPage.FillShippingInfo(clientInfo);
            this.ShippingAddressPage.Validate().Subtotal(itemPrice);
            this.ShippingAddressPage.ClickContinueButton();
            this.CheckoutPage.Validate().Subtotal(itemPrice);
        }
    }
}