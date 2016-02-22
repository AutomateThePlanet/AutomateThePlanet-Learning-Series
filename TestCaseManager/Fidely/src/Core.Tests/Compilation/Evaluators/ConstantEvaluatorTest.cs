using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fidely.Framework.Compilation.Evaluators;
using System.Linq.Expressions;

namespace Fidely.Framework.Tests.Compilation.Evaluators
{
    [TestFixture]
    public class ConstantEvaluatorTest
    {
        private GuardianEvaluator testee;


        [SetUp]
        public void SetUp()
        {
            testee = new GuardianEvaluator();
        }


        [TestCase(null, Result = "System.String:")]
        [TestCase("0", Result = "System.String:0")]
        [TestCase("2011/5/5", Result = "System.String:2011/5/5")]
        [TestCase("1.1.1.1", Result = "System.String:1.1.1.1")]
        [TestCase("a", Result = "System.String:a")]
        public string EvaluateShouldReturnCorrectValue(string value)
        {
            var operand = testee.Evaluate(null, value);
            var actual = Expression.Lambda(operand.Expression).Compile().DynamicInvoke();

            Assert.IsInstanceOf<ConstantExpression>(operand.Expression);
            Assert.IsTrue(actual.GetType() == operand.OperandType);

            return String.Format("{0}:{1}", actual.GetType().FullName, actual.ToString());
        }
    }
}
