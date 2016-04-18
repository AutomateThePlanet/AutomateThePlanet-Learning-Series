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
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Observer.Attributes;

namespace PatternsInAutomatedTests.Advanced.Observer.Classic.Behaviors
{
    public class AssociatedBugTestBehaviorObserver : BaseTestBehaviorObserver
    {
        public AssociatedBugTestBehaviorObserver(ITestExecutionSubject testExecutionSubject) : base(testExecutionSubject)
        {
        }

        public override void PostTestCleanup(TestContext context, MemberInfo memberInfo)
        {
            int? bugId = this.TryGetBugId(context, memberInfo);
            if (bugId.HasValue)
            {
                Console.WriteLine(string.Format("The test '{0}' is associated with bug id: {1}", context.TestName, bugId.Value));
            }
            else
            {
                Console.WriteLine(string.Format("The test '{0}' is not associated with any bug id.", context.TestName));
            }
        }

        private int? TryGetBugId(TestContext context, MemberInfo memberInfo)
        {
            var knownIssueAttribute = memberInfo.GetCustomAttribute<KnownIssueAttribute>(true);
            int? result = knownIssueAttribute == null ? null : (int?)knownIssueAttribute.BugId;
            return result;
        }
    }
}