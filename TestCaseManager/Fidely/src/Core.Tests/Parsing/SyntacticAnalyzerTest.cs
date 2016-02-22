using Fidely.Framework.Parsing;
using Fidely.Framework.Tests.Instrumentation;
using Fidely.Framework.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Fidely.Framework.Compilation.Objects.Operators;

namespace Fidely.Framework.Tests.Parsing
{
    [TestFixture]
    public class SyntacticAnalyzerTest
    {
        private SyntacticAnalyzer<CustomEntity> testee;


        [SetUp]
        public void SetUp()
        {
            testee = new SyntacticAnalyzer<CustomEntity>(new PartialMatch<CustomEntity>(":"));
        }


        [TestCase("", Result = "{[B:][cmp::][B:]}")]
        [TestCase("a", Result = "{[B:][cmp::][@:a]}")]
        [TestCase("a AND b", Result = "{{[B:][cmp::][@:a]}[&:AND]{[B:][cmp::][@:b]}}")]
        [TestCase("( a + b ) AND c", Result = "{{[B:][cmp::]{[@:a][calc:+][@:b]}}[&:AND]{[B:][cmp::][@:c]}}")]
        [TestCase("a = b AND c != d", Result = "{{[@:a][cmp:=][@:b]}[&:AND]{[@:c][cmp:!=][@:d]}}")]
        [TestCase("a = b OR c != d AND e < f", Result = "{{[@:a][cmp:=][@:b]}[|:OR]{{[@:c][cmp:!=][@:d]}[&:AND]{[@:e][cmp:<][@:f]}}}")]
        [TestCase("a = b", Result = "{[@:a][cmp:=][@:b]}")]
        [TestCase("( a ) < ( b )", Result = "{[@:a][cmp:<][@:b]}")]
        [TestCase("a < b < c", Result = "{{[@:a][cmp:<][@:b]}[&:AND]{[@:b][cmp:<][@:c]}}")]
        [TestCase("( a ) < ( b ) < ( c )", Result = "{{[@:a][cmp:<][@:b]}[&:AND]{[@:b][cmp:<][@:c]}}")]
        [TestCase("a < ( b < c )", Result = "{{[@:a][cmp:<][B:]}[&:AND]{[@:b][cmp:<][@:c]}}")]
        [TestCase("( a <  b ) < c", Result = "{{[@:a][cmp:<][@:b]}[&:AND]{[B:][cmp:<][@:c]}}")]
        [TestCase("a < b + c < d", Result = "{{[@:a][cmp:<]{[@:b][calc:+][@:c]}}[&:AND]{{[@:b][calc:+][@:c]}[cmp:<][@:d]}}")]
        [TestCase("( a < b ) + c < d", Result = "{{{[@:a][cmp:<][@:b]}[&:AND]{[B:][cmp::]{[B:][calc:+][@:c]}}}[&:AND]{{[B:][calc:+][@:c]}[cmp:<][@:d]}}")]
        [TestCase("a < b + ( c < d )", Result = "{{[@:a][cmp:<][B:]}[&:AND]{{[B:][cmp::]{[@:b][calc:+][B:]}}[&:AND]{[@:c][cmp:<][@:d]}}}")]
        [TestCase("( a < b ) + ( c < d )", Result = "{{[@:a][cmp:<][@:b]}[&:AND]{{[B:][cmp::]{[B:][calc:+][B:]}}[&:AND]{[@:c][cmp:<][@:d]}}}")]
        [TestCase("a < b + c * d < e", Result = "{{[@:a][cmp:<]{[@:b][calc:+]{[@:c][calc:*][@:d]}}}[&:AND]{{[@:b][calc:+]{[@:c][calc:*][@:d]}}[cmp:<][@:e]}}")]
        [TestCase("a < b < c < d", Result = "{{{[@:a][cmp:<][@:b]}[&:AND]{[@:b][cmp:<][@:c]}}[&:AND]{[@:c][cmp:<][@:d]}}")]
        [TestCase("a < ( b < ( c < d ) )", Result = "{{[@:a][cmp:<][B:]}[&:AND]{{[@:b][cmp:<][B:]}[&:AND]{[@:c][cmp:<][@:d]}}}")]
        [TestCase("( a AND b ) < ( c OR d )", Result = "{{{[B:][cmp::][@:a]}[&:AND]{[B:][cmp::][@:b]}}[&:AND]{{[B:][cmp:<][B:]}[&:AND]{{[B:][cmp::][@:c]}[|:OR]{[B:][cmp::][@:d]}}}}")]
        [TestCase("a < b + c", Result = "{[@:a][cmp:<]{[@:b][calc:+][@:c]}}")]
        [TestCase("( a < b ) + c", Result = "{{[@:a][cmp:<][@:b]}[&:AND]{[B:][cmp::]{[B:][calc:+][@:c]}}}")]
        [TestCase(" a + ( b < c )", Result = "{{[B:][cmp::]{[@:a][calc:+][B:]}}[&:AND]{[@:b][cmp:<][@:c]}}")]
        [TestCase("a < b + c * d", Result = "{[@:a][cmp:<]{[@:b][calc:+]{[@:c][calc:*][@:d]}}}")]
        [TestCase("a < ( b + c ) * d", Result = "{[@:a][cmp:<]{{[@:b][calc:+][@:c]}[calc:*][@:d]}}")]
        [TestCase("a + b", Result = "{[B:][cmp::]{[@:a][calc:+][@:b]}}")]
        [TestCase("a + b + c", Result = "{[B:][cmp::]{{[@:a][calc:+][@:b]}[calc:+][@:c]}}")]
        [TestCase("a * b + c", Result = "{[B:][cmp::]{{[@:a][calc:*][@:b]}[calc:+][@:c]}}")]
        [TestCase("a + b * c", Result = "{[B:][cmp::]{[@:a][calc:+]{[@:b][calc:*][@:c]}}}")]
        [TestCase("( a + b ) * c", Result = "{[B:][cmp::]{{[@:a][calc:+][@:b]}[calc:*][@:c]}}")]
        [TestCase("( ( a + b  * c ) + ( d + e ) ) = f", Result = "{{{[@:a][calc:+]{[@:b][calc:*][@:c]}}[calc:+]{[@:d][calc:+][@:e]}}[cmp:=][@:f]}")]
        public string FilterShouldBuildAbstractSyntaxTree(string query)
        {
            var tokens = SimplexTokenizer.Tokenize(query);
            return ToString(testee.Parse(tokens));
        }

        private static string ToString(TreeNode node)
        {
            var left = (node.Left != null) ? ToString(node.Left) : "";
            var right = (node.Right != null) ? ToString(node.Right) : "";
            return (left == "" && right == "") ? node.Value.ToString() : String.Format("{{{0}{1}{2}}}", left, node.Value.ToString(), right);
        }
    }
}
