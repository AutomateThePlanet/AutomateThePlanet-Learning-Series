// <copyright file="TestPlan.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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