// <copyright file="PlaceOrderPage.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Specifications.Base;
using PatternsInAutomatedTests.Advanced.Specifications.Data;

namespace PatternsInAutomatedTests.Advanced.Specifications
{
    public partial class PlaceOrderPage : BasePage
    {
        private readonly PurchaseTestInput purchaseTestInput;
        private readonly PromotionalPurchaseSpecification promotionalPurchaseSpecification;
        private readonly CreditCardSpecification creditCardSpecification;
        private readonly WiretransferSpecification wiretransferSpecification;
        private readonly FreePurchaseSpecification freePurchaseSpecification;

        public PlaceOrderPage(IWebDriver driver, PurchaseTestInput purchaseTestInput) : base(driver)
        {
            this.purchaseTestInput = purchaseTestInput;
            this.promotionalPurchaseSpecification = new PromotionalPurchaseSpecification(purchaseTestInput);
            this.wiretransferSpecification = new WiretransferSpecification(purchaseTestInput);
            this.creditCardSpecification = new CreditCardSpecification(purchaseTestInput);
            this.freePurchaseSpecification = new FreePurchaseSpecification();
            this.IsPromoCodePurchase = this.freePurchaseSpecification.Or(this.promotionalPurchaseSpecification).IsSatisfiedBy(this.purchaseTestInput);
            this.IsCreditCardPurchase = this.creditCardSpecification.
            And(this.wiretransferSpecification.Not()).
            And(this.freePurchaseSpecification.Not()).
            And(this.promotionalPurchaseSpecification.Not()).
            IsSatisfiedBy(this.purchaseTestInput);
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
            if (this.IsCreditCardPurchase)
            {
                this.CreditCard.SendKeys("371449635398431");
                this.SecurityNumber.SendKeys("1234");
            }
            else
            {
                this.Wiretransfer.SendKeys("pathToFile");
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
            if (this.IsPromoCodePurchase)
            {
                this.PromotionalCode.SendKeys(promoCode);
            }
        }
    }
}