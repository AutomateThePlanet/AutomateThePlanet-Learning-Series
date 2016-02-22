using Fidely.Framework.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fidely.Framework.Tests.Integration
{
    [TestFixture]
    public class RegexAutoCompleteItemTest
    {
        [TestCase(null, "description", "", "", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("value", "description", null, "", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("value", "description", "", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("value", null, "", "", Result = "value:")]
        [TestCase("value", "description", "", "", Result = "value:description")]
        public string ConstructorShouldWorkCorrectly(string displayName, string description, string pattern, string complete)
        {
            var isMatchProcedure = (pattern != null) ? (v, o) => v == pattern : (Func<string, MatchingOption, bool>)null;
            var completeProcedure = (complete != null) ? (v, o) => complete : (Func<string, MatchingOption, string>)null;
            var testee = new RegexAutoCompleteItem(displayName, description, isMatchProcedure, completeProcedure);
            return String.Format("{0}:{1}", testee.DisplayName, testee.Description);
        }

        [TestCase]
        public void IsMatchShouldCallIsMatchProcedure()
        {
            var counter = 0;
            Func<string, MatchingOption, bool> proc = (v, o) =>
            {
                counter++;
                return true;
            };

            var testee = new RegexAutoCompleteItem("value", "description", proc, (v, o) => v);
            testee.IsMatch("", new MatchingOption());

            Assert.AreEqual(1, counter);
        }

        [TestCase]
        public void CompleteShouldCallCompleteProcedure()
        {
            var counter = 0;
            Func<string, MatchingOption, string> proc = (v, o) =>
            {
                counter++;
                return v;
            };

            var testee = new RegexAutoCompleteItem("value", "description", (v, o) => true, proc);
            testee.Complete("", new MatchingOption());

            Assert.AreEqual(1, counter);
        }
    }
}
