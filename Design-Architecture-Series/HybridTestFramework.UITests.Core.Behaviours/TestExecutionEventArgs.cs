// <copyright file="testexecutioneventargs.cs" company="Automate The Planet Ltd.">
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
    public class TestExecutionEventArgs : EventArgs
    {
        private readonly TestOutcome testOutcome;
        private readonly MemberInfo memberInfo;
        private readonly string testName;

        public TestExecutionEventArgs(TestOutcome testOutcome, string testName, MemberInfo memberInfo)
        {
            this.testOutcome = testOutcome;
            this.testName = testName;
            this.memberInfo = memberInfo;
        }

        public TestExecutionEventArgs(MemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        public virtual MemberInfo MemberInfo
        {
            get
            {
                return this.memberInfo;
            }
        }

        public virtual TestOutcome TestOutcome
        {
            get
            {
                return this.testOutcome;
            }
        }

        public virtual string TestName
        {
            get
            {
                return this.testName;
            }
        }
    }
}