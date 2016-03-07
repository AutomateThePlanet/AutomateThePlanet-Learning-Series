using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents
{
    public class MSTestExecutionProvider : IExecutionProvider
    {
        public event EventHandler<TestExecutionEventArgs> TestInstantiatedEvent;

        public event EventHandler<TestExecutionEventArgs> PreTestInitEvent;

        public event EventHandler<TestExecutionEventArgs> PostTestInitEvent;

        public event EventHandler<TestExecutionEventArgs> PreTestCleanupEvent;

        public event EventHandler<TestExecutionEventArgs> PostTestCleanupEvent;

        public void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            this.RaiseTestEvent(this.PreTestInitEvent, context, memberInfo);
        }

        public void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
            this.RaiseTestEvent(this.PostTestInitEvent, context, memberInfo);
        }

        public void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            this.RaiseTestEvent(this.PreTestCleanupEvent, context, memberInfo);
        }

        public void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            this.RaiseTestEvent(this.PostTestCleanupEvent, context, memberInfo);
        }

        public void TestInstantiated(MemberInfo memberInfo)
        {
            this.RaiseTestEvent(this.TestInstantiatedEvent, null, memberInfo);
        }

        private void RaiseTestEvent(EventHandler<TestExecutionEventArgs> eventHandler, TestContext testContext, MemberInfo memberInfo)
        {
            if (eventHandler != null)
            {
                eventHandler(this, new TestExecutionEventArgs(testContext, memberInfo));
            }
        }
    }
}