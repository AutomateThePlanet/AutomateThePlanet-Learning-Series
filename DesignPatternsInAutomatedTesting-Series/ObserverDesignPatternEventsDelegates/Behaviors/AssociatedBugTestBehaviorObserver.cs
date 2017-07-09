// <copyright file="AssociatedBugTestBehaviorObserver.cs" company="Automate The Planet Ltd.">
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
using PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents;
using PatternsInAutomatedTests.Advanced.Observer.Attributes;

namespace ObserverDesignPatternEventsDelegates.Behaviors
{
    public class AssociatedBugTestBehaviorObserver : BaseTestBehaviorObserver
    {
        protected override void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
            var bugId = TryGetBugId(e.MemberInfo);
            if (bugId.HasValue)
            {
                Console.WriteLine(string.Format("The test '{0}' is associated with bug id: {1}", e.TestContext.TestName, bugId.Value));
            }
            else
            {
                Console.WriteLine(string.Format("The test '{0}' is not associated with any bug id.", e.TestContext.TestName));
            }
        }

        private int? TryGetBugId(MemberInfo memberInfo)
        {
            var knownIssueAttribute = memberInfo.GetCustomAttribute<KnownIssueAttribute>(true);
            var result = knownIssueAttribute == null ? null : (int?)knownIssueAttribute.BugId;
            return result;
        }
    }
}