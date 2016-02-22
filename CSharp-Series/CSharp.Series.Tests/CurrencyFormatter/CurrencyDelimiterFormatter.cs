using System;
using System.Globalization;

namespace CSharp.Series.Tests
{
    public static class CurrencyDelimiterFormatter
    {
        private static readonly CultureInfo usCultureInfo = CultureInfo.CreateSpecificCulture("en-US");
        
        public static string ToStringUsDigitsFormatting(
            this double number,
            DigitsFormattingSettings digitsFormattingSettings = DigitsFormattingSettings.None,
            int precesion = 2)
        {
            string result = ToStringUsDigitsFormattingInternal(number, digitsFormattingSettings, precesion);

            return result;
        }
        
        public static string ToStringUsDigitsFormatting(
            this decimal number,
            DigitsFormattingSettings digitsFormattingSettings = DigitsFormattingSettings.None,
            int precesion = 2)
        {
            string result = ToStringUsDigitsFormattingInternal(number, digitsFormattingSettings, precesion);

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
            string formattedDigits = string.Empty;
            string currentNoComaFormatSpecifier = string.Concat("#.", new string('0', precesion));
            string currentComaFormatSpecifier = string.Concat("##,#.", new string('0', precesion));
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
            string formattedDigits = string.Empty;
            string currentNoComaFormatSpecifier = string.Concat("#.", new string('0', precesion));
            string currentComaFormatSpecifier = string.Concat("##,#.", new string('0', precesion));
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
