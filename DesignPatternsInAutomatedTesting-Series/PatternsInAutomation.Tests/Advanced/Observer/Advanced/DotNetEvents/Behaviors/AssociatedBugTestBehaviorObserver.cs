using System;
using System.Reflection;
using PatternsInAutomation.Tests.Advanced.Observer.Attributes;

namespace PatternsInAutomation.Tests.Advanced.Observer.Advanced.DotNetEvents.Behaviors
{
    public class AssociatedBugTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            int? bugId = this.TryGetBugId(e.MemberInfo);
            if (bugId.HasValue)
            {
                Console.WriteLine(string.Format("The test '{0}' is associated with bug id: {1}", e.TestContext.TestName, bugId.Value));
            }
            else
            {
                Console.WriteLine(string.Format("The test '{0}' is not associated with any bug id.", e.TestContext.TestName));
            }
        }

        private int? TryGetBugId(MemberInfo memberInfo)
        {
            var knownIssueAttribute = memberInfo.GetCustomAttribute<KnownIssueAttribute>(true);
            int? result = knownIssueAttribute == null ? null : (int?)knownIssueAttribute.BugId;
            return result;
        }
    }
}