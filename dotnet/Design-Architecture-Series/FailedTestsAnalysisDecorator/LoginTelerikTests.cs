// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
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

using FailedTestsAnalysisDecorator.Pages;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Behaviours;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.TestsEngine.Enums;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording.Attributes;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording.Enums;
using HybridTestFramework.UITests.Core.Utilities;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.Decorator.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;

namespace FailedTestsAnalysisDecorator
{
    [TestClass,
    ExecutionEngineAttribute(ExecutionEngineType.TestStudio, Browsers.Firefox),
    VideoRecordingAttribute(VideoRecordingMode.DoNotRecord)]
    public class LoginTelerikTests : BaseTest
    {
        public override void TestInit()
        {
            base.TestInit();
            UnityContainerFactory.GetContainer().RegisterType<IExceptionAnalysationHandler, ServiceUnavailableExceptionHandler>(Guid.NewGuid().ToString());
            UnityContainerFactory.GetContainer().RegisterType<IExceptionAnalysationHandler, FileNotFoundExceptionHandler>(Guid.NewGuid().ToString());  
        }

        [TestMethod]
        public void TryToLoginTelerik_Decorator()
        {
            var loginPage = Container.Resolve<LoginPage>();
            loginPage.Navigate();
            loginPage.Login();
            loginPage.Logout();
        }
    }
}