// <copyright file="PurchaseFacade.cs" company="Automate The Planet Ltd.">
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

using System;
using System.Linq;
using FacadeDesignPattern.Data;
using FacadeDesignPattern.Pages.CheckoutPage;
using FacadeDesignPattern.Pages.ItemPage;
using FacadeDesignPattern.Pages.ShippingAddressPage;
using FacadeDesignPattern.Pages.SignInPage;

namespace PatternsInAutomatedTests.Advanced.Ebay;

public class PurchaseFacade
{
    private ItemPage _itemPage;
    private CheckoutPage _checkoutPage;
    private ShippingAddressPage _shippingAddressPage;
    private SignInPage _signInPage;

    public ItemPage ItemPage
    {
        get
        {
            if (_itemPage == null)
            {
                _itemPage = new ItemPage();
            }

            return _itemPage;
        }
    }

    public SignInPage SignInPage
    {
        get
        {
            if (_signInPage == null)
            {
                _signInPage = new SignInPage();
            }

            return _signInPage;
        }
    }

    public CheckoutPage CheckoutPage
    {
        get
        {
            if (_checkoutPage == null)
            {
                _checkoutPage = new CheckoutPage();
            }

            return _checkoutPage;
        }
    }

    public ShippingAddressPage ShippingAddressPage
    {
        get
        {
            if (_shippingAddressPage == null)
            {
                _shippingAddressPage = new ShippingAddressPage();
            }

            return _shippingAddressPage;
        }
    }

    public void PurchaseItem(string item, string itemPrice, ClientInfo clientInfo)
    {
        ItemPage.Navigate(item);
        ItemPage.Validate().Price(itemPrice);
        ItemPage.ClickBuyNowButton();
        SignInPage.ClickContinueAsGuestButton();
        ShippingAddressPage.FillShippingInfo(clientInfo);
        ShippingAddressPage.Validate().Subtotal(itemPrice);
        ShippingAddressPage.ClickContinueButton();
        CheckoutPage.Validate().Subtotal(itemPrice);
    }
}