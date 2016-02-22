using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fidely.Framework.Compilation.Evaluators;
using System.Linq.Expressions;
using Fidely.Framework.Compilation.Objects.Evaluators;

namespace Fidely.Framework.Compilation.Objects.Tests.Evaluators
{
    [TestFixture]
    public class TypeConversionEvaluatorTest
    {
        private TypeConversionEvaluator testee;


        [SetUp]
        public void SetUp()
        {
            testee = new TypeConversionEvaluator();
        }


        [TestCase(null, Result = "System.String:")]
        [TestCase("0", Result = "System.Decimal:0")]
        [TestCase("1", Result = "System.Decimal:1")]
        [TestCase("-1", Result = "System.Decimal:-1")]
        [TestCase("0.5", Result = "System.Decimal:0.5")]
        [TestCase("2011/5/5", Result = "System.DateTime:2011-05-05 12:00:00 AM")]
        [TestCase("2011-06-06", Result = "System.DateTime:2011-06-06 12:00:00 AM")]
        [TestCase("2011-07-07 8:34:55", Result = "System.DateTime:2011-07-07 8:34:55 AM")]
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
