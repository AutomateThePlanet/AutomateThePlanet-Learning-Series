// <copyright file="ExecutionEngineBehaviorObserver.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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

using Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HybridTestFramework.UITests.Core.Behaviours.TestsEngine.SecondVersion
{
    public class ExecutionEngineBehaviorObserver : BaseTestBehaviorObserver
    {
        private readonly IUnityContainer _unityContainer;
        private Browsers _executionBrowserType;
        private readonly List<HybridTestFramework.Core.Behaviours.Contracts.ITestExecutionEngine> _testExecutionEngines;

        public ExecutionEngineBehaviorObserver(IUnityContainer unityContainer)
        {
            this._unityContainer = unityContainer;
            _testExecutionEngines = this._unityContainer.ResolveAll<HybridTestFramework.Core.Behaviours.Contracts.ITestExecutionEngine>().ToList();
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            foreach (var testExecutionEngine in _testExecutionEngines)
            {
                if (testExecutionEngine != null)
                {
                    testExecutionEngine.Dispose();
                }
            }
        }

        protected override void PreTestInit(object sender, TestExecutionEventArgs e)
        {
            _executionBrowserType = ConfigureTestExecutionBrowser(e.MemberInfo);

            foreach (var testExecutionEngine in _testExecutionEngines)
            {
                if (testExecutionEngine.IsSelected(e.MemberInfo))
                {
                    testExecutionEngine.RegisterDependencies(_executionBrowserType);
                    break;
                }
            }
        }
        
        private Browsers ConfigureTestExecutionBrowser(MemberInfo memberInfo)
        {
            var currentExecutionBrowserType = Browsers.Firefox;
            var methodExecutionBrowser = GetExecutionBrowser(memberInfo);
            var classExecutionBrowser = GetExecutionBrowser(memberInfo.DeclaringType);
            
            if (methodExecutionBrowser != Browsers.NotSet)
            {
                currentExecutionBrowserType = methodExecutionBrowser;
            }
            else
            {
                if (classExecutionBrowser != Browsers.NotSet)
                {
                    currentExecutionBrowserType = classExecutionBrowser;
                }
                else
                {
                    currentExecutionBrowserType = Browsers.InternetExplorer;
                }
            }
        
            return currentExecutionBrowserType;
        }
        
        private Browsers GetExecutionBrowser(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("The test method's info cannot be null.");
            }

            var executionBrowserAttribute = memberInfo.GetCustomAttribute<HybridTestFramework.Core.Behaviours.Contracts.ExecutionEngineAttribute>(true);
            if (executionBrowserAttribute != null)
            {
                return executionBrowserAttribute.Browser;
            }
            return Browsers.NotSet;
        }
        
        private Browsers GetExecutionBrowser(Type currentType)
        {
            if (currentType == null)
            {
                throw new ArgumentNullException("The test method's type cannot be null.");
            }

            var executionBrowserAttribute = currentType.GetCustomAttribute<HybridTestFramework.Core.Behaviours.Contracts.ExecutionEngineAttribute>(true);
            if (executionBrowserAttribute != null)
            {
                return executionBrowserAttribute.Browser;
            }
            return Browsers.NotSet;
        }
    }
}