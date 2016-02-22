using System;

namespace CSharp.Series.Tests.CurrencyFormatter
{
    [Flags]
    public enum DigitsFormattingSettingsBitShifting
    {
        None = 0,
        NoComma = 1 << 0,
        PrefixDollar = 1 << 1,
        PrefixMinus = 1 << 2,
        SufixDollar = 1 << 3
    }
}
