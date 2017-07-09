// <copyright file="CreditCardChargeRule.cs" company="Automate The Planet Ltd.">
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
using RulesDesignPattern.Data;

namespace RulesDesignPattern
{
    public class CreditCardChargeRule : BaseRule
    {
        private readonly PurchaseTestInput _purchaseTestInput;
        private readonly decimal _totalPriceLowerBoundary;

        public CreditCardChargeRule(PurchaseTestInput purchaseTestInput, decimal totalPriceLowerBoundary, Action actionToBeExecuted) : base(actionToBeExecuted)
        {
            _purchaseTestInput = purchaseTestInput;
            _totalPriceLowerBoundary = totalPriceLowerBoundary;
        }
        
        public override IRuleResult Eval()
        {
            if (!string.IsNullOrEmpty(_purchaseTestInput.CreditCardNumber) &&
                !_purchaseTestInput.IsWiretransfer &&
                !_purchaseTestInput.IsPromotionalPurchase &&
                _purchaseTestInput.TotalPrice > _totalPriceLowerBoundary)
            {
                RuleResult.IsSuccess = true;
                return RuleResult;
            }
            return new RuleResult();
        }
    }
}