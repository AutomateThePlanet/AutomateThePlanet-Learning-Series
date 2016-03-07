using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver.Enums;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver
{
    public class ExecutionStatus
    {
        public TestContext TestContext { get; set; }

        public MemberInfo MemberInfo { get; set; }
        
        public ExecutionPhases ExecutionPhase { get; set; }

        public ExecutionStatus(TestContext testContext, ExecutionPhases executionPhase) : this(testContext, null, executionPhase)
        {
        }

        public ExecutionStatus(TestContext testContext, MemberInfo memberInfo, ExecutionPhases executionPhase)
        {
            this.TestContext = testContext;
            this.MemberInfo = memberInfo;
            this.ExecutionPhase = executionPhase;
        }
    }
}