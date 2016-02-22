using Fidely.Framework.Tokens;
using System;
using System.Collections.Generic;

namespace Fidely.Framework.Tests.Instrumentation
{
    internal static class SimplexTokenizer
    {
        internal static IEnumerable<IToken> Tokenize(string query)
        {
            foreach (var value in query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
            {
                switch (value)
                {
                    case "(":
                        yield return new OpenedParenthesisToken();
                        break;
                    case ")":
                        yield return new ClosedParenthesisToken();
                        break;
                    case "AND":
                        yield return new LogicalAndOperatorToken();
                        break;
                    case "OR":
                        yield return new LogicalOrOperatorToken();
                        break;
                    case "=":
                    case "!=":
                    case "<":
                    case ">":
                        yield return new ComparativeOperatorToken(new NullComparer(value));
                        break;
                    case "*":
                    case "/":
                        yield return new CalculatingOperatorToken(new NullCalculator(value, 0));
                        break;
                    case "+":
                    case "-":
                        yield return new CalculatingOperatorToken(new NullCalculator(value, 1));
                        break;
                    default:
                        yield return new OperandToken(value);
                        break;
                }
            }
        }
    }
}