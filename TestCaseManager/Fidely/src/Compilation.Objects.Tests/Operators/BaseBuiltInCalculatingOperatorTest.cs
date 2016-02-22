using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Objects.Operators;
using Fidely.Framework.Compilation.Operators;
using NUnit.Framework;
using System;

namespace Fidely.Framework.Compilation.Objects.Tests.Operators
{
    [TestFixture]
    public class BaseBuiltInCalculatingOperatorTest
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


        public class CustomOperatorNoDescriptionAttribute
            : BaseBuiltInCalculatingOperator
        {
            public CustomOperatorNoDescriptionAttribute(string description)
                : base("x", 0, OperatorIndependency.Strong, description)
            {
            }

            public override Operand Calculate(Operand left, Operand right)
            {
                throw new NotImplementedException();
            }

            public override FidelyOperator Clone()
            {
                throw new NotImplementedException();
            }
        }

        [System.ComponentModel.Description("Description Attribute")]
        public class CustomOperator : BaseBuiltInCalculatingOperator
        {
            public CustomOperator(string description)
                : base("x", 0, OperatorIndependency.Strong, description)
            {
            }

            public override Operand Calculate(Operand left, Operand right)
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
