using Fidely.Framework.Compilation;
using Fidely.Framework.Compilation.Operators;
using System;

namespace Fidely.Framework.Tests.Instrumentation
{
    public class NullCalculator : CalculatingOperator
    {
        public NullCalculator(string symbol, int priority, OperatorIndependency independency = OperatorIndependency.Strong)
            : base(symbol, priority, independency)
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
