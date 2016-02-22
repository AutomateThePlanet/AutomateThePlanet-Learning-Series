using Fidely.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fidely.Framework.Compilation.Operators;
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Compilation;
using System.Linq.Expressions;

namespace Fidely.Framework.Tests
{
    [TestFixture]
    public class OperatorCollectionTest
    {
        private OperatorCollection testee;

        private List<FidelyOperator> items;


        [SetUp]
        public void SetUp()
        {
            testee = new OperatorCollection();
            testee.Add(new CustomOperator("xxx"));

            items = testee.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(testee) as List<FidelyOperator>;
        }

        [Test]
        public void AddShouldThrowExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => testee.Add(null));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase("AND")]
        [TestCase("And")]
        [TestCase("and")]
        [TestCase("OR")]
        [TestCase("Or")]
        [TestCase("or")]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("xxx")]
        [TestCase("XXX")]
        public void AddShouldThrowExceptionWhenSpecifiedOperatorIsInvalid(string op)
        {
            Assert.Throws<ArgumentException>(() => testee.Add(new CustomOperator(op)));
        }

        [Test]
        public void AddShouldAddItemToItems()
        {
            var item = new CustomOperator("foo");
            testee.Add(item);

            Assert.AreEqual(2, items.Count);
            Assert.AreSame(item, items[1]);
        }


        private class CustomOperator : ComparativeOperator
        {
            public CustomOperator(string symbol)
                : base(symbol)
            {
            }

            public override Operand Compare(Expression current, Operand left, Operand right)
            {
                throw new NotImplementedException();
            }

            public override FidelyOperator Clone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
