// <copyright file="CurrencyDelimiterFormatter.cs" company="Automate The Planet Ltd.">
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
using System.Globalization;

namespace NeatTricksEffortlesslyFormattingCurrency
{
    public static class CurrencyDelimiterFormatter
    {
        private static readonly CultureInfo usCultureInfo = CultureInfo.CreateSpecificCulture("en-US");
        
        public static string ToStringUsDigitsFormatting(
            this double number,
            DigitsFormattingSettings digitsFormattingSettings = DigitsFormattingSettings.None,
            int precesion = 2)
        {
            var result = ToStringUsDigitsFormattingInternal(number, digitsFormattingSettings, precesion);

            return result;
        }
        
        public static string ToStringUsDigitsFormatting(
            this decimal number,
            DigitsFormattingSettings digitsFormattingSettings = DigitsFormattingSettings.None,
            int precesion = 2)
        {
            var result = ToStringUsDigitsFormattingInternal(number, digitsFormattingSettings, precesion);

            return result;
        }

        private static string ToStringUsDigitsFormattingInternal<T>(
            T number,
            DigitsFormattingSettings digitsFormattingSettings = DigitsFormattingSettings.None,
            int precesion = 2)
            where T : struct,
                      IComparable,
                      IComparable<T>,
                      IConvertible,
                      IEquatable<T>,
                      IFormattable
        {
            var formattedDigits = string.Empty;
            var currentNoComaFormatSpecifier = string.Concat("#.", new string('0', precesion));
            var currentComaFormatSpecifier = string.Concat("##,#.", new string('0', precesion));
            formattedDigits =
                             digitsFormattingSettings.HasFlag(DigitsFormattingSettings.NoComma) ? number.ToString(currentNoComaFormatSpecifier, usCultureInfo) :
                              number.ToString(currentComaFormatSpecifier, usCultureInfo);
            if (digitsFormattingSettings.HasFlag(DigitsFormattingSettings.PrefixDollar))
            {
                formattedDigits = string.Concat("$", formattedDigits);
            }
            if (digitsFormattingSettings.HasFlag(DigitsFormattingSettings.PrefixMinus))
            {
                formattedDigits = string.Concat("-", formattedDigits);
            }
            if (digitsFormattingSettings.HasFlag(DigitsFormattingSettings.SufixDollar))
            {
                formattedDigits = string.Concat(formattedDigits, "$");
            }

            return formattedDigits;
        }

        private static string ToStringUsDigitsFormattingInternal(
            dynamic number,
            DigitsFormattingSettings digitsFormattingSettings = DigitsFormattingSettings.None,
            int precesion = 2)
        {
            var formattedDigits = string.Empty;
            var currentNoComaFormatSpecifier = string.Concat("#.", new string('0', precesion));
            var currentComaFormatSpecifier = string.Concat("##,#.", new string('0', precesion));
            formattedDigits =
                             digitsFormattingSettings.HasFlag(DigitsFormattingSettings.NoComma) ? number.ToString(currentNoComaFormatSpecifier, usCultureInfo) :
                              number.ToString(currentComaFormatSpecifier, usCultureInfo);
            if (digitsFormattingSettings.HasFlag(DigitsFormattingSettings.PrefixDollar))
            {
                formattedDigits = string.Concat("$", formattedDigits);
            }
            if (digitsFormattingSettings.HasFlag(DigitsFormattingSettings.PrefixMinus))
            {
                formattedDigits = string.Concat("-", formattedDigits);
            }
            if (digitsFormattingSettings.HasFlag(DigitsFormattingSettings.SufixDollar))
            {
                formattedDigits = string.Concat(formattedDigits, "$");
            }
            return formattedDigits;
        }
    }
}
