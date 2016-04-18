// <copyright file="ClipBoardTestCase.cs" company="Automate The Planet Ltd.">
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
    using AAngelov.Utilities.Enums;

    /// <summary>
    /// Helper class which contains test cases which will be moved to clipboard + clipboard mode: copy or cut
    /// </summary>
    [Serializable]
    public class ClipBoardTestCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClipBoardTestCase"/> class.
        /// </summary>
        /// <param name="testCases">The test cases.</param>
        /// <param name="clipBoardCommand">The clip board command.</param>
        public ClipBoardTestCase(List<TestCase> testCases, ClipBoardCommand clipBoardCommand)
        {
            this.TestCases = testCases;
            this.ClipBoardCommand = clipBoardCommand;
        }

        /// <summary>
        /// Gets or sets the test cases.
        /// </summary>
        /// <value>
        /// The test cases.
        /// </value>
        public List<TestCase> TestCases { get; set; }

        /// <summary>
        /// Gets or sets the clip board command.
        /// </summary>
        /// <value>
        /// The clip board command.
        /// </value>
        public ClipBoardCommand ClipBoardCommand { get; set; }
    }
}