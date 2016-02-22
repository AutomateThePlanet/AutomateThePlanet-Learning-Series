using System;
using System.Linq.Expressions;

namespace Fidely.Framework.Compilation.Objects.Tests.Instrumentation
{
    internal static class OperandBuilder
    {
        internal static Operand BuildUp(object value)
        {
            return value != null ? new Operand(Expression.Constant(value, value.GetType()), value.GetType()) : new Operand(Expression.Constant(value, typeof(string)), typeof(string));
        }
    }
}
