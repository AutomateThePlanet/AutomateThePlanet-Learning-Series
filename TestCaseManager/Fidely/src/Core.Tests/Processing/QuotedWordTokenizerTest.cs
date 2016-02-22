using Fidely.Framework.Processing;
using Fidely.Framework.Tests.Instrumentation;
using Fidely.Framework.Tests.Instrumentation.Processing;
using Fidely.Framework.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fidely.Framework.Tests.Processing
{
    [TestFixture]
    public class QuotedWordTokenizerTest
    {
        private QuotedWordTokenizerWrapper testee;


        [SetUp]
        public void SetUp()
        {
            testee = new QuotedWordTokenizerWrapper();
        }

        [TestCase("", Result = "")]
        [TestCase(" ", Result = "[?: ]")]
        [TestCase("abc def", Result = "[?:abc def]")]
        [TestCase("'abc'", Result = "[Q:abc]")]
        [TestCase("\"abc\"", Result = "[@:abc]")]
        [TestCase("'a\"b\"c'", Result = "[Q:a\"b\"c]")]
        [TestCase("\"a'b'c\"", Result = "[@:a'b'c]")]
        [TestCase("'abc", Result = "[Q:abc]")]
        [TestCase("\"abc", Result = "[@:abc]")]
        [TestCase("abc'", Result = "[?:abc][Q:]")]
        [TestCase("abc\"", Result = "[?:abc][@:]")]
        [TestCase("'ab cd' ef", Result = "[Q:ab cd][?: ef]")]
        [TestCase("ab \"cd ef\"", Result = "[?:ab ][@:cd ef]")]
        [TestCase(" 'a' b \"c\" ", Result = "[?: ][Q:a][?: b ][@:c][?: ]")]
        public string TokenizeShouldReturnTokensCorrectly(string query)
        {
            var actual = testee.InvokeTokenize(new UncategorizedToken(query));
            return String.Join("", actual.Select(o => o.ToString()));
        }
    }
}
