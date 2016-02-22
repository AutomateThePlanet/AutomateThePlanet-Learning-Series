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
    public class LogicalAndFillerTest
    {
        private LogicalAndFiller testee;


        [SetUp]
        public void SetUp()
        {
            testee = new LogicalAndFiller();
        }

        [TestCase("", Result = "")]
        [TestCase("a", Result = "[@:a]")]
        [TestCase("a b", Result = "[@:a][&:AND][@:b]")]
        [TestCase("a b c", Result = "[@:a][&:AND][@:b][&:AND][@:c]")]
        [TestCase("AND", Result = "[&:AND]")]
        [TestCase("a AND", Result = "[@:a][&:AND]")]
        [TestCase("AND a", Result = "[&:AND][@:a]")]
        [TestCase("a AND b", Result = "[@:a][&:AND][@:b]")]
        [TestCase("AND AND", Result = "[&:AND][&:AND]")]
        [TestCase("OR", Result = "[|:OR]")]
        [TestCase("a OR", Result = "[@:a][|:OR]")]
        [TestCase("OR a", Result = "[|:OR][@:a]")]
        [TestCase("a OR b", Result = "[@:a][|:OR][@:b]")]
        [TestCase("OR OR", Result = "[|:OR][|:OR]")]
        [TestCase("=", Result = "[cmp:=]")]
        [TestCase("a =", Result = "[@:a][cmp:=]")]
        [TestCase("= a", Result = "[cmp:=][@:a]")]
        [TestCase("a = b", Result = "[@:a][cmp:=][@:b]")]
        [TestCase("= =", Result = "[cmp:=][cmp:=]")]
        [TestCase("+", Result = "[calc:+]")]
        [TestCase("a +", Result = "[@:a][calc:+]")]
        [TestCase("+ a", Result = "[calc:+][@:a]")]
        [TestCase("a + b", Result = "[@:a][calc:+][@:b]")]
        [TestCase("+ +", Result = "[calc:+][calc:+]")]
        [TestCase("(", Result = "[(:(]")]
        [TestCase("a (", Result = "[@:a][&:AND][(:(]")]
        [TestCase("( a", Result = "[(:(][@:a]")]
        [TestCase("a ( b", Result = "[@:a][&:AND][(:(][@:b]")]
        [TestCase("( (", Result = "[(:(][(:(]")]
        [TestCase(")", Result = "[):)]")]
        [TestCase("a )", Result = "[@:a][):)]")]
        [TestCase(") a", Result = "[):)][&:AND][@:a]")]
        [TestCase("a ) b", Result = "[@:a][):)][&:AND][@:b]")]
        [TestCase(") )", Result = "[):)][):)]")]
        [TestCase("( )", Result = "[(:(][):)]")]
        [TestCase(") (", Result = "[):)][&:AND][(:(]")]
        [TestCase("( AND OR = + )", Result = "[(:(][&:AND][|:OR][cmp:=][calc:+][):)]")]
        [TestCase(") ( a b ) c", Result = "[):)][&:AND][(:(][@:a][&:AND][@:b][):)][&:AND][@:c]")]
        public string FilterShouldFillLogicalAndOperatorsCorrectly(string query)
        {
            var tokens = SimplexTokenizer.Tokenize(query);
            var actual = testee.Filter(tokens);
            return String.Join("", actual.Select(o => o.ToString()));
        }
    }
}
