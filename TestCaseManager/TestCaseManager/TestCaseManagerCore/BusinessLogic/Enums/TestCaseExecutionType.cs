// <copyright file="TestCaseExecutionType.cs" company="Automate The Planet Ltd.">
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

namespace TestCaseManagerCore.BusinessLogic.Enums
{
    public enum TestCaseExecutionType
    {
        All = -2,
        Active = -1,
        Unspecified = 0,
        None = 1,
        Passed = 2,
        Failed = 3,
        Inconclusive = 4,
        Timeout = 5,
        Aborted = 6,
        Blocked = 7,
        NotExecuted = 8,
        Warning = 9,
        Error = 10,
        NotApplicable = 11,
        MaxValue = 12,
        Paused = 12,
    }
}