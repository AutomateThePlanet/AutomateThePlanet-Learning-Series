using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents.Behaviors
{
    public class OwnerTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PreTestInit(object sender, TestExecutionEventArgs e)
        {
            this.ThrowExceptionIfOwnerAttributeNotSet(e.MemberInfo);
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