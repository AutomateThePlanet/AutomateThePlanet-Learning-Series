using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Operators;
using System;
using System.Linq.Expressions;

namespace Fidely.Framework.Tests.Instrumentation
{
    public class NullComparer : ComparativeOperator
    {
        public NullComparer(string symbol, OperatorIndependency independency = OperatorIndependency.Strong)
            : base(symbol, independency)
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
}
