// <copyright file="RulesEvaluatorTests.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesDesignPattern.Data;

namespace RulesDesignPattern
{
    [TestClass]
    public class RulesEvaluatorTests
    {
        [TestMethod]
        public void TestRules()
        {
            var purchaseTestInput = new PurchaseTestInput()
            {
                IsWiretransfer = false,
                IsPromotionalPurchase = false,
                TotalPrice = 100,
                CreditCardNumber = "378734493671000"
            };
            
            var rulesEvaluator = new RulesEvaluator();

            rulesEvaluator.Eval(new PromotionalPurchaseRule(purchaseTestInput, () => PerformUiAssert()));
            rulesEvaluator.Eval(new CreditCardChargeRule(purchaseTestInput, 20, () => PerformUiAssert()));
            rulesEvaluator.OtherwiseEval(new PromotionalPurchaseRule(purchaseTestInput, () => PerformUiAssert()));
            rulesEvaluator.OtherwiseEval(new CreditCardChargeRule<CreditCardChargeRuleRuleResult>(purchaseTestInput, 30));
            rulesEvaluator.OtherwiseEval(new CreditCardChargeRule<CreditCardChargeRuleAssertResult>(purchaseTestInput, 40));
            rulesEvaluator.OtherwiseEval(new CreditCardChargeRule(purchaseTestInput, 50, () => PerformUiAssert()));
            rulesEvaluator.OtherwiseDo(() => Debug.WriteLine("Perform other UI actions"));          
            
            rulesEvaluator.EvaluateRulesChains();
        }

        [TestMethod]
        public void TestRules_NormalConditions()
        {
            var purchaseTestInput = new PurchaseTestInput()
            {
                IsWiretransfer = false,
                IsPromotionalPurchase = false,
                TotalPrice = 100,
                CreditCardNumber = "378734493671000"
            };
            if (string.IsNullOrEmpty(purchaseTestInput.CreditCardNumber) &&
                !purchaseTestInput.IsWiretransfer &&
                purchaseTestInput.IsPromotionalPurchase &&
                purchaseTestInput.TotalPrice == 0)
            {
                PerformUiAssert("Assert volume discount promotion amount. + additional UI actions");
            }
            if (!string.IsNullOrEmpty(purchaseTestInput.CreditCardNumber) &&
                !purchaseTestInput.IsWiretransfer &&
                !purchaseTestInput.IsPromotionalPurchase &&
                purchaseTestInput.TotalPrice > 20)
            {
                PerformUiAssert("Assert that total amount label is over 20$ + additional UI actions");
            }
            else if (!string.IsNullOrEmpty(purchaseTestInput.CreditCardNumber) &&
                     !purchaseTestInput.IsWiretransfer &&
                     !purchaseTestInput.IsPromotionalPurchase &&
                     purchaseTestInput.TotalPrice > 30)
            {
                Console.WriteLine("Assert that total amount label is over 30$ + additional UI actions");
            }
            else if (!string.IsNullOrEmpty(purchaseTestInput.CreditCardNumber) &&
                     !purchaseTestInput.IsWiretransfer &&
                     !purchaseTestInput.IsPromotionalPurchase &&
                     purchaseTestInput.TotalPrice > 40)
            {
                Console.WriteLine("Assert that total amount label is over 40$ + additional UI actions");
            }
            else if (!string.IsNullOrEmpty(purchaseTestInput.CreditCardNumber) &&
                     !purchaseTestInput.IsWiretransfer &&
                     !purchaseTestInput.IsPromotionalPurchase &&
                     purchaseTestInput.TotalPrice > 50)
            {
                PerformUiAssert("Assert that total amount label is over 50$ + additional UI actions");
            }
            else
            {
                Debug.WriteLine("Perform other UI actions");
            }
        }

        private void PerformUiAssert(string text = "Perform other UI actions")
        {
            Debug.WriteLine(text);
        }
    }
}