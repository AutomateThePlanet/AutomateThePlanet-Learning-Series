using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation.Objects.Tests.Instrumentation
{
    internal class BaseTestCaseDataGenerator
    {
        protected TestCaseData BuildUpTestCaseData(object value)
        {
            return TestCaseDataBuilder.BuildUp(value);
        }

        protected TestCaseData BuildUpTestCaseData(object left, object right)
        {
            return TestCaseDataBuilder.BuildUp(left, right);
        }
    }
}
