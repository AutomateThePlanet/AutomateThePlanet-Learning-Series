using PatternsInAutomation.Tests.Advanced.Decorator.Enums;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Services
{
    public class GiftWrappingPriceCalculationService
    {
        public decimal Calculate(GiftWrappingStyles giftWrappingStyle)
        {
            decimal giftWrappingPrice = default(decimal);

            // Call Real Web Service to determine the Gift Wrapping Tax.
            switch (giftWrappingStyle)
            {
                case GiftWrappingStyles.Fancy:
                    giftWrappingPrice = 10.5M;
                    break;
                case GiftWrappingStyles.Cheap:
                    giftWrappingPrice = 1.5M;
                    break;
                case GiftWrappingStyles.UltraFancy:
                    giftWrappingPrice = 30.2M;
                    break;
                case GiftWrappingStyles.Paper:
                    giftWrappingPrice = 0.2M;
                    break;
                case GiftWrappingStyles.None:
                default:
                    giftWrappingPrice = 0.0M;
                    break;
            }

            return giftWrappingPrice;
        }
    }
}
