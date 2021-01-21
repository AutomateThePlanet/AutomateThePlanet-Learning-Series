﻿// <copyright file="NullPurchasePromotionalCodeStrategy.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using System;
using AdvancedNullObjectDesignPattern.Base;

namespace AdvancedNullObjectDesignPattern.SingletonStrategies
{
    public class NullPurchasePromotionalCodeStrategy : IPurchasePromotionalCodeStrategy
    {
        private static NullPurchasePromotionalCodeStrategy _instance;

        public static NullPurchasePromotionalCodeStrategy Null
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NullPurchasePromotionalCodeStrategy();
                }
                return _instance;
            }
        }

        public void AssertPromotionalCodeDiscount()
        {
        }

        public double GetPromotionalCodeDiscountAmount()
        {
            return 0;
        }

        public void ApplyPromotionalCode(string couponCode)
        {
        }
    }
}