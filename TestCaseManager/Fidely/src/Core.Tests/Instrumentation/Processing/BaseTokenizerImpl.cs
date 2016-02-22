using Fidely.Framework.Processing;
using Fidely.Framework.Tokens;
using System;
using System.Collections.Generic;

namespace Fidely.Framework.Tests.Instrumentation.Processing
{
    internal class BaseTokenizerImpl : BaseTokenizer
    {
        internal ICollection<UncategorizedToken> PassedUncategorizedTokens { get; private set; }


        internal BaseTokenizerImpl()
        {
            PassedUncategorizedTokens = new List<UncategorizedToken>();
        }


        protected override IEnumerable<IToken> Tokenize(UncategorizedToken token)
        {
            PassedUncategorizedTokens.Add(token);
            return new List<IToken>();
        }
    }
}
