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

using System.Collections.Generic;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Enums;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Utilities;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.AmbientContext;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.ChainOfResponsibility;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
using HybridTestFramework.UITests.Selenium.Engine;
using HybridTestFramework.UITests.TestingFramework.Engine;
using Unity;
using System;
using System.Reflection;
using Unity;
using Unity.Injection;
using SeleniumControls = HybridTestFramework.UITests.Selenium.Controls;
using TestingFrameworkControls = HybridTestFramework.UITests.TestingFramework.Controls;

namespace HybridTestFramework.UITests.Core.Behaviours.TestsEngine
{
    public class ExecutionEngineBehaviorObserver : BaseTestBehaviorObserver
    {
        private readonly IUnityContainer _unityContainer;
        private ExecutionEngineType _executionEngineType;
        private Browsers _executionBrowserType;
        private IDriver _driver;

        public ExecutionEngineBehaviorObserver(IUnityContainer unityContainer)
        {
            this._unityContainer = unityContainer;
        }

        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            _driver.Quit();
        }

        protected override void PreTestInit(object sender, TestExecutionEventArgs e)
        {
            _executionBrowserType = ConfigureTestExecutionBrowser(e.MemberInfo);
            _executionEngineType = GetExecutionEngineType(e.MemberInfo);
            ResolveAllDriverDependencies();
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

            var methodExecutionEngineType = GetExecutionEngineTypeByMethodInfo(memberInfo);
            var classExecutionEngineType = GetExecutionEngineType(memberInfo.DeclaringType);

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
            var browserSettings = new BrowserSettings(_executionBrowserType);
            if (_executionEngineType.Equals(ExecutionEngineType.TestStudio))
            {
                #region Default Registration
                
                _unityContainer.RegisterType<IDriver, TestingFrameworkDriver>(
                    new InjectionFactory(x => new TestingFrameworkDriver(_unityContainer, browserSettings)));
                
                #endregion
                
                # region 9. Failed Tests Аnalysis- Chain of Responsibility Design Pattern
                
                ////ServiceUnavailableExceptionHandler serviceUnavailableExceptionHandler = new ServiceUnavailableExceptionHandler();
                ////var exceptionAnalyzer = new HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.ChainOfResponsibility.ExceptionAnalyzer(serviceUnavailableExceptionHandler);
                ////this.unityContainer.RegisterInstance<IExceptionAnalyzer>(exceptionAnalyzer);
                ////this.unityContainer.RegisterType<IDriver, TestingFrameworkDriver>(
                ////    new InjectionFactory(x => new TestingFrameworkDriver(
                ////        this.unityContainer,
                ////        browserSettings,
                ////        exceptionAnalyzer)));
                
                #endregion
                
                # region 10. Failed Tests Аnalysis- Ambient Context Design Pattern
                
                ////UnityContainerFactory.GetContainer().RegisterType<FileNotFoundExceptionHandler>(ExceptionAnalyzerConstants.MainApplicationHandlerName);
                ////var mainHandler = UnityContainerFactory.GetContainer().Resolve<FileNotFoundExceptionHandler>();
                ////UnityContainerFactory.GetContainer().RegisterInstance<Handler>(ExceptionAnalyzerConstants.MainApplicationHandlerName, mainHandler, new HierarchicalLifetimeManager());
                
                #endregion
                
                _driver = _unityContainer.Resolve<IDriver>();
                
                _unityContainer.RegisterType<IButton, TestingFrameworkControls.Button>();
                _unityContainer.RegisterType<ITextBox, TestingFrameworkControls.TextBox>();
                _unityContainer.RegisterType<IDiv, TestingFrameworkControls.Div>();
                _unityContainer.RegisterType<ISearch, TestingFrameworkControls.Search>();
                _unityContainer.RegisterType<IInputSubmit, TestingFrameworkControls.InputSubmit>();
            }
            else if (_executionEngineType.Equals(ExecutionEngineType.WebDriver))
            {
                _unityContainer.RegisterType<IDriver, SeleniumDriver>(
                    new InjectionFactory(x => new SeleniumDriver(_unityContainer, browserSettings)));
                _driver = _unityContainer.Resolve<IDriver>();
                
                _unityContainer.RegisterType<IButton, SeleniumControls.Button>();
                _unityContainer.RegisterType<ITextBox, SeleniumControls.TextBox>();
                _unityContainer.RegisterType<IDiv, SeleniumControls.Div>();
                _unityContainer.RegisterType<ISearch, SeleniumControls.Search>();
                _unityContainer.RegisterType<IInputSubmit, SeleniumControls.InputSubmit>();
            }
            
            _unityContainer.RegisterInstance(_driver);
            _unityContainer.RegisterInstance<IBrowser>(_driver);
            _unityContainer.RegisterInstance<ICookieService>(_driver);
            _unityContainer.RegisterInstance<IDialogService>(_driver);
            _unityContainer.RegisterInstance<IJavaScriptInvoker>(_driver);
            _unityContainer.RegisterInstance<INavigationService>(_driver);
            _unityContainer.RegisterInstance<IElementFinder>(_driver);
            
            # region 11. Failed Tests Аnalysis - Decorator Design Pattern
            
            _unityContainer.RegisterType<IEnumerable<IExceptionAnalysationHandler>, IExceptionAnalysationHandler[]>();
            _unityContainer.RegisterType<IUiExceptionAnalyser, UiExceptionAnalyser>();
            _unityContainer.RegisterType<IElementFinder, ExceptionAnalyzedElementFinder>(
                new InjectionFactory(x => new ExceptionAnalyzedElementFinder(_driver, _unityContainer.Resolve<IUiExceptionAnalyser>())));
            _unityContainer.RegisterType<INavigationService, ExceptionAnalyzedNavigationService>(
                new InjectionFactory(x => new ExceptionAnalyzedNavigationService(_driver, _unityContainer.Resolve<IUiExceptionAnalyser>())));
        
            #endregion
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