using Fidely.Framework.Tokens;
using System;
using System.Collections;

namespace Fidely.Framework.Tests.Instrumentation
{
    public class TokenComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null)
            {
                throw new InvalidOperationException("Failed to compare because the specified arguments contain null.");
            }

            if (x is UncategorizedToken)
            {
                return Compare(x as UncategorizedToken, y as UncategorizedToken);
            }
            else if (x is OperatorToken)
            {
                return Compare(x as OperandToken, y as OperandToken);
            }
            else if (x is OperandToken)
            {
                return Compare(x as OperandToken, y as OperandToken);
            }

            throw new InvalidOperationException("Failed to compare because the specified arguments contain object that is unsupported token type.");
        }

        private int Compare(UncategorizedToken left, UncategorizedToken right)
        {
            if (left == null || right == null)
            {
                throw new InvalidOperationException("Failed to compare because the specified arguments contain null.");
            }

            return String.Compare(left.Value, right.Value);
        }

        private int Compare(OperandToken left, OperandToken right)
        {
            if (left == null || right == null)
            {
                throw new InvalidOperationException("Failed to compare because the specified arguments contain null.");
            }

            return String.Compare(left.Value, right.Value);
        }
    }
}
