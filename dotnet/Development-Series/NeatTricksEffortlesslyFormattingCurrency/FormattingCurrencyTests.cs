// <copyright file="FormattingCurrencyTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeatTricksEffortlesslyFormattingCurrency
{
    [TestClass]
    public class FormattingCurrencyTests
    {
        [TestMethod]
        public void FormatCurrencyExample()
        {
            Console.WriteLine(1220.5.ToStringUsDigitsFormatting(DigitsFormattingSettings.PrefixDollar));
            ////// Result- $1,220.50
            Console.WriteLine(1220.5.ToStringUsDigitsFormatting(DigitsFormattingSettings.SufixDollar | DigitsFormattingSettings.NoComma));
            ////// Result- 1220.50$
            Console.WriteLine(1220.53645.ToStringUsDigitsFormatting(DigitsFormattingSettings.SufixDollar | DigitsFormattingSettings.NoComma | DigitsFormattingSettings.PrefixMinus, 4));
            //////Result- -1220.5365$
        }
    }
}
