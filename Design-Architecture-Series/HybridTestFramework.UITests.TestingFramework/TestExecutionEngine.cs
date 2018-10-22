// <copyright file="TestExecutionEngine.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.Core.Behaviours.Contracts;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
using HybridTestFramework.UITests.TestingFramework.Controls;
using HybridTestFramework.UITests.TestingFramework.Engine;
using Unity;
using System.Collections.Generic;
using Unity;
using Unity.Injection;

namespace HybridTestFramework.UITests.TestingFramework
{
    public class TestExecutionEngine : BaseTestExecutionEngine<TestingFrameworkExecutionEngineAttribute>
    {
        public TestExecutionEngine(IUnityContainer container) : base(container)
        {
        }

        public override void RegisterDependencies(Browsers executionBrowserType)
        {
            var browserSettings = new BrowserSettings(executionBrowserType);
            Driver = new TestingFrameworkDriver(Container, browserSettings);

            Container.RegisterType<IButton, Button>();
            Container.RegisterType<ITextBox, TextBox>();
            Container.RegisterType<IDiv, Div>();
            Container.RegisterType<ISearch, Search>();
            Container.RegisterType<IInputSubmit, InputSubmit>();

            Container.RegisterInstance(Driver);
            Container.RegisterInstance<IBrowser>(Driver);
            Container.RegisterInstance<ICookieService>(Driver);
            Container.RegisterInstance<IDialogService>(Driver);
            Container.RegisterInstance<IJavaScriptInvoker>(Driver);
            Container.RegisterInstance<INavigationService>(Driver);
            Container.RegisterInstance<IElementFinder>(Driver);

            # region 11. Failed Tests Аnalysis - Decorator Design Pattern
            
            Container.RegisterType<IEnumerable<IExceptionAnalysationHandler>, IExceptionAnalysationHandler[]>();
            Container.RegisterType<IUiExceptionAnalyser, UiExceptionAnalyser>();
            Container.RegisterType<IElementFinder, ExceptionAnalyzedElementFinder>(
                new InjectionFactory(x => new ExceptionAnalyzedElementFinder(Driver, Container.Resolve<IUiExceptionAnalyser>())));
            Container.RegisterType<INavigationService, ExceptionAnalyzedNavigationService>(
                new InjectionFactory(x => new ExceptionAnalyzedNavigationService(Driver, Container.Resolve<IUiExceptionAnalyser>())));
        
            #endregion
        }
    }
}