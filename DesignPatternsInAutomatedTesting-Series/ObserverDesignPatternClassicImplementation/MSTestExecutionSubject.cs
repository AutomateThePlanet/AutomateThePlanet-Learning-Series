// <copyright file="MSTestExecutionSubject.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObserverDesignPatternClassicImplementation
{
    public class MsTestExecutionSubject : ITestExecutionSubject
    {
        private readonly List<ITestBehaviorObserver> _testBehaviorObservers;

        public MsTestExecutionSubject()
        {
            _testBehaviorObservers = new List<ITestBehaviorObserver>();
        }

        public void Attach(ITestBehaviorObserver observer)
        {
            _testBehaviorObservers.Add(observer);
        }

        public void Detach(ITestBehaviorObserver observer)
        {
            _testBehaviorObservers.Remove(observer);
        }

        public void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in _testBehaviorObservers)
            {
                currentObserver.PreTestInit(context, memberInfo);
            }
        }
        public void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in _testBehaviorObservers)
            {
                currentObserver.PostTestInit(context, memberInfo);
            }
        }

        public void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in _testBehaviorObservers)
            {
                currentObserver.PreTestCleanup(context, memberInfo);
            }
        }

        public void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            foreach (var currentObserver in _testBehaviorObservers)
            {
                currentObserver.PostTestCleanup(context, memberInfo);
            }
        }

        public void TestInstantiated(MemberInfo memberInfo)
        {
            foreach (var currentObserver in _testBehaviorObservers)
            {
                currentObserver.TestInstantiated(memberInfo);
            }
        }
    }
}
