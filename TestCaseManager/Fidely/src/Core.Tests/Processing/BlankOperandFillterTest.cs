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
    public class BlankOperandFillterTest 
    {
        private BlankOperandFiller testee;


        [SetUp]
        public void SetUp()
        {
            testee = new BlankOperandFiller();
        }

        [TestCase("", Result = "[B:]")]
        [TestCase("a", Result = "[@:a]")]
        [TestCase("a b", Result = "[@:a][@:b]")]
        [TestCase("AND", Result = "[B:][&:AND][B:]")]
        [TestCase("a AND", Result = "[@:a][&:AND][B:]")]
        [TestCase("AND a", Result = "[B:][&:AND][@:a]")]
        [TestCase("a AND b", Result = "[@:a][&:AND][@:b]")]
        [TestCase("AND AND", Result = "[B:][&:AND][B:][&:AND][B:]")]
        [TestCase("OR", Result = "[B:][|:OR][B:]")]
        [TestCase("a OR", Result = "[@:a][|:OR][B:]")]
        [TestCase("OR a", Result = "[B:][|:OR][@:a]")]
        [TestCase("a OR b", Result = "[@:a][|:OR][@:b]")]
        [TestCase("OR OR", Result = "[B:][|:OR][B:][|:OR][B:]")]
        [TestCase("=", Result = "[B:][cmp:=][B:]")]
        [TestCase("a =", Result = "[@:a][cmp:=][B:]")]
        [TestCase("= a", Result = "[B:][cmp:=][@:a]")]
        [TestCase("a = b", Result = "[@:a][cmp:=][@:b]")]
        [TestCase("= =", Result = "[B:][cmp:=][B:][cmp:=][B:]")]
        [TestCase("+", Result = "[B:][calc:+][B:]")]
        [TestCase("a +", Result = "[@:a][calc:+][B:]")]
        [TestCase("+ a", Result = "[B:][calc:+][@:a]")]
        [TestCase("a + b", Result = "[@:a][calc:+][@:b]")]
        [TestCase("+ +", Result = "[B:][calc:+][B:][calc:+][B:]")]
        [TestCase("(", Result = "[(:(][B:]")]
        [TestCase("a (", Result = "[@:a][(:(][B:]")]
        [TestCase("( a", Result = "[(:(][@:a]")]
        [TestCase("a ( b", Result = "[@:a][(:(][@:b]")]
        [TestCase("( (", Result = "[(:(][(:(][B:]")]
        [TestCase(")", Result = "[B:][):)]")]
        [TestCase("a )", Result = "[@:a][):)]")]
        [TestCase(") a", Result = "[B:][):)][@:a]")]
        [TestCase("a ) b", Result = "[@:a][):)][@:b]")]
        [TestCase(") )", Result = "[B:][):)][):)]")]
        [TestCase("( AND OR = + )", Result = "[(:(][B:][&:AND][B:][|:OR][B:][cmp:=][B:][calc:+][B:][):)]")]
        [TestCase("( a AND b OR c = d + e )", Result = "[(:(][@:a][&:AND][@:b][|:OR][@:c][cmp:=][@:d][calc:+][@:e][):)]")]
        public string FilterShouldFillBlankOperandsCorrectly(string query)
        {
            var tokens = SimplexTokenizer.Tokenize(query);
            var actual = testee.Filter(tokens);
            return String.Join("", actual.Select(o => o.ToString()));
        }
    }
}
