// <copyright file="NoTaxesOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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

using System;
using AdvancedStrategyDesignPattern.Base;
using AdvancedStrategyDesignPattern.Data;
using AdvancedStrategyDesignPattern.Enums;
using AdvancedStrategyDesignPattern.Pages.PlaceOrderPage;

namespace AdvancedStrategyDesignPattern.Strategies;

public class NoTaxesOrderPurchaseStrategy : IOrderPurchaseStrategy
{
    public NoTaxesOrderPurchaseStrategy(bool shouldExecute)
    {
    }

    public void AssertOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
    {
        PlaceOrderPage.Instance.Validate().EstimatedTaxPrice("0.00");
    }

    public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
    {
        if (clientPurchaseInfo.ShippingInfo.Country.Equals(Countries.UnitedStates))
        {
            throw new ArgumentException("If the NoTaxesOrderPurchaseStrategy is used, the country cannot be set to United States because a sales tax is going to be applied.");
        }

        // Add another validation for the EU contries because for them a VAT Tax is going to be applied if not VAT ID is set.
    }
}