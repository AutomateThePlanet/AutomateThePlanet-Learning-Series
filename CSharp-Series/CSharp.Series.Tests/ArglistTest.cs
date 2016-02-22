using System;

namespace CSharp.Series.Tests
{
    public static class ArglistTest
    {
        public static void DisplayNumbersOnConsole(__arglist)
        {
            ArgIterator ai = new ArgIterator(__arglist);
            while (ai.GetRemainingCount() > 0)
            {
                TypedReference tr = ai.GetNextArg();
                Console.WriteLine(TypedReference.ToObject(tr));
            }
        }
    }
}
