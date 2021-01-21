// <copyright file="ObsoleteExample.cs" company="Automate The Planet Ltd.">
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

namespace TopUnderutilizedFeaturesdDotNetPartOne
{
    public static class ObsoleteExample
    {
        // Mark OrderDetailTotal As Obsolete.
        [ObsoleteAttribute("This property (DepricatedOrderDetailTotal) is obsolete. Use InvoiceTotal instead.", false)]
        public static decimal OrderDetailTotal
        {
            get
            {
                return 12m;
            }
        }

        public static decimal InvoiceTotal
        {
            get
            {
                return 25m;
            }
        }

        // Mark CalculateOrderDetailTotal As Obsolete.
        [ObsoleteAttribute("This method is obsolete. Call CalculateInvoiceTotal instead.", true)]
        public static decimal CalculateOrderDetailTotal()
        {
            return 0m;
        }

        public static decimal CalculateInvoiceTotal()
        {
            return 1m;
        }
    }
}