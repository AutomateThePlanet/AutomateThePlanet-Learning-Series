// <copyright file="BrowserLaunchTestBehaviorObserver.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObserverDesignPatternIObservableIObserver.Attributes;

namespace ObserverDesignPatternIObservableIObserver.Behaviors
{
    public class BrowserLaunchTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            var browserType = GetExecutionBrowser(memberInfo);
            Driver.StartBrowser(browserType);
        }

        protected override void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            Driver.StopBrowser();
        }

        private BrowserTypes GetExecutionBrowser(MemberInfo memberInfo)
        {
            var result = BrowserTypes.Firefox;
            var classBrowserType = GetExecutionBrowserClassLevel(memberInfo.DeclaringType);
            var methodBrowserType = GetExecutionBrowserMethodLevel(memberInfo);
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
