using NUnit.Framework;
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation.Objects.Tests.Instrumentation
{
    internal static class TestCaseDataBuilder
    {
        internal static TestCaseData BuildUp(object value)
        {
            return new TestCaseData(new BlankOperand(), OperandBuilder.BuildUp(value));
        }

        internal static TestCaseData BuildUp(object left, object right)
        {
            return new TestCaseData(OperandBuilder.BuildUp(left), OperandBuilder.BuildUp(right));
        }
    }
}
