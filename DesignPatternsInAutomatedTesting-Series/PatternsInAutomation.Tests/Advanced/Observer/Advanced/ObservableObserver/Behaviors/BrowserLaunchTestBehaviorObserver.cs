using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;
using PatternsInAutomation.Tests.Advanced.Observer.Attributes;

namespace PatternsInAutomation.Tests.Advanced.Observer.Advanced.ObservableObserver.Behaviors
{
    public class BrowserLaunchTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            var browserType = this.GetExecutionBrowser(memberInfo);
            Driver.StartBrowser(browserType);
        }

        protected override void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            Driver.StopBrowser();
        }

        private BrowserTypes GetExecutionBrowser(MemberInfo memberInfo)
        {
            BrowserTypes result = BrowserTypes.Firefox;
            BrowserTypes classBrowserType = this.GetExecutionBrowserClassLevel(memberInfo.DeclaringType);
            BrowserTypes methodBrowserType = this.GetExecutionBrowserMethodLevel(memberInfo);
            if (methodBrowserType != BrowserTypes.NotSet)
            {
                result = methodBrowserType;
            }
            else if (classBrowserType != BrowserTypes.NotSet)
            {
                result = classBrowserType;
            }
            return result;
        }

        private BrowserTypes GetExecutionBrowserMethodLevel(MemberInfo memberInfo)
        {
            var executionBrowserAttribute = memberInfo.GetCustomAttribute<ExecutionBrowserAttribute>(true);
            if (executionBrowserAttribute != null)
            {
                return executionBrowserAttribute.BrowserType;
            }
            return BrowserTypes.NotSet;
        }

        private BrowserTypes GetExecutionBrowserClassLevel(Type type)
        {
            var executionBrowserAttribute = type.GetCustomAttribute<ExecutionBrowserAttribute>(true);
            if (executionBrowserAttribute != null)
            {
                return executionBrowserAttribute.BrowserType;
            }
            return BrowserTypes.NotSet;
        }
    }
}
