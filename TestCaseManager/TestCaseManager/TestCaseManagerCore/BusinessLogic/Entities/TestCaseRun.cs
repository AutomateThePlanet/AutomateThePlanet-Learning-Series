// <copyright file="TestCaseRun.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    /// <summary>
    /// Contains Properties Related to Test Case Execution Statistics
    /// </summary>
    public class TestCaseRun
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseRun"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        public TestCaseRun(DateTime startTime)
        {
            this.StartTime = startTime;
            this.LastStartedTime = startTime;
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the last started time.
        /// </summary>
        /// <value>
        /// The last started time.
        /// </value>
        public DateTime LastStartedTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is paused.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is paused; otherwise, <c>false</c>.
        /// </value>
        public bool IsPaused { get; set; }
    }
}
