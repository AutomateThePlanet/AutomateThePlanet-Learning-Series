using Fidely.Framework.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fidely.Framework.Tests.Integration
{
    [TestFixture]
    public class AutoCompleteItemTest
    {
        private AutoCompleteItem testee;


        [SetUp]
        public void SetUp()
        {
            testee = new AutoCompleteItem("Test", "Test Description");
        }

        [TestCase(null, "description", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("value", null, Result = "value:")]
        [TestCase("value", "description", Result = "value:description")]
        public string ConstructorShouldWorkCorrectly(string displayValue, string description)
        {
            var item = new AutoCompleteItem(displayValue, description);
            return String.Format("{0}:{1}", item.DisplayName, item.Description);
        }

        [TestCaseSource(typeof(TestCaseDataGenerator), "Generate")]
        public bool IsMatchShouldWorkCorrectly(string value, MatchingOption option)
        {
            return testee.IsMatch(value, option);
        }

        [TestCase(null, Result = "Test")]
        [TestCase("", Result = "Test")]
        [TestCase("t", Result = "Test")]
        [TestCase("T", Result = "Test")]
        [TestCase("test", Result = "Test")]
        [TestCase("TEST", Result = "Test")]
        [TestCase("XXXX", Result = "Test")]
        public string CompleteShouldWorkCorrectly(string value)
        {
            return testee.Complete(value, new MatchingOption());
        }


        private class TestCaseDataGenerator
        {
            public IEnumerable<TestCaseData> Generate()
            {
                yield return new TestCaseData(null, new MatchingOption()).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("", null).Returns(true);

                foreach (var data in GenerateForPartial())
                {
                    yield return data;
                }

                foreach (var data in GenerateForPrefix())
                {
                    yield return data;
                }
            }

            private IEnumerable<TestCaseData> GenerateForPartial()
            {
                var option = new MatchingOption { Mode = MatchingMode.Partial };
                yield return new TestCaseData("", option).Returns(true);
                yield return new TestCaseData("test", option).Returns(true);
                yield return new TestCaseData("Test", option).Returns(true);
                yield return new TestCaseData("TEST", option).Returns(true);
                yield return new TestCaseData("tEST", option).Returns(true);
                yield return new TestCaseData("t", option).Returns(true);
                yield return new TestCaseData("e", option).Returns(true);
                yield return new TestCaseData("s", option).Returns(true);
                yield return new TestCaseData("te", option).Returns(true);
                yield return new TestCaseData("es", option).Returns(true);
                yield return new TestCaseData("st", option).Returns(true);
                yield return new TestCaseData("x", option).Returns(false);
                yield return new TestCaseData("tst", option).Returns(false);
            }

            private IEnumerable<TestCaseData> GenerateForPrefix()
            {
                var option = new MatchingOption { Mode = MatchingMode.Prefix };
                yield return new TestCaseData("", option).Returns(true);
                yield return new TestCaseData("test", option).Returns(true);
                yield return new TestCaseData("Test", option).Returns(true);
                yield return new TestCaseData("TEST", option).Returns(true);
                yield return new TestCaseData("tEST", option).Returns(true);
                yield return new TestCaseData("t", option).Returns(true);
                yield return new TestCaseData("e", option).Returns(false);
                yield return new TestCaseData("s", option).Returns(false);
                yield return new TestCaseData("te", option).Returns(true);
                yield return new TestCaseData("es", option).Returns(false);
                yield return new TestCaseData("st", option).Returns(false);
                yield return new TestCaseData("x", option).Returns(false);
                yield return new TestCaseData("tst", option).Returns(false);
            }
        }
    }
}
