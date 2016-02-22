using Fidely.Framework.Compilation.Evaluators;
using Fidely.Framework.Tests.Instrumentation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fidely.Framework.Tests.Compilation.Evaluators
{
    [TestFixture]
    public class StaticVariableEvaluatorTest
    {
        private StaticVariableEvaluator testee;

        private Dictionary<string, Func<object>> evaluators;


        [SetUp]
        public void SetUp()
        {
            testee = new StaticVariableEvaluator(new OperandBuilderImpl());
            evaluators = testee.GetType().GetField("evaluators", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(testee) as Dictionary<string, Func<object>>;
        }

        [Test]
        public void RegisterVariableShouldThrowExceptionWhenProcedureIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => testee.RegisterVariable("", null));
        }

        [TestCase(null, "", "", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("a", 0, "description")]
        public void RegisterVariableShouldWorkCorrectly(string name, object value, string description)
        {
            testee.RegisterVariable(name, () => value, description);

            Assert.AreEqual(1, evaluators.Count);
            Assert.AreSame(value, evaluators[name.ToUpperInvariant()].Invoke());
            Assert.AreEqual(1, testee.AutocompleteItems.Count());
            Assert.AreEqual(name, testee.AutocompleteItems.ElementAt(0).DisplayName);
            Assert.AreEqual(description, testee.AutocompleteItems.ElementAt(0).Description);
        }

        [Test]
        public void RegisterVariableShouldThrowExceptionWhenSpecifiedVariableNameHaveBeAlreadyRegistered()
        {
            testee.RegisterVariable("x", () => 0);
            Assert.Throws<ArgumentException>(() => testee.RegisterVariable("x", () => 1));
        }

        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("x", Result = 10)]
        [TestCase("X", Result = 10)]
        [TestCase("y", Result = "foo")]
        [TestCase("Y", Result = "foo")]
        [TestCase("z", Result = true)]
        [TestCase("Z", Result = true)]
        public object EvaluateShouldWorkCorrectly(string name)
        {
            testee.RegisterVariable("x", () => 10);
            testee.RegisterVariable("y", () => "foo");
            testee.RegisterVariable("z", () => true);

            var operand = testee.Evaluate(null, name);

            return Expression.Lambda(operand.Expression).Compile().DynamicInvoke();
        }

        [Test]
        public void EvaluateShouldReturnNullWhenSpecifiedVariableIsNotRegistered()
        {
            Assert.IsNull(testee.Evaluate(null, "x"));
        }
    }
}
