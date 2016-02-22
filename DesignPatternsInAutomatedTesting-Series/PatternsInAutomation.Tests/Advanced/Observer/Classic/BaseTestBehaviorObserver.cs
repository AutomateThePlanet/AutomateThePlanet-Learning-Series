using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Advanced.Observer.Classic
{
    public class BaseTestBehaviorObserver : ITestBehaviorObserver
    {
        private readonly ITestExecutionSubject testExecutionSubject;

        public BaseTestBehaviorObserver(ITestExecutionSubject testExecutionSubject)
        {
            this.testExecutionSubject = testExecutionSubject;
            testExecutionSubject.Attach(this);
        }

        public virtual void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
        }

        public virtual void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
        }

        public virtual void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
        }

        public virtual void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
        }

        public virtual void TestInstantiated(MemberInfo memberInfo)
        {
        }
    }
}
