// <copyright file="TestExecutionEngine.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.Core.Behaviours.Contracts;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
using HybridTestFramework.UITests.Selenium.Controls;
using HybridTestFramework.UITests.Selenium.Engine;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Selenium
{
    public class TestExecutionEngine : BaseTestExecutionEngine<WebDriverExecutionEngineAttribute>
    {
        public TestExecutionEngine(IUnityContainer container) : base(container)
        {
        }

        public override void RegisterDependencies(Browsers executionBrowserType)
        {
            var browserSettings = new BrowserSettings(executionBrowserType);
            this.driver = new SeleniumDriver(this.container, browserSettings);

            this.container.RegisterType<IButton, Button>();
            this.container.RegisterType<ITextBox, TextBox>();
            this.container.RegisterType<IDiv, Div>();
            this.container.RegisterType<ISearch, Search>();
            this.container.RegisterType<IInputSubmit, InputSubmit>();

            this.container.RegisterInstance<IDriver>(this.driver);
            this.container.RegisterInstance<IBrowser>(this.driver);
            this.container.RegisterInstance<ICookieService>(this.driver);
            this.container.RegisterInstance<IDialogService>(this.driver);
            this.container.RegisterInstance<IJavaScriptInvoker>(this.driver);
            this.container.RegisterInstance<INavigationService>(this.driver);
            this.container.RegisterInstance<IElementFinder>(this.driver);

            # region 11. Failed Tests Аnalysis - Decorator Design Pattern
            
            this.container.RegisterType<IEnumerable<IExceptionAnalysationHandler>, IExceptionAnalysationHandler[]>();
            this.container.RegisterType<IUiExceptionAnalyser, UiExceptionAnalyser>();
            this.container.RegisterType<IElementFinder, ExceptionAnalyzedElementFinder>(
                new InjectionFactory(x => new ExceptionAnalyzedElementFinder(this.driver, this.container.Resolve<IUiExceptionAnalyser>())));
            this.container.RegisterType<INavigationService, ExceptionAnalyzedNavigationService>(
                new InjectionFactory(x => new ExceptionAnalyzedNavigationService(this.driver, this.container.Resolve<IUiExceptionAnalyser>())));
            
            #endregion
        }
    }
}