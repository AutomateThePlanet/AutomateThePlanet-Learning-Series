using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Rules.Data;
using System;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    [TestClass]
    public class RulesEvaluatorTests
    {
        [TestMethod]
        public void TestRules()
        {
            PurchaseTestInput purchaseTestInput = new PurchaseTestInput()
            {
                IsWiretransfer = false,
                IsPromotionalPurchase = false,
                TotalPrice = 100,
                CreditCardNumber = "378734493671000"
            };
            
            RulesEvaluator rulesEvaluator = new RulesEvaluator();

            rulesEvaluator.Eval(new PromotionalPurchaseRule(purchaseTestInput, () => this.PerformUIAssert()));
            rulesEvaluator.Eval(new CreditCardChargeRule(purchaseTestInput, 20, () => this.PerformUIAssert()));
            rulesEvaluator.OtherwiseEval(new PromotionalPurchaseRule(purchaseTestInput, () => this.PerformUIAssert()));
            rulesEvaluator.OtherwiseEval(new CreditCardChargeRule<CreditCardChargeRuleRuleResult>(purchaseTestInput, 30));
            rulesEvaluator.OtherwiseEval(new CreditCardChargeRule<CreditCardChargeRuleAssertResult>(purchaseTestInput, 40));
            rulesEvaluator.OtherwiseEval(new CreditCardChargeRule(purchaseTestInput, 50, () => this.PerformUIAssert()));
            rulesEvaluator.OtherwiseDo(() => Debug.WriteLine("Perform other UI actions"));          
            
            rulesEvaluator.EvaluateRulesChains();
        }

        [TestMethod]
        public void TestRules_NormalConditions()
        {
            PurchaseTestInput purchaseTestInput = new PurchaseTestInput()
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
                this.PerformUIAssert("Assert volume discount promotion amount. + additional UI actions");
            }
            if (!string.IsNullOrEmpty(purchaseTestInput.CreditCardNumber) &&
                !purchaseTestInput.IsWiretransfer &&
                !purchaseTestInput.IsPromotionalPurchase &&
                purchaseTestInput.TotalPrice > 20)
            {
                this.PerformUIAssert("Assert that total amount label is over 20$ + additional UI actions");
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
                this.PerformUIAssert("Assert that total amount label is over 50$ + additional UI actions");
            }
            else
            {
                Debug.WriteLine("Perform other UI actions");
            }
        }

        private void PerformUIAssert(string text = "Perform other UI actions")
        {
            Debug.WriteLine(text);
        }
    }
}