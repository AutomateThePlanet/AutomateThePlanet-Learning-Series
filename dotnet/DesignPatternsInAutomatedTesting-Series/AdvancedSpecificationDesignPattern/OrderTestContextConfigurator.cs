﻿// <copyright file="OrderTestContextConfigurator.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
// <site>http://automatetheplanet.com/</site>u

using AdvancedSpecificationDesignPattern.Data;
using AdvancedSpecificationDesignPattern.Specifications;
using AdvancedSpecificationDesignPattern.Specifications.Core;

namespace AdvancedSpecificationDesignPattern
{
    public class OrderTestContextConfigurator
    {
        public OrderTestContextConfigurator()
        {
            CreditCardSpecification = new ExpressionSpecification<PurchaseTestInput>(x => !string.IsNullOrEmpty(x.CreditCardNumber));
            FreePurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.TotalPrice == 0);
            WiretransferSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsWiretransfer);
            PromotionalPurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsPromotionalPurchase && x.TotalPrice < 5);
        }

        public ISpecification<PurchaseTestInput> PromotionalPurchaseSpecification { get; private set; }

        public ISpecification<PurchaseTestInput> CreditCardSpecification { get; private set; }

        public ISpecification<PurchaseTestInput> WiretransferSpecification { get; private set; }

        public ISpecification<PurchaseTestInput> FreePurchaseSpecification { get; private set; }
    }
}