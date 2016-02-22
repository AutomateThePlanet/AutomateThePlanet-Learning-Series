using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Observer.Attributes;

namespace PatternsInAutomation.Tests.Advanced.Observer.Classic.Behaviors
{
    public class AssociatedBugTestBehaviorObserver : BaseTestBehaviorObserver
    {
        public AssociatedBugTestBehaviorObserver(ITestExecutionSubject testExecutionSubject) : base(testExecutionSubject)
        {
        }

        public override void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            int? bugId = this.TryGetBugId(context, memberInfo);
            if (bugId.HasValue)
            {
                Console.WriteLine(string.Format("The test '{0}' is associated with bug id: {1}", context.TestName, bugId.Value));
            }
            else
            {
                Console.WriteLine(string.Format("The test '{0}' is not associated with any bug id.", context.TestName));
            }
        }

        private int? TryGetBugId(TestContext context, MemberInfo memberInfo)
        {
            var knownIssueAttribute = memberInfo.GetCustomAttribute<KnownIssueAttribute>(true);
            int? result = knownIssueAttribute == null ? null : (int?)knownIssueAttribute.BugId;
            return result;
        }
    }
}