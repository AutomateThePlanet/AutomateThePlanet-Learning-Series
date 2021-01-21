// <copyright file="BaseTest.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObserverDesignPatternClassicImplementation.Behaviors;
using OpenQA.Selenium;
using System.Reflection;

namespace ObserverDesignPatternClassicImplementation
{
    [TestClass]
    public class BaseTest
    {
        private readonly ITestExecutionSubject _currentTestExecutionSubject;
        private TestContext _testContextInstance;

        public BaseTest()
        {
            _currentTestExecutionSubject = new MsTestExecutionSubject();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionSubject);
            var memberInfo = MethodBase.GetCurrentMethod();
            _currentTestExecutionSubject.TestInstantiated(memberInfo);
        }

        public string BaseUrl { get; set; }

        public IWebDriver Browser { get; set; }

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

        [ClassInitialize]
        public static void OnClassInitialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void OnClassCleanup()
        {
        }

        [TestInitialize]
        public void CoreTestInit()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            _currentTestExecutionSubject.PreTestInit(TestContext, memberInfo);
            TestInit();
            _currentTestExecutionSubject.PostTestInit(TestContext, memberInfo);
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            _currentTestExecutionSubject.PreTestCleanup(TestContext, memberInfo);
            TestCleanup();
            _currentTestExecutionSubject.PostTestCleanup(TestContext, memberInfo);
        }

        public virtual void TestInit()
        {
        }

        public virtual void TestCleanup()
        {
        }

        private MethodInfo GetCurrentExecutionMethodInfo()
        {
            var memberInfo = GetType().GetMethod(TestContext.TestName);
            return memberInfo;
        }

        private void InitializeTestExecutionBehaviorObservers(ITestExecutionSubject currentTestExecutionSubject)
        {
            new AssociatedBugTestBehaviorObserver(currentTestExecutionSubject);
            new BrowserLaunchTestBehaviorObserver(currentTestExecutionSubject);
            new OwnerTestBehaviorObserver(currentTestExecutionSubject);
        }
    }
}
