// <copyright file="Hooks - Copy.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using ExtendTestExecutionWorkflowUsingHooks.Base;
using ExtendTestExecutionWorkflowUsingHooks.Core;
using ExtendTestExecutionWorkflowUsingHooks.Pages;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace ExtendTestExecutionWorkflowUsingHooks
{
    [Binding]
    public sealed class OrderHooks
    {
        // Reuse browser for the whole run.
        [BeforeTestRun(Order = 1)]
        public static void RegisterPages()
        {
            Console.WriteLine("BeforeTestRun");
            Driver.StartBrowser(BrowserTypes.Chrome);
            UnityContainerFactory.GetContainer().RegisterType<HomePage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<KilowattHoursPage>(new ContainerControlledLifetimeManager());
        }

        [BeforeTestRun(Order = 2)]
        public static void RegisterDriver()
        {
            Console.WriteLine("Execute BeforeTestRun- RegisterDriver");
            UnityContainerFactory.GetContainer().RegisterInstance<IWebDriver>(Driver.Browser);
        }

        // Reuse browser for the whole run.
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("AfterTestRun");
            Driver.StopBrowser();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Console.WriteLine("BeforeFeature");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("AfterFeature");
        }

        [BeforeScenario]
        public void LoginUser()
        {
            Console.WriteLine("BeforeScenario");
        }

        [AfterScenario(Order = 1)]
        public void AfterScenario()
        {
            Console.WriteLine("AfterScenario");
        }

        [BeforeStep]
        public void BeforeStep()
        {
            Console.WriteLine("BeforeStep");
        }

        [AfterStep]
        public void AfterStep()
        {
            Console.WriteLine("AfterStep");
        }
    }
}

////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using TechTalk.SpecFlow;

////namespace ExtendTestExecutionWorkflowUsingHooks
////{
////    [Binding]
////    public sealed class Hooks
////    {
////        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

////        [BeforeScenario]
////        public void BeforeScenario()
////        {
////            //TODO: implement logic that has to run before executing each scenario
////        }

////        [AfterScenario]
////        public void AfterScenario()
////        {
////            //TODO: implement logic that has to run after executing each scenario
////        }
////    }
////}