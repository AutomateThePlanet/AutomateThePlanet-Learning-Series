using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HybridTestFramework.UITests.Core.Behaviours
{
    public class TestContextTestExecutionEventArgsAdapter : TestExecutionEventArgs
    {
        // watch about adapter and bridge.
        private TestContext _testContext;

        public TestContextTestExecutionEventArgsAdapter(TestContext testContext, MemberInfo memberInfo) : base(memberInfo)
        {
            this._testContext = testContext;
        }

        public override string TestName
        {
            get
            {
                return _testContext.TestName;
            }
        }

        public override TestOutcome TestOutcome
        {
            get
            {
                return (TestOutcome)_testContext.CurrentTestOutcome;
            }
        }
    }
}