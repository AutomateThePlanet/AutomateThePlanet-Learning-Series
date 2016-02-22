// <copyright file="SharedStepIdReplacePair.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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