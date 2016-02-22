namespace PatternsInAutomation.Tests.Advanced.Specifications.Data
{
    public class PurchaseTestInput
    {
        public bool IsWiretransfer { get; set; }

        public bool IsPromotionalPurchase { get; set; }

        public string CreditCardNumber { get; set; }

        public decimal TotalPrice { get; set; }
    }
}