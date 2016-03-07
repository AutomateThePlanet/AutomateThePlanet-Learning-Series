using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver.Enums;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver
{
    public class MSTestExecutionProvider : IObservable<ExecutionStatus>, IDisposable, ITestExecutionProvider
    {
        private readonly List<IObserver<ExecutionStatus>> testBehaviorObservers;

        public MSTestExecutionProvider()
        {
            this.testBehaviorObservers = new List<IObserver<ExecutionStatus>>();
        }

        public void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            this.NotifyObserversExecutionPhase(context, memberInfo, ExecutionPhases.PreTestInit);
        }

        public void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
            this.NotifyObserversExecutionPhase(context, memberInfo, ExecutionPhases.PostTestInit);
        }

        public void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            this.NotifyObserversExecutionPhase(context, memberInfo, ExecutionPhases.PreTestCleanup);
        }

        public void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            this.NotifyObserversExecutionPhase(context, memberInfo, ExecutionPhases.PostTestCleanup);
        }

        public void TestInstantiated(MemberInfo memberInfo)
        {
            this.NotifyObserversExecutionPhase(null, memberInfo, ExecutionPhases.TestInstantiated);
        }

        public IDisposable Subscribe(IObserver<ExecutionStatus> observer)
        {
            if (!testBehaviorObservers.Contains(observer))
            {
                testBehaviorObservers.Add(observer);
            }
            return new Unsubscriber<ExecutionStatus>(testBehaviorObservers, observer);
        }

        private void NotifyObserversExecutionPhase(TestContext context, MemberInfo memberInfo, ExecutionPhases executionPhase)
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.OnNext(new ExecutionStatus(context, memberInfo, executionPhase));
            }
        }

        public void Dispose()
        {
            foreach (var currentObserver in this.testBehaviorObservers)
            {
                currentObserver.OnCompleted();
            }

            this.testBehaviorObservers.Clear();
        }
    }
}