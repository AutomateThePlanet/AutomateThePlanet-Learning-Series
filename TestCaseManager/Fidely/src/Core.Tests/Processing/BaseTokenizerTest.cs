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
    public class BaseTokenizerTest
    {
        private BaseTokenizerImpl testee;


        [SetUp]
        public void SetUp()
        {
            testee = new BaseTokenizerImpl();
        }

        [Test]
        public void TokenizeShouldThrowExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => testee.Filter(null));
        }

        [Test]
        public void TokenizeShouldThrowExceptionWhenArgumentContainsNull()
        {
            var tokens = new List<IToken>();
            tokens.Add(new UncategorizedToken());
            tokens.Add(null);
            tokens.Add(new OperandToken());

            Assert.Throws<ArgumentException>(() => testee.Filter(tokens));
        }

        [Test]
        public void TokenizeShouldReturnEmptyWhenArgumentIsEmpty()
        {
            var actual = testee.Filter(new List<IToken>());

            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void TokenizeShouldInvokeTokenizeWhenArgumentContainsUncategorizedToken()
        {
            var tokens = new List<IToken>();
            tokens.Add(new UncategorizedToken("A"));
            tokens.Add(new OperandToken("B"));
            tokens.Add(new UncategorizedToken("C"));

            testee.Filter(tokens);

            var expected = new List<IToken>();
            expected.Add(new UncategorizedToken("A"));
            expected.Add(new UncategorizedToken("C"));

            CollectionAssert.AreEqual(expected, testee.PassedUncategorizedTokens, new TokenComparer());
        }

        [Test]
        public void TokenizeShouldNotInvokeTokenizeWhenArgumentDoesNotContainUncategorizedToken()
        {
            var tokens = new List<IToken>();
            tokens.Add(new OperandToken("A"));
            tokens.Add(new OperandToken("B"));
            tokens.Add(new OperandToken("C"));

            testee.Filter(tokens);

            Assert.AreEqual(0, testee.PassedUncategorizedTokens.Count);
        }
    }
}
