// <copyright file="BaseTest.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObserverDesignPatternIObservableIObserver.Behaviors;
using OpenQA.Selenium;

namespace ObserverDesignPatternIObservableIObserver
{
    [TestClass]
    public class BaseTest
    {
        private readonly MsTestExecutionProvider _currentTestExecutionProvider;
        private TestContext _testContextInstance;

        public BaseTest()
        {
            _currentTestExecutionProvider = new MsTestExecutionProvider();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
            var memberInfo = MethodBase.GetCurrentMethod();
            _currentTestExecutionProvider.TestInstantiated(memberInfo);
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
            _currentTestExecutionProvider.PreTestInit(TestContext, memberInfo);
            TestInit();
            _currentTestExecutionProvider.PostTestInit(TestContext, memberInfo);
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            _currentTestExecutionProvider.PreTestCleanup(TestContext, memberInfo);
            TestCleanup();
            _currentTestExecutionProvider.PostTestCleanup(TestContext, memberInfo);
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

        private void InitializeTestExecutionBehaviorObservers(MsTestExecutionProvider currentTestExecutionProvider)
        {
            new AssociatedBugTestBehaviorObserver().Subscribe(currentTestExecutionProvider);
            new BrowserLaunchTestBehaviorObserver().Subscribe(currentTestExecutionProvider);
            new OwnerTestBehaviorObserver().Subscribe(currentTestExecutionProvider);
        }
    }
}
