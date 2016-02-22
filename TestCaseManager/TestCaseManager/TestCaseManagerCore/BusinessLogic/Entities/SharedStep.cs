// <copyright file="SharedStep.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Enums;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains information about Shared Step object
    /// </summary>
    public class SharedStep : TestBase, IComparable<SharedStep>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedStep"/> class.
        /// </summary>
        /// <param name="sharedStepCore">The shared step core object.</param>
        public SharedStep(ISharedStep sharedStepCore)
        {
            this.ISharedStep = sharedStepCore;
            List<TestStep> allTestSteps = TestStepManager.GetAllTestStepsInSharedStep(sharedStepCore);
            this.StepsToolTip = TestStepManager.GenerateTestStepsText(allTestSteps);
            this.Title = sharedStepCore.Title;
            this.Area = sharedStepCore.Area;
            this.Priority = (Priority)sharedStepCore.Priority;
            this.TeamFoundationIdentityName = new TeamFoundationIdentityName(sharedStepCore.OwnerTeamFoundationId, sharedStepCore.OwnerName);
            this.OwnerDisplayName = sharedStepCore.OwnerName;
            this.TeamFoundationId = sharedStepCore.OwnerTeamFoundationId;
            this.DateCreated = sharedStepCore.DateCreated;
            this.DateModified = sharedStepCore.DateModified;
            base.isInitialized = true;
            this.Id = sharedStepCore.Id;
            this.CreatedBy = sharedStepCore.WorkItem.CreatedBy;
        }

        /// <summary>
        /// Gets or sets the attribute shared step core object.
        /// </summary>
        /// <value>
        /// The attribute shared step core object.
        /// </value>
        public ISharedStep ISharedStep { get; set; }

        /// <summary>
        /// Gets or sets the shared steps tool tip.
        /// </summary>
        /// <value>
        /// The shared steps tool tip.
        /// </value>
        public string StepsToolTip { get; set; }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(SharedStep other)
        {
            return this.Title.CompareTo(other.Title);
        }
    }
}