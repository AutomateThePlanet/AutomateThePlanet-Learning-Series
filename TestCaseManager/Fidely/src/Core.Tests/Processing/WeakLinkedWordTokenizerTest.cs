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
    public class WeakLinkedWordTokenizerTest
    {
        private WeakLinkedWordTokenizerWrapper testee;


        [SetUp]
        public void SetUp()
        {
            var operators = new List<FidelyOperator>();
            operators.Add(new NullComparer("<", OperatorIndependency.Weak));
            operators.Add(new NullCalculator("+", 1, OperatorIndependency.Weak));
            operators.Add(new NullCalculator("*", 0, OperatorIndependency.Weak));
            testee = new WeakLinkedWordTokenizerWrapper(operators);
        }

        [TestCase("", Result = "")]
        [TestCase("   \t   ", Result = "")]
        [TestCase("abc", Result = "[@:abc]")]
        [TestCase("\t\ta   ", Result = "[@:a]")]
        [TestCase("a b c", Result = "[@:a][@:b][@:c]")]
        [TestCase("a   b \t \tc", Result = "[@:a][@:b][@:c]")]
        [TestCase("a AND b", Result = "[@:a][&:AND][@:b]")]
        [TestCase("a OR b", Result = "[@:a][|:OR][@:b]")]
        [TestCase("a and b Or c", Result = "[@:a][&:AND][@:b][|:OR][@:c]")]
        [TestCase("a<b+c*d", Result = "[@:a<b+c*d]")]
        [TestCase("a < b + c * d", Result = "[@:a][cmp:<][@:b][calc:+][@:c][calc:*][@:d]")]
        public string TokenizeShouldReturnTokensCorrectly(string query)
        {
            var actual = testee.InvokeTokenize(new UncategorizedToken(query));
            return String.Join("", actual.Select(o => o.ToString()));
        }
    }
}
