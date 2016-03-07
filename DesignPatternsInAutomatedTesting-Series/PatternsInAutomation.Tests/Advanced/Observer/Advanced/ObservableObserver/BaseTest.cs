using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver.Behaviors;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver
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
