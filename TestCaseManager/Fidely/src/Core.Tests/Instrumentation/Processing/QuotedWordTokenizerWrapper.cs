using Fidely.Framework.Processing;
using Fidely.Framework.Tokens;
using System;
using System.Collections.Generic;

namespace Fidely.Framework.Tests.Instrumentation.Processing
{
    internal class QuotedWordTokenizerWrapper : QuotedWordTokenizer
    {
        internal IEnumerable<IToken> InvokeTokenize(UncategorizedToken token)
        {
            return base.Tokenize(token);
        }
    }
}
