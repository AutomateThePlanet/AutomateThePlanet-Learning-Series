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

using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Enums;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Selenium.Engine;
using HybridTestFramework.UITests.TestingFramework.Engine;
using Microsoft.Practices.Unity;
using System;
using System.Reflection;
using SeleniumControls = HybridTestFramework.UITests.Selenium.Controls;
using TestingFrameworkControls = HybridTestFramework.UITests.TestingFramework.Controls;

namespace HybridTestFramework.UITests.Core.Behaviours.TestsEngine
{
    public class ExecutionEngineBehaviorObserver : BaseTestBehaviorObserver
    {
        private readonly IUnityContainer unityContainer;
        private ExecutionEngineType executionEngineType;
        private Browsers executionBrowserType;
        private IDriver driver;

        public ExecutionEngineBehaviorObserver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            this.driver.Quit();
        }

        protected override void PreTestInit(object sender, TestExecutionEventArgs e)
        {
            this.executionBrowserType = this.ConfigureTestExecutionBrowser(e.MemberInfo);
            this.executionEngineType = this.GetExecutionEngineType(e.MemberInfo);
            this.ResolveAllDriverDependencies();
        }

        private ExecutionEngineType GetExecutionEngineTypeByMethodInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("The test method's info cannot be null.");
            }

            var executionEngineTypeMethodAttribute = memberInfo.GetCustomAttribute<ExecutionEngineAttribute>();
            if (executionEngineTypeMethodAttribute != null)
            {
                return executionEngineTypeMethodAttribute.ExecutionEngineType;
            }
            return ExecutionEngineType.NotSpecified;
        }

        private ExecutionEngineType GetExecutionEngineType(Type currentType)
        {
            if (currentType == null)
            {
                throw new ArgumentNullException("The test method's type cannot be null.");
            }

            var executionEngineClassAttribute = currentType.GetCustomAttribute<ExecutionEngineAttribute>(true);
            if (executionEngineClassAttribute != null)
            {
                return executionEngineClassAttribute.ExecutionEngineType;
            }
            return ExecutionEngineType.NotSpecified;
        }

        private ExecutionEngineType GetExecutionEngineType(MemberInfo memberInfo)
        {
            var executionEngineType = ExecutionEngineType.TestStudio;

            ExecutionEngineType methodExecutionEngineType = this.GetExecutionEngineTypeByMethodInfo(memberInfo);
            ExecutionEngineType classExecutionEngineType = this.GetExecutionEngineType(memberInfo.DeclaringType);

            if (methodExecutionEngineType != ExecutionEngineType.NotSpecified)
            {
                executionEngineType = methodExecutionEngineType;
            }
            else if (classExecutionEngineType != ExecutionEngineType.NotSpecified)
            {
                executionEngineType = classExecutionEngineType;
            }

            return executionEngineType;
        }

        private void ResolveAllDriverDependencies()
        {
            var browserSettings = new BrowserSettings(this.executionBrowserType);
            if (this.executionEngineType.Equals(ExecutionEngineType.TestStudio))
            { 
                this.unityContainer.RegisterType<IDriver, TestingFrameworkDriver>(
                    new InjectionFactory(x => new TestingFrameworkDriver(this.unityContainer, browserSettings)));
                this.driver = this.unityContainer.Resolve<IDriver>();

                this.unityContainer.RegisterType<IButton, TestingFrameworkControls.Button>();
                this.unityContainer.RegisterType<ITextBox, TestingFrameworkControls.TextBox>();
                this.unityContainer.RegisterType<IDiv, TestingFrameworkControls.Div>();
                this.unityContainer.RegisterType<ISearch, TestingFrameworkControls.Search>();
                this.unityContainer.RegisterType<IInputSubmit, TestingFrameworkControls.InputSubmit>();
            }
            else if (this.executionEngineType.Equals(ExecutionEngineType.WebDriver))
            {
                this.unityContainer.RegisterType<IDriver, SeleniumDriver>(
                    new InjectionFactory(x => new SeleniumDriver(this.unityContainer, browserSettings)));
                this.driver = this.unityContainer.Resolve<IDriver>();

                this.unityContainer.RegisterType<IButton, SeleniumControls.Button>();
                this.unityContainer.RegisterType<ITextBox, SeleniumControls.TextBox>();
                this.unityContainer.RegisterType<IDiv, SeleniumControls.Div>();
                this.unityContainer.RegisterType<ISearch, SeleniumControls.Search>();
                this.unityContainer.RegisterType<IInputSubmit, SeleniumControls.InputSubmit>();
            }

            this.unityContainer.RegisterInstance<IDriver>(this.driver);
            this.unityContainer.RegisterInstance<IBrowser>(this.driver);
            this.unityContainer.RegisterInstance<ICookieService>(this.driver);
            this.unityContainer.RegisterInstance<IDialogService>(this.driver);
            this.unityContainer.RegisterInstance<IJavaScriptInvoker>(this.driver);
            this.unityContainer.RegisterInstance<INavigationService>(this.driver);
            this.unityContainer.RegisterInstance<IElementFinder>(this.driver);
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

            var executionBrowserAttribute = memberInfo.GetCustomAttribute<ExecutionEngineAttribute>(true);
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

            var executionBrowserAttribute = currentType.GetCustomAttribute<ExecutionEngineAttribute>(true);
            if (executionBrowserAttribute != null)
            {
                return executionBrowserAttribute.Browser;
            }
            return Browsers.NotSet;
        }
    }
}