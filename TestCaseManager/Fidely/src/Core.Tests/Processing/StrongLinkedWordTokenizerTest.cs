using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Operators;
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
    public class StrongLinkedWordTokenizerTest
    {
        private StrongLinkedWordTokenizerWrapper testee;


        [SetUp]
        public void SetUp()
        {
            var operators = new List<FidelyOperator>();
            operators.Add(new NullCalculator("+", 0));
            operators.Add(new NullCalculator("-", 0));
            operators.Add(new NullComparer("="));
            operators.Add(new NullComparer("=="));
            operators.Add(new NullComparer("!="));
            testee = new StrongLinkedWordTokenizerWrapper(operators);
        }

        [TestCase("", Result = "")]
        [TestCase("abc", Result = "[?:abc]")]
        [TestCase("(", Result = "[(:(]")]
        [TestCase("(((", Result = "[(:(][(:(][(:(]")]
        [TestCase(" ( ", Result = "[?: ][(:(][?: ]")]
        [TestCase("a ( b", Result = "[?:a ][(:(][?: b]")]
        [TestCase("a(b(c", Result = "[?:a][(:(][?:b][(:(][?:c]")]
        [TestCase(")", Result = "[):)]")]
        [TestCase(")))", Result = "[):)][):)][):)]")]
        [TestCase(" ) ", Result = "[?: ][):)][?: ]")]
        [TestCase("a ) b", Result = "[?:a ][):)][?: b]")]
        [TestCase("a)b)c", Result = "[?:a][):)][?:b][):)][?:c]")]
        [TestCase("()", Result = "[(:(][):)]")]
        [TestCase("(a)", Result = "[(:(][?:a][):)]")]
        [TestCase("()()", Result = "[(:(][):)][(:(][):)]")]
        [TestCase("(a)(b)", Result = "[(:(][?:a][):)][(:(][?:b][):)]")]
        [TestCase("((a))", Result = "[(:(][(:(][?:a][):)][):)]")]
        [TestCase("+-=!===", Result = "[calc:+][calc:-][cmp:=][cmp:!=][cmp:==]")]
        [TestCase("a+b", Result = "[?:a][calc:+][?:b]")]
        [TestCase("a - b", Result = "[?:a ][calc:-][?: b]")]
        [TestCase("a=b!=c", Result = "[?:a][cmp:=][?:b][cmp:!=][?:c]")]
        [TestCase("(a+b)-c", Result = "[(:(][?:a][calc:+][?:b][):)][calc:-][?:c]")]
        public string TokenizeShouldReturnTokensCorrectly(string query)
        {
            var actual = testee.InvokeTokenize(new UncategorizedToken(query));
            return String.Join("", actual.Select(o => o.ToString()));
        }
    }
}
