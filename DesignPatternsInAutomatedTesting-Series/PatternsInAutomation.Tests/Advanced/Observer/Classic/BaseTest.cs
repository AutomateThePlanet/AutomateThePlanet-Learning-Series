using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Observer.Classic.Behaviors;

namespace PatternsInAutomatedTests.Advanced.Observer.Classic
{
    [TestClass]
    public class BaseTest
    {
        private readonly ITestExecutionSubject currentTestExecutionSubject;
        private TestContext testContextInstance;

        public BaseTest()
        {
            this.currentTestExecutionSubject = new MSTestExecutionSubject();
            this.InitializeTestExecutionBehaviorObservers(this.currentTestExecutionSubject);
            var memberInfo = MethodInfo.GetCurrentMethod();
            this.currentTestExecutionSubject.TestInstantiated(memberInfo);
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
            this.currentTestExecutionSubject.PreTestInit(this.TestContext, memberInfo);
            this.TestInit();
            this.currentTestExecutionSubject.PostTestInit(this.TestContext, memberInfo);
        }

        [TestCleanup]
        public void CoreTestCleanup()
        {
            var memberInfo = GetCurrentExecutionMethodInfo();
            this.currentTestExecutionSubject.PreTestCleanup(this.TestContext, memberInfo);
            this.TestCleanup();
            this.currentTestExecutionSubject.PostTestCleanup(this.TestContext, memberInfo);

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

        private void InitializeTestExecutionBehaviorObservers(ITestExecutionSubject currentTestExecutionSubject)
        {
            new AssociatedBugTestBehaviorObserver(currentTestExecutionSubject);
            new BrowserLaunchTestBehaviorObserver(currentTestExecutionSubject);
            new OwnerTestBehaviorObserver(currentTestExecutionSubject);
        }
    }
}
