using System;

namespace CSharp.Series.Tests
{
    [Flags]
    public enum DigitsFormattingSettings
    {
        None = 0,
        NoComma = 1,
        PrefixDollar = 2,
        PrefixMinus = 4,
        SufixDollar = 8
    }
}
