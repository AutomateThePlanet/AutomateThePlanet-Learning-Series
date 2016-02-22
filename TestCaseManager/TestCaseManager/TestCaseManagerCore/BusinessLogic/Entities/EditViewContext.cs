// <copyright file="EditViewContext.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;

    /// <summary>
    /// Contains properties related with the correct execution of the Edit Test Case View
    /// </summary>
    public class EditViewContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether [is initialized].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is initialized]; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; set; }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test suite unique identifier.
        /// </summary>
        /// <value>
        /// The test suite unique identifier.
        /// </value>
        public int TestSuiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [create new].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [create new]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [duplicate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [duplicate]; otherwise, <c>false</c>.
        /// </value>
        public bool Duplicate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is already created]. Is already saved
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is already created]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlreadyCreated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is fake item inserted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is fake item inserted]; otherwise, <c>false</c>.
        /// </value>
        public bool IsFakeItemInserted { get; set; }

        /// <summary>
        /// Gets or sets the current edited step unique identifier.
        /// </summary>
        /// <value>
        /// The current edited step unique identifier.
        /// </value>
        public Guid CurrentEditedStepGuid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is shared step].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is shared step]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSharedStep { get; set; }

        /// <summary>
        /// Gets or sets the shared step unique identifier.
        /// </summary>
        /// <value>
        /// The shared step unique identifier.
        /// </value>
        public int SharedStepId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [come from test case].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [come from test case]; otherwise, <c>false</c>.
        /// </value>
        public bool ComeFromTestCase { get; set; }

        public override string ToString()
        {
            return String.Format("TestCaseId= {0}, TestSuiteId= {1}, CreateNew= {2}, Duplicate= {3}, IsSharedStep= {4}, SharedStepId= {5}, ComeFromTestCase = {6}, CurrentEditedStepGuid= {7}", this.TestCaseId, this.TestSuiteId, this.CreateNew, this.Duplicate, this.IsSharedStep, this.SharedStepId, this.ComeFromTestCase, this.CurrentEditedStepGuid);
        }
    }
}