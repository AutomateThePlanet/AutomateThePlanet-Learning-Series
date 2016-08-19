using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HybridTestFramework.UITests.Core.Behaviours
{
    public class TestContextTestExecutionEventArgsAdapter : TestExecutionEventArgs
    {
        // watch about adapter and bridge.
        private TestContext testContext;

        public TestContextTestExecutionEventArgsAdapter(TestContext testContext, MemberInfo memberInfo) : base(memberInfo)
        {
            this.testContext = testContext;
        }

        public override string TestName
        {
            get
            {
                return this.testContext.TestName;
            }
        }

        public override TestOutcome TestOutcome
        {
            get
            {
                return (TestOutcome)this.testContext.CurrentTestOutcome;
            }
        }
    }
}