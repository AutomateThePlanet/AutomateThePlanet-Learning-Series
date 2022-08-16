// <copyright file="SalesTaxCalculationService.cs" company="Automate The Planet Ltd.">
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

using StrategyDesignPattern.Enums;

namespace StrategyDesignPattern.Services;

public class SalesTaxCalculationService
{
    public decimal Calculate(decimal price, States state, string zip)
    {
        var taxPrice = default(decimal);

        // Call Real Web Service to determine the Sales Tax.
        switch (state)
        {
            case States.Arizona:
                taxPrice = CalculateTaxPriceInternal(price, 7.125, zip);
                break;
            case States.Illinois:
                taxPrice = CalculateTaxPriceInternal(price, 3.75, zip);
                break;
            case States.Massachusetts:
                taxPrice = CalculateTaxPriceInternal(price, 6.25, zip);
                break;
            case States.California:
                taxPrice = CalculateTaxPriceInternal(price, 2.50, zip);
                break;
            case States.Washington:
                taxPrice = CalculateTaxPriceInternal(price, 3.10, zip);
                break;
            case States.NewJersey:
                taxPrice = CalculateTaxPriceInternal(price, 7.00, zip);
                break;
            case States.Texas:
                taxPrice = CalculateTaxPriceInternal(price, 8.15, zip);
                break;
            default:
                taxPrice = 0;
                break;
        }

        return taxPrice;
    }

    private static decimal CalculateTaxPriceInternal(decimal price, double percent, string zip)
    {
        var taxPrice = price / (decimal)percent;
        return taxPrice;
    }
}