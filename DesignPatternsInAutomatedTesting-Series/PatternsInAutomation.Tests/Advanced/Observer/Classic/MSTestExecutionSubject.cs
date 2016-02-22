using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Advanced.Observer.Classic
{
    public class MSTestExecutionSubject : ITestExecutionSubject
    {
        private readonly List<ITestBehaviorObserver> testBehaviorObservers;

        public MSTestExecutionSubject()
        {
            this.testBehaviorObservers = new List<ITestBehaviorObserver>();
        }

        public void Attach(ITestBehaviorObserver observer)
        {
            testBehaviorObservers.Add(observer);
        }

        public void Detach(ITestBehaviorObserver observer)
        {
            testBehaviorObservers.Remove(observer);
        }

        public void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.PreTestInit(context, memberInfo);
            }
        }
        public void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.PostTestInit(context, memberInfo);
            }
        }

        public void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.PreTestCleanup(context, memberInfo);
            }
        }

        public void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.PostTestCleanup(context, memberInfo);
            }
        }

        public void TestInstantiated(MemberInfo memberInfo)
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.TestInstantiated(memberInfo);
            }
        }
    }
}
