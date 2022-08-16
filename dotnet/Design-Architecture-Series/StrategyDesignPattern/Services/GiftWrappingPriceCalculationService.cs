// <copyright file="GiftWrappingPriceCalculationService.cs" company="Automate The Planet Ltd.">
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

public class GiftWrappingPriceCalculationService
{
    public decimal Calculate(GiftWrappingStyles giftWrappingStyle)
    {
        var giftWrappingPrice = default(decimal);

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