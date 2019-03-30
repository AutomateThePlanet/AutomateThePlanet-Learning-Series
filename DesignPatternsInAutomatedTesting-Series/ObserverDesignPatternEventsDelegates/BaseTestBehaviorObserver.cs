// <copyright file="BaseTestBehaviorObserver.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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
using ObserverDesignPatternEventsDelegates;
using PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents
{
    public class BaseTestBehaviorObserver
    {
        public void Subscribe(IExecutionProvider provider)
        {
            provider.TestInstantiatedEvent += TestInstantiated;
            provider.PreTestInitEvent += PreTestInit;
            provider.PostTestInitEvent += PostTestInit;
            provider.PreTestCleanupEvent += PreTestCleanup;
            provider.PostTestCleanupEvent += PostTestCleanup;
        }

        public void Unsubscribe(IExecutionProvider provider)
        {
            provider.TestInstantiatedEvent -= TestInstantiated;
            provider.PreTestInitEvent -= PreTestInit;
            provider.PostTestInitEvent -= PostTestInit;
            provider.PreTestCleanupEvent -= PreTestCleanup;
            provider.PostTestCleanupEvent -= PostTestCleanup;
        }

        protected virtual void TestInstantiated(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PreTestInit(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PostTestInit(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PreTestCleanup(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
        }
    }
}
