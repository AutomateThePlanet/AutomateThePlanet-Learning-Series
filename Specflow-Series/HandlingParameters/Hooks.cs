// <copyright file="Hooks.cs" company="Automate The Planet Ltd.">
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

using HandlingParameters.Core;
using HandlingParameters.Pages.CelsiusFahrenheitPage;
using HandlingParameters.Pages.HomePage;
using HandlingParameters.Pages.SecondsToMinutesPage;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace HandlingParameters
{
    [Binding]
    public sealed class Hooks
    {
        // Reuse browser for the whole run.
        [BeforeTestRun(Order = 1)]
        public static void RegisterPages()
        {
            System.Console.WriteLine("Execute BeforeTestRun- RegisterPages");
            Driver.StartBrowser(BrowserTypes.Chrome);
            HandlingParameters.Base.UnityContainerFactory.GetContainer().RegisterType<HomePage>(new ContainerControlledLifetimeManager());
            HandlingParameters.Base.UnityContainerFactory.GetContainer().RegisterType<KilowattHoursPage>(new ContainerControlledLifetimeManager());
            HandlingParameters.Base.UnityContainerFactory.GetContainer().RegisterType<SecondsToMinutesPage>(new ContainerControlledLifetimeManager());
        }

        [BeforeTestRun(Order = 2)]
        public static void RegisterDriver()
        {
            System.Console.WriteLine("Execute BeforeTestRun- RegisterDriver");
            HandlingParameters.Base.UnityContainerFactory.GetContainer().RegisterInstance<IWebDriver>(Driver.Browser);
        }

        // Reuse browser for the whole run.
        [AfterTestRun]
        public static void AfterTestRun()
        {
            System.Console.WriteLine("Execute AfterTestRun- StopBrowser");
            Driver.StopBrowser();
        }
    }
}