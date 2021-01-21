// <copyright file="VatTaxCalculationService.cs" company="Automate The Planet Ltd.">
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

using AdvancedStrategyDesignPattern.Enums;

namespace PatternsInAutomatedTests.Advanced.Strategy
{
    public class VatTaxCalculationService
    {
        public decimal Calculate(decimal price, Countries country)
        {
            var taxPrice = default(decimal);

            // Call Real Web Service to determine the VAT Tax.
            switch (country)
            {
                case Countries.Bulgaria:
                    taxPrice = CalculateTaxPriceInternal(price, 20);
                    break;
                case Countries.Germany:
                    taxPrice = CalculateTaxPriceInternal(price, 19);
                    break;
                case Countries.Austria:
                    taxPrice = CalculateTaxPriceInternal(price, 20);
                    break;
                case Countries.France:
                    taxPrice = CalculateTaxPriceInternal(price, 23);
                    break;
                default:
                    taxPrice = 0;
                    break;
            }

            return taxPrice;
        }

        private static decimal CalculateTaxPriceInternal(decimal price, double percent)
        {
            var taxPrice = price / (decimal)percent;
            return taxPrice;
        }
    }
}