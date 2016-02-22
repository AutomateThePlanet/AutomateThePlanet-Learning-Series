using System;
using System.Linq.Expressions;
using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Compilation;

namespace Fidely.Framework.Tests.Instrumentation
{
    public class OperandBuilderImpl : IOperandBuilder
    {
        public Operand BuildUp(object value)
        {
            return new Operand(Expression.Constant(value), value.GetType());
        }
    }
}
