// <copyright file="MSTestExecutionProvider.cs" company="Automate The Planet Ltd.">
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