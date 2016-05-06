// <copyright file="LinqPlaceOrderPage.cs" company="Automate The Planet Ltd.">
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
using AdvancedSpecificationDesignPattern.Base;
using AdvancedSpecificationDesignPattern.Data;
using AdvancedSpecificationDesignPattern.Specifications;
using AdvancedSpecificationDesignPattern.Specifications.Core;
using OpenQA.Selenium;

namespace AdvancedSpecificationDesignPattern.Pages.LinqPlaceOrderPage
{
    public partial class LinqPlaceOrderPage : BasePage
    {
        private readonly PurchaseTestInput purchaseTestInput;
        private readonly ISpecification<PurchaseTestInput> promotionalPurchaseSpecification;
        private readonly ISpecification<PurchaseTestInput> creditCardSpecification;
        private readonly ISpecification<PurchaseTestInput> wiretransferSpecification;
        private readonly ISpecification<PurchaseTestInput> freePurchaseSpecification;

        public LinqPlaceOrderPage(IWebDriver driver, PurchaseTestInput purchaseTestInput) : base(driver)
        {
            this.purchaseTestInput = purchaseTestInput;
            this.creditCardSpecification = new ExpressionSpecification<PurchaseTestInput>(x => !string.IsNullOrEmpty(x.CreditCardNumber));
            this.freePurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.TotalPrice == 0);
            this.wiretransferSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsWiretransfer);
            this.promotionalPurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsPromotionalPurchase && x.TotalPrice < 5);
        }

        public override string Url
        {
            get
            {
                return @"http://www.bing.com/";
            }
        }

        public void ChoosePaymentMethod()
        {
            if (this.creditCardSpecification.
            And(this.wiretransferSpecification.Not()).
            And(this.freePurchaseSpecification.Not()).
            And(this.promotionalPurchaseSpecification.Not()).
            IsSatisfiedBy(this.purchaseTestInput))
            {
                this.CreditCard.SendKeys("371449635398431");
                this.SecurityNumber.SendKeys("1234");
            }
            else
            {
                this.Wiretransfer.SendKeys("pathToFile");
            }
        }
    }
}