using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Compilation.Operators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation.Objects.Tests.Operators
{
    [TestFixture]
    public class BaseBuiltInComparativeOperatorTest
    {
        [TestCase(null, Result = "")]
        [TestCase("", Result = "")]
        [TestCase(" ", Result = " ")]
        [TestCase("XXX", Result = "XXX")]
        public string DescriptionShouldReturnCorrectValue(string description)
        {
            var testee = new CustomOperatorNoDescriptionAttribute(description);
            return testee.Description;
        }

        [TestCase(null, Result = "Description Attribute")]
        [TestCase("", Result = "")]
        [TestCase(" ", Result = " ")]
        [TestCase("XXX", Result = "XXX")]
        public string DescriptionShouldReturnCorrectValueWhenOperatorHasDescriptionAttribute(string description)
        {
            var testee = new CustomOperator(description);
            return testee.Description;
        }

        [TestCase]
        public void CompareShouldNotEvaluatePropertyWhenLeftOperandIsNotBlank()
        {
            var entity = new Entity();
            var testee = new CustomOperator("");
            var left = new Operand(Expression.Constant(true), typeof(bool));
            var right = new Operand(Expression.Constant(true), typeof(bool));

            testee.Compare(Expression.Constant(entity), left, right);

            Assert.AreEqual(0, entity.CalledProperties.Count);
            Assert.AreEqual(1, testee.CompareCalledCount);
        }

        [TestCase]
        public void CompareShouldIgnoreSetOnlyProperty()
        {
            var entity = new Entity();
            var testee = new CustomOperator("");
            var left = new BlankOperand();
            var right = new Operand(Expression.Constant(true), typeof(bool));

            testee.Compare(Expression.Constant(entity), left, right);

            CollectionAssert.AreEqual(new string[] { "Both:Get", "GetOnly:Get" }, entity.CalledProperties);
            Assert.AreEqual(2, testee.CompareCalledCount);
        }

        [TestCase]
        public void CompareShouldReturnConstantTrueWhenTargetTypeDoesNotHaveProperty()
        {
            var entity = new EmptyEntity();
            var testee = new CustomOperatorNoDescriptionAttribute("");
            var left = new BlankOperand();
            var right = new Operand(Expression.Constant(true), typeof(bool));

            var actual = testee.Compare(Expression.Constant(entity), left, right);

            Assert.IsInstanceOf<ConstantExpression>(actual.Expression);
            Assert.IsTrue((bool)Expression.Lambda(actual.Expression).Compile().DynamicInvoke());
            Assert.AreEqual(typeof(bool), actual.OperandType);
        }


        public class EmptyEntity
        {
        }

        public class Entity
        {
            public readonly List<string> CalledProperties = new List<string>();

            public string Both
            {
                get
                {
                    CalledProperties.Add("Both:Get");
                    return "";
                }
                set
                {
                    CalledProperties.Add("Both:Set");
                }
            }

            public string GetOnly
            {
                get
                {
                    CalledProperties.Add("GetOnly:Get");
                    return "";
                }
            }

            public string SetOnly
            {
                set
                {
                    CalledProperties.Add("SetOnly:Set");
                }
            }
        }

        public class CustomOperatorNoDescriptionAttribute : BaseBuiltInComparativeOperator<EmptyEntity>
        {
            public CustomOperatorNoDescriptionAttribute(string description)
                : base("x", OperatorIndependency.Strong, description)
            {
            }

            protected internal override Expression Compare(Operand left, Operand right)
            {
                throw new NotImplementedException();
            }

            public override FidelyOperator Clone()
            {
                throw new NotImplementedException();
            }
        }

        [System.ComponentModel.Description("Description Attribute")]
        public class CustomOperator : BaseBuiltInComparativeOperator<Entity>
        {
            public int CompareCalledCount { get; private set; }

            public CustomOperator(string description)
                : base("x", OperatorIndependency.Strong, description)
            {
            }

            protected internal override Expression Compare(Operand left, Operand right)
            {
                CompareCalledCount++;
                Expression.Lambda(left.Expression).Compile().DynamicInvoke();
                return Expression.Constant(false);
            }

            public override FidelyOperator Clone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
