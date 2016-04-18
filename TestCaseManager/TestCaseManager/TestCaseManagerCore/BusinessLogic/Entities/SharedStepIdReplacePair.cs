// <copyright file="SharedStepIdReplacePair.cs" company="Automate The Planet Ltd.">
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
    using System.Linq;

    /// <summary>
    /// Contains old/new id pair used to change specific test case actions
    /// </summary>
    public class SharedStepIdReplacePair
    {
        /// <summary>
        /// Gets or sets the old shared step unique identifier.
        /// </summary>
        /// <value>
        /// The old shared step unique identifier.
        /// </value>
        public int OldSharedStepId { get; set; }

        /// <summary>
        /// Gets or sets the new shared step unique identifier.
        /// </summary>
        /// <value>
        /// The new shared step unique identifier.
        /// </value>
        public string NewSharedStepIds { get; set; }
    }
}