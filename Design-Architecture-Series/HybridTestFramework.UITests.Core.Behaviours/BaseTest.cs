// <copyright file="basetest.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Behaviours.TestsEngine;
using HybridTestFramework.UITests.Core.Behaviours.VideoRecording;
using HybridTestFramework.UITests.Core.Utilities;
using HybridTestFramework.UITests.Core.Utilities.VideoRecording;
using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Unity;

namespace HybridTestFramework.UITests.Core.Behaviours
{
    [TestClass]
    public class BaseTest
    {
        private readonly TestExecutionProvider _currentTestExecutionProvider;
        private IDriver _driver;
        private readonly IUnityContainer _container;
        private TestContext _testContextInstance;

        public BaseTest()
        {
            _container = UnityContainerFactory.GetContainer();
            _container.RegisterInstance(_container);
            _currentTestExecutionProvider = new TestExecutionProvider();
            InitializeTestExecutionBehaviorObservers(
                _currentTestExecutionProvider, _container);
        }

        public IDriver Driver
        {
            get
            {
                return _driver;
            }
        }

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        public string TestName
        {
            get
            {
                return TestContext.TestName;
            }
        }

        [TestInitialize]
        public void CoreTestInit()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            _currentTestExecutionProvider.PreTestInit(
                (TestOutcome)TestContext.CurrentTestOutcome, 
                TestContext.TestName, 
                memberInfo);
            _driver = _container.Resolve<IDriver>();
            TestInit();
            _currentTestExecutionProvider.PostTestInit(
                (TestOutcome)TestContext.CurrentTestOutcome, 
                TestContext.TestName, 
                memberInfo);
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            _currentTestExecutionProvider.PreTestCleanup(
                (TestOutcome)TestContext.CurrentTestOutcome, 
                TestContext.TestName, 
                memberInfo);
            TestCleanup();
            _currentTestExecutionProvider.PostTestCleanup(
                (TestOutcome)TestContext.CurrentTestOutcome, 
                TestContext.TestName, 
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
            ////var videoRecording = 
            ////    new VideoBehaviorObserver(new MsExpressionEncoderVideoRecorder());
            executionEngine.Subscribe(testExecutionProvider);
            ////videoRecording.Subscribe(testExecutionProvider);
        }

        private MethodInfo GetCurrentExecutionMethodInfo()
        {
            var memberInfo = GetType().GetMethod(TestContext.TestName);
            return memberInfo;
        }
    }
}