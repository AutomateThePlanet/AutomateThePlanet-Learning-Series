// <copyright file="MSTestExecutionProvider.cs" company="Automate The Planet Ltd.">
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

namespace ObserverDesignPatternEventsDelegates
{
    public class MsTestExecutionProvider : IExecutionProvider
    {
        public event EventHandler<TestExecutionEventArgs> TestInstantiatedEvent;

        public event EventHandler<TestExecutionEventArgs> PreTestInitEvent;

        public event EventHandler<TestExecutionEventArgs> PostTestInitEvent;

        public event EventHandler<TestExecutionEventArgs> PreTestCleanupEvent;

        public event EventHandler<TestExecutionEventArgs> PostTestCleanupEvent;

        public void PreTestInit(TestContext context, MemberInfo memberInfo)
        {
            RaiseTestEvent(PreTestInitEvent, context, memberInfo);
        }

        public void PostTestInit(TestContext context, MemberInfo memberInfo)
        {
            RaiseTestEvent(PostTestInitEvent, context, memberInfo);
        }

        public void PreTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            RaiseTestEvent(PreTestCleanupEvent, context, memberInfo);
        }

        public void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            RaiseTestEvent(PostTestCleanupEvent, context, memberInfo);
        }

        public void TestInstantiated(MemberInfo memberInfo)
        {
            RaiseTestEvent(TestInstantiatedEvent, null, memberInfo);
        }

        private void RaiseTestEvent(EventHandler<TestExecutionEventArgs> eventHandler, TestContext testContext, MemberInfo memberInfo)
        {
            if (eventHandler != null)
            {
                eventHandler(this, new TestExecutionEventArgs(testContext, memberInfo));
            }
        }
    }
}