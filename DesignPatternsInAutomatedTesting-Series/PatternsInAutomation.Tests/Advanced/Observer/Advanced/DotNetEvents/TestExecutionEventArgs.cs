using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents
{
    public class TestExecutionEventArgs : EventArgs
    {
        private readonly TestContext testContext;
        private readonly MemberInfo memberInfo;

        public TestExecutionEventArgs(TestContext context, MemberInfo memberInfo)
        {
            this.testContext = context;
            this.memberInfo = memberInfo;
        }

        public MemberInfo MemberInfo
        {
            get
            {
                return this.memberInfo;
            }
        }

        public TestContext TestContext
        {
            get
            {
                return this.testContext;
            }
        }
    }
}