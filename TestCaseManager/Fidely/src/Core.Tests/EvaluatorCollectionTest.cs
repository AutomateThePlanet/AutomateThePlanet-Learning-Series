using Fidely.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Compilation;
using System.Linq.Expressions;

namespace Fidely.Framework.Tests
{
    [TestFixture]
    public class EvaluatorCollectionTest
    {
        private EvaluatorCollection testee;

        private List<OperandEvaluator> items;


        [SetUp]
        public void SetUp()
        {
            testee = new EvaluatorCollection();
            items = testee.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(testee) as List<OperandEvaluator>;
        }

        [Test]
        public void AddShouldThrowExceptionWhenSpecifiedEvaluatorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => testee.Add(null));
        }

        [Test]
        public void AddShouldAddItemToItems()
        {
            var item = new CustomOperandEvaluator();
            testee.Add(item);

            Assert.AreEqual(1, items.Count);
            Assert.AreSame(item, items[0]);
        }


        private class CustomOperandEvaluator : OperandEvaluator
        {
            public override Operand Evaluate(Expression current, string value)
            {
                throw new NotImplementedException();
            }

            public override OperandEvaluator Clone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
