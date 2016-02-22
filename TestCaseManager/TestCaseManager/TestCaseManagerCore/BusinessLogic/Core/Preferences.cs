// <copyright file="Preferences.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore
{
    using System;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Conatains main app preferences
    /// </summary>
    public class Preferences
    {
        /// <summary>
        /// Gets or sets the TFS URI.
        /// </summary>
        /// <value>
        /// The TFS URI.
        /// </value>
        public Uri TfsUri { get; set; }

        /// <summary>
        /// Gets or sets the name of the test project.
        /// </summary>
        /// <value>
        /// The name of the test project.
        /// </value>
        public string TestProjectName { get; set; }

        /// <summary>
        /// Gets or sets the test plan.
        /// </summary>
        /// <value>
        /// The test plan.
        /// </value>
        public ITestPlan TestPlan { get; set; }
    }
}