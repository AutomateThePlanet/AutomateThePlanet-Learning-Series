// <copyright file="TestPlan.cs" company="Automate The Planet Ltd.">
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
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains TestPlan object information properties
    /// </summary>
    public class TestPlan
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestPlan"/> class.
        /// </summary>
        /// <param name="testPlanCore">The test plan core.</param>
        public TestPlan(ITestPlan testPlanCore)
        {
            this.Id = testPlanCore.Id;
            this.Name = testPlanCore.Name;
            this.OwnerDisplayName = testPlanCore.Owner.DisplayName;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the display name of the owner.
        /// </summary>
        /// <value>
        /// The display name of the owner.
        /// </value>
        public string OwnerDisplayName { get; set; }
    }
}