using System;

namespace CSharp.Series.Tests
{
    public static class CurryMethodExtensions
    {
        public static Func<A, Func<B, Func<C, R>>> Curry<A, B, C, R>(this Func<A, B, C, R> f)
        {
            return a => b => c => f(a, b, c);
        }

        public static Func<C, R> Partial<A, B, C, R>(this Func<A, B, C, R> f, A a, B b)
        {
            return c => f(a, b, c);
        }
    }
}
