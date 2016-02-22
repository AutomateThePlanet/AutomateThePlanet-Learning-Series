using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Advanced.Observer.Classic.Behaviors
{
    public class OwnerTestBehaviorObserver : BaseTestBehaviorObserver
    {
        public OwnerTestBehaviorObserver(ITestExecutionSubject testExecutionSubject)
            : base(testExecutionSubject)
        {
        }

        public override void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            this.ThrowExceptionIfOwnerAttributeNotSet(memberInfo);
        }

        private void ThrowExceptionIfOwnerAttributeNotSet(MemberInfo memberInfo)
        {
            try
            {
                var ownerAttribute = memberInfo.GetCustomAttribute<OwnerAttribute>(true);
            }
            catch
            {
                throw new Exception("You have to set Owner of your test before you run it");
            }
        }
    }
}