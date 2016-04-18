// <copyright file="TestCaseFull.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains Test Case object + all test steps
    /// </summary>
    [Serializable]
    public class TestCaseFull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseFull" /> class.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="testSteps">The test steps.</param>
        /// <param name="mostRecentResult">The most recent result.</param>
        /// <param name="executionComment">The execution comment.</param>
        public TestCaseFull(TestCase testCase, List<TestStep> testSteps, string mostRecentResult, string executionComment)
        {
            this.TestCase = testCase;
            this.TestSteps = testSteps;
            this.MostRecentResult = mostRecentResult;
            this.ExecutionComment = executionComment;
        }

        /// <summary>
        /// Gets or sets the tests case.
        /// </summary>
        /// <value>
        /// The tests case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the test steps.
        /// </summary>
        /// <value>
        /// The test steps.
        /// </value>
        public List<TestStep> TestSteps { get; set; }

        /// <summary>
        /// Gets or sets the most recent result.
        /// </summary>
        /// <value>
        /// The most recent result.
        /// </value>
        public string MostRecentResult { get; set; }

        /// <summary>
        /// Gets or sets the execution comment.
        /// </summary>
        /// <value>
        /// The execution comment.
        /// </value>
        public string ExecutionComment { get; set; }
    }
}