using System;

namespace TestingTechniques.UnitTests
{
    public static class TransportSubscriptionCardPriceCalculator
    {
        /*
         * >= 5 years - free
         * 5 >= 18 years - 20 lv
         * > 18 - 40 lv
         * >= 65 - 5 lv
         */

        public static decimal CalculateSubscriptionPrice(string ageInput)
        {
            decimal subscriptionPrice = default(decimal);
            int age = default(int);
            bool isInteger = int.TryParse(ageInput, out age);

            if (!isInteger)
            {
                throw new ArgumentException("The age input should be an integer value between 0 - 122.");    
            }

            if (age <= 0)
            {
                throw new ArgumentException("The age should be greater than zero."); 
            }
            else if (age > 0 && age <= 5)
            {
                subscriptionPrice = 0;
            }
            else if (age > 5 && age <= 18)
            {
                subscriptionPrice = 20;
            }
            else if (age > 18 && age < 65)
            {
                subscriptionPrice = 40;
            }
            else if (age >= 65 && age <= 122)
            {
                subscriptionPrice = 5;
            }
            else
            {
                throw new ArgumentException("The age should be smaller than 123."); 
            }

            return subscriptionPrice;
        }
    }
}
