// <copyright file="PlaceOrderPage.cs" company="Automate The Planet Ltd.">
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

using AdvancedSpecificationDesignPattern.Base;
using AdvancedSpecificationDesignPattern.Data;
using AdvancedSpecificationDesignPattern.Specifications;
using OpenQA.Selenium;

namespace AdvancedSpecificationDesignPattern.Pages.PlaceOrderPage;

public partial class PlaceOrderPage : BasePage
{
    private readonly PurchaseTestInput _purchaseTestInput;
    private readonly PromotionalPurchaseSpecification _promotionalPurchaseSpecification;
    private readonly CreditCardSpecification _creditCardSpecification;
    private readonly WiretransferSpecification _wiretransferSpecification;
    private readonly FreePurchaseSpecification _freePurchaseSpecification;

    public PlaceOrderPage(IWebDriver driver, PurchaseTestInput purchaseTestInput) : base(driver)
    {
        _purchaseTestInput = purchaseTestInput;
        _promotionalPurchaseSpecification = new PromotionalPurchaseSpecification(purchaseTestInput);
        _wiretransferSpecification = new WiretransferSpecification(purchaseTestInput);
        _creditCardSpecification = new CreditCardSpecification(purchaseTestInput);
        _freePurchaseSpecification = new FreePurchaseSpecification();
        IsPromoCodePurchase = _freePurchaseSpecification.Or(_promotionalPurchaseSpecification).IsSatisfiedBy(_purchaseTestInput);
        IsCreditCardPurchase = _creditCardSpecification.
        And(_wiretransferSpecification.Not()).
        And(_freePurchaseSpecification.Not()).
        And(_promotionalPurchaseSpecification.Not()).
        IsSatisfiedBy(_purchaseTestInput);
    }

    public bool IsPromoCodePurchase { get; private set; }

    public bool IsCreditCardPurchase { get; private set; }

    public override string Url
    {
        get
        {
            return @"http://www.bing.com/";
        }
    }

    ////public void ChoosePaymentMethod()
    ////{
    ////    if (!string.IsNullOrEmpty(this.purchaseTestInput.CreditCardNumber)
    ////        && !this.purchaseTestInput.IsWiretransfer
    ////        && !(this.purchaseTestInput.IsPromotionalPurchase && this.purchaseTestInput.TotalPrice < 5)
    ////        && !(this.purchaseTestInput.TotalPrice == 0))
    ////    {
    ////        this.CreditCard.SendKeys("371449635398431");
    ////        this.SecurityNumber.SendKeys("1234");
    ////    }
    ////    else
    ////    {
    ////        this.Wiretransfer.SendKeys("pathToFile");
    ////    }
    ////}
    public void ChoosePaymentMethod()
    {
        if (IsCreditCardPurchase)
        {
            CreditCard.SendKeys("371449635398431");
            SecurityNumber.SendKeys("1234");
        }
        else
        {
            Wiretransfer.SendKeys("pathToFile");
        }
    }

    ////public void ChoosePaymentMethod()
    ////{
    ////    if (this.creditCardSpecification.
    ////    And(this.wiretransferSpecification.Not()).
    ////    And(this.freePurchaseSpecification.Not()).
    ////    And(this.promotionalPurchaseSpecification.Not()).
    ////    IsSatisfiedBy(this.purchaseTestInput))
    ////    {
    ////        this.CreditCard.SendKeys("371449635398431");
    ////        this.SecurityNumber.SendKeys("1234");
    ////    }
    ////    else
    ////    {
    ////        this.Wiretransfer.SendKeys("pathToFile");
    ////    }
    ////}
    public void TypePromotionalCode(string promoCode)
    {
        if (IsPromoCodePurchase)
        {
            PromotionalCode.SendKeys(promoCode);
        }
    }
}