// <copyright file="mstestexecutionprovider.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;

namespace HybridTestFramework.UITests.Core.Behaviours
{
    public class TestExecutionProvider : IExecutionProvider
    {
        public event EventHandler<TestExecutionEventArgs> PreTestInitEvent;

        public event EventHandler<TestExecutionEventArgs> PostTestInitEvent;

        public event EventHandler<TestExecutionEventArgs> PreTestCleanupEvent;

        public event EventHandler<TestExecutionEventArgs> PostTestCleanupEvent;

        public void PreTestInit(TestOutcome testOutcome, string testName, MemberInfo memberInfo)
        {
            RaiseTestEvent(PreTestInitEvent, testOutcome, testName, memberInfo);
        }

        public void PostTestInit(TestOutcome testOutcome, string testName, MemberInfo memberInfo)
        {
            RaiseTestEvent(PostTestInitEvent, testOutcome, testName, memberInfo);
        }

        public void PreTestCleanup(TestOutcome testOutcome, string testName, MemberInfo memberInfo)
        {
            RaiseTestEvent(PreTestCleanupEvent, testOutcome, testName, memberInfo);
        }

        public void PostTestCleanup(TestOutcome testOutcome, string testName, MemberInfo memberInfo)
        {
            RaiseTestEvent(PostTestCleanupEvent, testOutcome, testName, memberInfo);
        }

        private void RaiseTestEvent(EventHandler<TestExecutionEventArgs> eventHandler, TestOutcome testOutcome, string testName, MemberInfo memberInfo)
        {
            if (eventHandler != null)
            {
                // add decorator around the test context instance + interface where properties will be get when it is not null
                eventHandler(this, new TestExecutionEventArgs(testOutcome, testName, memberInfo));
            }
        }
    }
}