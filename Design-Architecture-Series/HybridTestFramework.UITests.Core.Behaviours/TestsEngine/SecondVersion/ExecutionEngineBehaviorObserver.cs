// <copyright file="ExecutionEngineBehaviorObserver.cs" company="Automate The Planet Ltd.">
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

using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HybridTestFramework.UITests.Core.Behaviours.TestsEngine.SecondVersion
{
    public class ExecutionEngineBehaviorObserver : BaseTestBehaviorObserver
    {
        private readonly IUnityContainer unityContainer;
        private Browsers executionBrowserType;
        private readonly List<HybridTestFramework.Core.Behaviours.Contracts.ITestExecutionEngine> testExecutionEngines;

        public ExecutionEngineBehaviorObserver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            this.testExecutionEngines = this.unityContainer.ResolveAll<HybridTestFramework.Core.Behaviours.Contracts.ITestExecutionEngine>().ToList();
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            foreach (var testExecutionEngine in this.testExecutionEngines)
            {
                if (testExecutionEngine != null)
                {
                    testExecutionEngine.Dispose();
                }
            }
        }

        protected override void PreTestInit(object sender, TestExecutionEventArgs e)
        {
            this.executionBrowserType = this.ConfigureTestExecutionBrowser(e.MemberInfo);

            foreach (var testExecutionEngine in this.testExecutionEngines)
            {
                if (testExecutionEngine.IsSelected(e.MemberInfo))
                {
                    testExecutionEngine.RegisterDependencies(executionBrowserType);
                    break;
                }
            }
        }
        
        private Browsers ConfigureTestExecutionBrowser(MemberInfo memberInfo)
        {
            var currentExecutionBrowserType = Browsers.Firefox;
            Browsers methodExecutionBrowser = this.GetExecutionBrowser(memberInfo);
            Browsers classExecutionBrowser = this.GetExecutionBrowser(memberInfo.DeclaringType);
            
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