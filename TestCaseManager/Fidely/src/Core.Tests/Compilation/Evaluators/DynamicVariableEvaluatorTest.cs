using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Integration;
using Fidely.Framework.Tests.Instrumentation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Fidely.Framework.Tests.Compilation.Evaluators
{
    [TestFixture]
    public class DynamicVariableEvaluatorTest
    {
        private DynamicVariableEvaluator testee;

        private Dictionary<Regex, Func<Match, object>> evaluators;


        [SetUp]
        public void SetUp()
        {
            testee = new DynamicVariableEvaluator(new OperandBuilderImpl());
            evaluators = testee.GetType().GetField("evaluators", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(testee) as Dictionary<Regex, Func<Match, object>>;
        }


        [TestCase(null, "", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("a", 0)]
        public void RegisterVariableShouldWorkCorrectly(string pattern, object value)
        {
            var item = new RegexAutoCompleteItem("", "", (v, o) => true, (v, o) => v);
            testee.RegisterVariable(pattern, o => value, item);

            Assert.AreEqual(1, evaluators.Count);
            Assert.AreEqual(1, testee.AutocompleteItems.Count());
            Assert.AreSame(item, testee.AutocompleteItems.ElementAt(0));
        }

        [Test]
        public void RegisterVariableShouldThrowExcepionWhenRegexIsNull()
        {
            Regex regex = null;
            var item = new RegexAutoCompleteItem("", "", (v, o) => true, (v, o) => v);
            Assert.Throws<ArgumentNullException>(() => testee.RegisterVariable(regex, o => o, item));
        }

        [Test]
        public void RegisterVariableShouldThrowExceptionWhenProcedureIsNull()
        {
            var regex = new Regex("");
            var item = new RegexAutoCompleteItem("", "", (v, o) => true, (v, o) => v);
            Assert.Throws<ArgumentNullException>(() => testee.RegisterVariable(regex, null, item));
        }

        [Test]
        public void RegisterVariableShouldThrowExceptionWhenAutoCompleteItemIsNull()
        {
            var regex = new Regex("");
            Assert.Throws<ArgumentNullException>(() => testee.RegisterVariable(regex, o => o, null));
        }

        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("x", Result = 10)]
        [TestCase("xyz", Result = 10)]
        [TestCase("y", Result = "foo")]
        [TestCase("yzx", Result = "foo")]
        [TestCase("z", Result = true)]
        [TestCase("zxy", Result = true)]
        public object EvaluateShouldWorkCorrectly(string value)
        {
            var item = new RegexAutoCompleteItem("", "", (v, o) => true, (v, o) => v);
            testee.RegisterVariable("^x.*$", o => 10, item);
            testee.RegisterVariable("^y.*$", o => "foo", item);
            testee.RegisterVariable("^z.*$", o => true, item);

            var operand = testee.Evaluate(null, value);

            return Expression.Lambda(operand.Expression).Compile().DynamicInvoke();
        }

        [TestCase("X")]
        [TestCase("Y")]
        [TestCase("Z")]
        public void EvaluateShouldReturnNullWhenSpecifiedVariableIsNotMatchedAnyPattnens(string value)
        {
            var item = new RegexAutoCompleteItem("", "", (v, o) => true, (v, o) => v);
            testee.RegisterVariable("^x.*$", o => 10, item);
            testee.RegisterVariable("^y.*$", o => "foo", item);
            testee.RegisterVariable("^z.*$", o => true, item);

            Assert.IsNull(testee.Evaluate(null, value));
        }
    }
}
