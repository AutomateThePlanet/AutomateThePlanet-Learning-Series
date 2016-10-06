// <copyright file="basetest.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Behaviours.TestsEngine;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording;
using HybridTestFramework.UITests.Core.Utilities;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace HybridTestFramework.UITests.Core.Behaviours
{
    [TestClass]
    public class BaseTest
    {
        private readonly TestExecutionProvider currentTestExecutionProvider;
        private IDriver driver;
        private readonly IUnityContainer container;
        private TestContext testContextInstance;

        public BaseTest()
        {
            this.container = UnityContainerFactory.GetContainer();
            this.container.RegisterInstance<IUnityContainer>(this.container);
            this.currentTestExecutionProvider = new TestExecutionProvider();
            this.InitializeTestExecutionBehaviorObservers(
                this.currentTestExecutionProvider, this.container);
        }

        public IDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public IUnityContainer Container
        {
            get
            {
                return container;
            }
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public string TestName
        {
            get
            {
                return this.TestContext.TestName;
            }
        }

        [TestInitialize]
        public void CoreTestInit()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            this.currentTestExecutionProvider.PreTestInit(
                (TestOutcome)this.TestContext.CurrentTestOutcome, 
                this.TestContext.TestName, 
                memberInfo);
            this.driver = this.container.Resolve<IDriver>();
            this.TestInit();
            this.currentTestExecutionProvider.PostTestInit(
                (TestOutcome)this.TestContext.CurrentTestOutcome, 
                this.TestContext.TestName, 
                memberInfo);
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            this.currentTestExecutionProvider.PreTestCleanup(
                (TestOutcome)this.TestContext.CurrentTestOutcome, 
                this.TestContext.TestName, 
                memberInfo);
            this.TestCleanup();
            this.currentTestExecutionProvider.PostTestCleanup(
                (TestOutcome)this.TestContext.CurrentTestOutcome, 
                this.TestContext.TestName, 
                memberInfo);
        }

        public virtual void TestInit()
        {
        }

        public virtual void TestCleanup()
        {
        }

        public virtual void InitializeTestExecutionBehaviorObservers(
            TestExecutionProvider testExecutionProvider,
            IUnityContainer container)
        {
            var executionEngine = new ExecutionEngineBehaviorObserver(container);
            var videoRecording = 
                new VideoBehaviorObserver(new MsExpressionEncoderVideoRecorder());
            executionEngine.Subscribe(testExecutionProvider);
            videoRecording.Subscribe(testExecutionProvider);
        }

        private MethodInfo GetCurrentExecutionMethodInfo()
        {
            var memberInfo = this.GetType().GetMethod(this.TestContext.TestName);
            return memberInfo;
        }
    }
}