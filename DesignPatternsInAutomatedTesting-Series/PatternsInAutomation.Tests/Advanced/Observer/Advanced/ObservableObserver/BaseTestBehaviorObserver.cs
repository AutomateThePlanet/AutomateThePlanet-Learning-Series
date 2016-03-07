using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver.Enums;
using PatternsInAutomatedTests.Advanced.Observer.Classic;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver
{
    public class BaseTestBehaviorObserver : IObserver<ExecutionStatus>
    {
        private IDisposable cancellation;

        public virtual void Subscribe(IObservable<ExecutionStatus> provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
        }

        public void OnNext(ExecutionStatus currentExecutionStatus)
        {
            switch (currentExecutionStatus.ExecutionPhase)
            {
                case ExecutionPhases.TestInstantiated:
                    this.TestInstantiated(currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PreTestInit:
                    this.PreTestInit(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PostTestInit:
                    this.PostTestInit(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PreTestCleanup:
                    this.PreTestCleanup(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PostTestCleanup:
                    this.PostTestCleanup(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                default:
                    break;
            }
        }

        public virtual void OnError(Exception e)
        {
            Console.WriteLine("The following exception occurred: {0}", e.Message);
        }

        public virtual void OnCompleted()
        {
        }

        protected virtual void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
        }

        protected virtual void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
        }

        protected virtual void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
        }

        protected virtual void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
        }

        protected virtual void TestInstantiated(MemberInfo memberInfo)
        {
        }
    }
}
