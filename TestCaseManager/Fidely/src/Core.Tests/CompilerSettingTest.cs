using Fidely.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Compilation;
using Fidely.Framework.Integration;
using System.Linq.Expressions;
using Fidely.Framework.Compilation.Operators;

namespace Fidely.Framework.Tests
{
    [TestFixture]
    public class CompilerSettingTest
    {
        private CompilerSetting testee;


        [SetUp]
        public void SetUp()
        {
            testee = new CompilerSetting();
            testee.Evaluators.Add(new CustomEvaluator());
            testee.Operators.Add(new CustomComparativeOperator());
            testee.Operators.Add(new CustomAddOperator());
            testee.Operators.Add(new CustomSubtractOperator());
        }

        [Test]
        public void ExtractAutoCompleteItemsShouldWorkCorrectly()
        {
            var actual = testee.ExtractAutoCompleteItems().Select(o => o.DisplayName + ":" + o.Description);

            var expected = new string[]
            {
                "aaa:AAA",
                "bbb:BBB",
                "ccc:CCC",
                "cmp:XXX",
                "+:YYY",
                "-:"
            };
            CollectionAssert.AreEqual(expected, actual.ToList());
        }


        private class CustomEvaluator : OperandEvaluator
        {
            public CustomEvaluator()
            {
                Register(new AutoCompleteItem("aaa", "AAA"));
                Register(new AutoCompleteItem("bbb", "BBB"));
                Register(new AutoCompleteItem("ccc", "CCC"));
            }

            public override Operand Evaluate(Expression current, string value)
            {
                throw new NotImplementedException();
            }

            public override OperandEvaluator Clone()
            {
                throw new NotImplementedException();
            }
        }

        private class CustomComparativeOperator : ComparativeOperator, IDescribable
        {
            public string Description { get { return "XXX"; } }

            public CustomComparativeOperator()
                : base("cmp")
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

        [System.ComponentModel.Description("YYY")]
        private class CustomAddOperator : CalculatingOperator
        {
            public CustomAddOperator()
                : base("+", 0)
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

        private class CustomSubtractOperator : CalculatingOperator
        {
            public CustomSubtractOperator()
                : base("-", 0)
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
