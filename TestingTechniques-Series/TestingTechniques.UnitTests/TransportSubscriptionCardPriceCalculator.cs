// <copyright file="TransportSubscriptionCardPriceCalculator.cs" company="Automate The Planet Ltd.">
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
