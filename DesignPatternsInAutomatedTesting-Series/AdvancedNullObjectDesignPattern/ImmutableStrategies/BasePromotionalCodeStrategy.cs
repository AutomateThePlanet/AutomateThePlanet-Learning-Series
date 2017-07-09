// <copyright file="BasePromotionalCodeStrategy.cs" company="Automate The Planet Ltd.">
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
namespace AdvancedNullObjectDesignPattern.ImmutableStrategies
{
    using Base;

    public abstract class BasePromotionalCodeStrategy : IPurchasePromotionalCodeStrategy
    {
        private static readonly NullPurchasePromotionalCodeStrategy _nullPurchasePromotionalCodeStrategy = new NullPurchasePromotionalCodeStrategy();

        public static NullPurchasePromotionalCodeStrategy Null
        {
            get
            {
                return _nullPurchasePromotionalCodeStrategy;
            }
        }

        public abstract void AssertPromotionalCodeDiscount();

        public abstract double GetPromotionalCodeDiscountAmount();

        public abstract void ApplyPromotionalCode(string couponCode);

        public class NullPurchasePromotionalCodeStrategy : BasePromotionalCodeStrategy
        {
            public override void AssertPromotionalCodeDiscount()
            {
            }

            public override double GetPromotionalCodeDiscountAmount()
            {
                return 0;
            }

            public override void ApplyPromotionalCode(string couponCode)
            {
            }
        }
    }
}