// <copyright file="BaseTest.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace HybridTestFramework.UITests.Core.Behaviours
{
    [TestClass]
    public class BaseTest
    {
        private readonly MSTestExecutionProvider currentTestExecutionProvider;
        private IDriver driver;
        private readonly IUnityContainer container;
        private TestContext testContextInstance;

        public BaseTest()
        {
            this.container = new UnityContainer();
            this.container.RegisterInstance<IUnityContainer>(this.container);
            this.currentTestExecutionProvider = new MSTestExecutionProvider();
            this.InitializeTestExecutionBehaviorObservers(
                this.currentTestExecutionProvider, this.container);
            var memberInfo = MethodInfo.GetCurrentMethod();
            this.currentTestExecutionProvider.TestInstantiated(memberInfo);
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
            this.currentTestExecutionProvider.PreTestInit(this.TestContext, memberInfo);
            this.driver = this.container.Resolve<IDriver>();
            this.TestInit();
            this.currentTestExecutionProvider.PostTestInit(this.TestContext, memberInfo);
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            this.currentTestExecutionProvider.PreTestCleanup(this.TestContext, memberInfo);
            this.TestCleanup();
            this.currentTestExecutionProvider.PostTestCleanup(this.TestContext, memberInfo);
        }

        public virtual void TestInit()
        {
        }

        public virtual void TestCleanup()
        {
        }

        private MethodInfo GetCurrentExecutionMethodInfo()
        {
            var memberInfo = this.GetType().GetMethod(this.TestContext.TestName);
            return memberInfo;
        }

        private void InitializeTestExecutionBehaviorObservers(
            MSTestExecutionProvider currentTestExecutionProvider, 
            IUnityContainer container)
        {
            var executionEngine = new ExecutionEngineBehaviorObserver(container);
            executionEngine.Subscribe(currentTestExecutionProvider);
        }
    }
}