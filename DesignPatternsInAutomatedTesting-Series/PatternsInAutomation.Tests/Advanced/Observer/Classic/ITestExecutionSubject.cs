using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Observer.Classic
{
    public interface ITestExecutionSubject
    {
        void Attach(ITestBehaviorObserver observer);

        void Detach(ITestBehaviorObserver observer);

        void PreTestInit(TestContext context, MemberInfo memberInfo);

        void PostTestInit(TestContext context, MemberInfo memberInfo);

        void PreTestCleanup(TestContext context, MemberInfo memberInfo);
        
        void PostTestCleanup(TestContext context, MemberInfo memberInfo);

        void TestInstantiated(MemberInfo memberInfo);
    }
}