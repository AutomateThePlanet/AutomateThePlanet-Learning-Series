using System;
using System.Reflection;
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.Observer.Attributes;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents.Behaviors
{
    public class BrowserLaunchTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PreTestInit(object sender, TestExecutionEventArgs e)
        {
            var browserType = this.GetExecutionBrowser(e.MemberInfo);
            Driver.StartBrowser(browserType);
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
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
