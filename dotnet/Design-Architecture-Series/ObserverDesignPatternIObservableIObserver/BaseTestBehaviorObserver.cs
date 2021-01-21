// <copyright file="BaseTestBehaviorObserver.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Observer.Advanced.ObservableObserver.Enums;

namespace ObserverDesignPatternIObservableIObserver
{
    public class BaseTestBehaviorObserver : IObserver<ExecutionStatus>
    {
        private IDisposable _cancellation;

        public virtual void Subscribe(IObservable<ExecutionStatus> provider)
        {
            _cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _cancellation.Dispose();
        }

        public void OnNext(ExecutionStatus currentExecutionStatus)
        {
            switch (currentExecutionStatus.ExecutionPhase)
            {
                case ExecutionPhases.TestInstantiated:
                    TestInstantiated(currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PreTestInit:
                    PreTestInit(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PostTestInit:
                    PostTestInit(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PreTestCleanup:
                    PreTestCleanup(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
                    break;
                case ExecutionPhases.PostTestCleanup:
                    PostTestCleanup(currentExecutionStatus.TestContext, currentExecutionStatus.MemberInfo);
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
