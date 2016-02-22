using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Advanced.Observer.Advanced.ObservableObserver.Behaviors
{
    public class OwnerTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            this.ThrowExceptionIfOwnerAttributeNotSet(memberInfo);
        }

        private void ThrowExceptionIfOwnerAttributeNotSet(MemberInfo memberInfo)
        {
            try
            {
                memberInfo.GetCustomAttribute<OwnerAttribute>(true);
            }
            catch
            {
                throw new Exception("You have to set Owner of your test before you run it");
            }
        }
    }
}