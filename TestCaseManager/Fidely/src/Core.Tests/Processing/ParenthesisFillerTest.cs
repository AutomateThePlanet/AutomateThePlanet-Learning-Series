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
    public class ParenthesisFillerTest
    {
        private ParenthesisFiller testee;


        [SetUp]
        public void SetUp()
        {
            testee = new ParenthesisFiller();
        }

        [TestCase("", Result = "")]
        [TestCase("a", Result = "[@:a]")]
        [TestCase("a b", Result = "[@:a][@:b]")]
        [TestCase("( )", Result = "[(:(][):)]")]
        [TestCase("( a )", Result = "[(:(][@:a][):)]")]
        [TestCase("a ( b ) c", Result = "[@:a][(:(][@:b][):)][@:c]")]
        [TestCase("(", Result = "[(:(][):)]")]
        [TestCase(")", Result = "[(:(][):)]")]
        [TestCase("( (", Result = "[(:(][(:(][):)][):)]")]
        [TestCase(") )", Result = "[(:(][(:(][):)][):)]")]
        [TestCase("( ( )", Result = "[(:(][(:(][):)][):)]")]
        [TestCase("( ) (", Result = "[(:(][):)][(:(][):)]")]
        [TestCase("( ) )", Result = "[(:(][(:(][):)][):)]")]
        [TestCase(") ( (", Result = "[(:(][):)][(:(][(:(][):)][):)]")]
        [TestCase("a ) b ( c ( d", Result = "[(:(][@:a][):)][@:b][(:(][@:c][(:(][@:d][):)][):)]")]
        public string FilterShouldFillParenthesesCorrectly(string query)
        {
            var tokens = SimplexTokenizer.Tokenize(query);
            var actual = testee.Filter(tokens);
            return String.Join("", actual.Select(o => o.ToString()));
        }
    }
}
