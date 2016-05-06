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
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObserverDesignPatternEventsDelegates.Behaviors;
using OpenQA.Selenium;

namespace ObserverDesignPatternEventsDelegates
{
    [TestClass]
    public class BaseTest
    {
        private readonly MSTestExecutionProvider currentTestExecutionProvider;
        private TestContext testContextInstance;

        public BaseTest()
        {
            this.currentTestExecutionProvider = new MSTestExecutionProvider();
            this.InitializeTestExecutionBehaviorObservers(this.currentTestExecutionProvider);
            var memberInfo = MethodInfo.GetCurrentMethod();
            this.currentTestExecutionProvider.TestInstantiated(memberInfo);
        }

        public string BaseUrl { get; set; }
        
        public IWebDriver Browser { get; set; }

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

        private void InitializeTestExecutionBehaviorObservers(MSTestExecutionProvider currentTestExecutionProvider)
        {
            new AssociatedBugTestBehaviorObserver().Subscribe(currentTestExecutionProvider);
            new BrowserLaunchTestBehaviorObserver().Subscribe(currentTestExecutionProvider);
            new OwnerTestBehaviorObserver().Subscribe(currentTestExecutionProvider);
        }
    }
}
