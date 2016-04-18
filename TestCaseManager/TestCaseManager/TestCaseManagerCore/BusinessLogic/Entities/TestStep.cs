// <copyright file="TestStep.cs" company="Automate The Planet Ltd.">
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
    using AAngelov.Utilities.Managers;
    using AAngelov.Utilities.UI.Core;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains Test Step object information properties
    /// </summary>
    [Serializable]
    public class TestStep : BaseNotifyPropertyChanged, ICloneable, IEquatable<TestStep>
    {
        /// <summary>
        /// The is initialized
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The is paste enabled
        /// </summary>
        private bool isPasteEnabled;

        /// <summary>
        /// The action title
        /// </summary>
        private string actionTitle;

        /// <summary>
        /// The action expected result
        /// </summary>
        private string actionExpectedResult;

        /// <summary>
        /// The original action title
        /// </summary>
        private string originalActionTitle;

        /// <summary>
        /// The original action expected result
        /// </summary>
        private string originalActionExpectedResult;

        /// <summary>
        /// The test step unique identifier
        /// </summary>
        private Guid testStepGuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid)
        {
            this.IsShared = isShared;
            this.Title = title;
            this.OriginalTitle = title;
            this.TestStepGuid = testStepGuid;
            this.IsPasteEnabled = false;
            this.isInitialized = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep"/> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique guid.</param>
        /// <param name="testStepId">The test step unique identifier.</param>
        /// <param name="actionTitle">The action title.</param>
        /// <param name="actionExpectedResult">The action expected result.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid, int testStepId, string actionTitle, string actionExpectedResult) : this(isShared, title, testStepGuid)
        {
            this.TestStepId = testStepId;
            this.ActionTitle = actionTitle;
            this.ActionExpectedResult = actionExpectedResult;
            this.OriginalActionTitle = actionTitle;
            this.OriginalActionExpectedResult = actionExpectedResult;
            this.isInitialized = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        /// <param name="testStepCore">The test step core.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid, ITestStep testStepCore) : this(isShared, title, testStepGuid)
        {
            this.TestStepId = testStepCore.Id;
            this.ActionTitle = testStepCore.Title.ToPlainText();
            this.ActionExpectedResult = testStepCore.ExpectedResult.ToPlainText();
            this.OriginalActionTitle = testStepCore.Title.ToPlainText();
            this.OriginalActionExpectedResult = testStepCore.ExpectedResult.ToPlainText();
            this.isInitialized = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        /// <param name="testStepCore">The test step core.</param>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid, ITestStep testStepCore, int sharedStepId) : this(isShared, title, testStepGuid, testStepCore)
        {
            this.SharedStepId = sharedStepId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep"/> class.
        /// </summary>
        /// <param name="otherTestStep">The other test step.</param>
        public TestStep(TestStep otherTestStep) : this(otherTestStep.IsShared, otherTestStep.Title, otherTestStep.TestStepGuid, otherTestStep.TestStepId, otherTestStep.ActionTitle, otherTestStep.ActionExpectedResult)
        {
            this.SharedStepId = otherTestStep.SharedStepId;
            this.OriginalActionTitle = otherTestStep.OriginalActionTitle;
            this.OriginalActionExpectedResult = otherTestStep.OriginalActionExpectedResult;
        }

        /// <summary>
        /// Gets or sets the test step unique identifier.
        /// </summary>
        /// <value>
        /// The test step unique identifier.
        /// </value>
        public int TestStepId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is shared].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is shared]; otherwise, <c>false</c>.
        /// </value>
        public bool IsShared { get; set; }

        /// <summary>
        /// Gets or sets the shared step unique identifier.
        /// </summary>
        /// <value>
        /// The shared step unique identifier.
        /// </value>
        public int SharedStepId { get; set; }

        /// <summary>
        /// Gets or sets the step unique identifier.
        /// </summary>
        /// <value>
        /// The step unique identifier.
        /// </value>
        public Guid TestStepGuid 
        {
            get
            {
                return this.testStepGuid;
            }

            set
            {
                if (value == default(Guid))
                {
                    this.testStepGuid = Guid.NewGuid();
                }
                else
                {
                    this.testStepGuid = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is pate enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is pate enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsPasteEnabled
        {
            get
            {
                return this.isPasteEnabled;
            }

            set
            {
                this.isPasteEnabled = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the action title.
        /// </summary>
        /// <value>
        /// The action title.
        /// </value>
        public string ActionTitle
        {
            get
            { 
                return this.actionTitle;
            }

            set
            {
                //if (this.isInitialized)
                //{
                //    UndoRedoManager.Instance().Push(t => this.ActionTitle = t, this.actionTitle, "Change the test step action title");
                //} 
                this.actionTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the action expected result.
        /// </summary>
        /// <value>
        /// The action expected result.
        /// </value>
        public string ActionExpectedResult
        {
            get
            {
                return this.actionExpectedResult;
            }

            set
            {
                //if (this.isInitialized)
                //{
                //    UndoRedoManager.Instance().Push(t => this.ActionExpectedResult = t, this.actionExpectedResult, "Change the testp step expected result");
                //} 
                this.actionExpectedResult = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the original title.
        /// </summary>
        /// <value>
        /// The original title.
        /// </value>
        public string OriginalTitle
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t =>
                    { 
                        this.OriginalTitle = t;
                        this.Title = t;
                    }, this.title, "Change the test step title");
                }
                this.actionTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the original action title.
        /// </summary>
        /// <value>
        /// The original action title.
        /// </value>
        public string OriginalActionTitle
        {
            get
            {
                return this.originalActionTitle;
            }

            set
            {
                //if (this.isInitialized)
                //{
                //    UndoRedoManager.Instance().Push(t =>
                //        {
                //            this.OriginalActionTitle = t;
                //            this.ActionTitle = t;
                //        }, this.originalActionTitle, "Change the test step original action title");
                //}
                this.originalActionTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the original expected result.
        /// </summary>
        /// <value>
        /// The original expected result.
        /// </value>
        public string OriginalActionExpectedResult
        {
            get
            {
                return this.originalActionExpectedResult;
            }

            set
            {
                //if (this.isInitialized)
                //{
                //    UndoRedoManager.Instance().Push(t =>
                //        {
                //            this.OriginalActionExpectedResult = t;
                //            this.ActionExpectedResult = t;
                //        }, this.originalActionExpectedResult, "Change the test step original expected result");
                //}
                this.originalActionExpectedResult = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            TestStep clonedTestStep = new TestStep(this);
            clonedTestStep.TestStepGuid = Guid.NewGuid();

            return clonedTestStep;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TestStep other)
        {
            bool result = this.TestStepGuid.Equals(other.TestStepGuid) &&
                          this.ActionTitle.Equals(other.ActionTitle) &&
                          this.ActionExpectedResult.Equals(other.ActionExpectedResult) &&
                          this.IsShared.Equals(other.IsShared) &&
                          this.SharedStepId.Equals(other.SharedStepId) &&
                          this.TestStepId.Equals(other.TestStepId);

            return result;
        }
    }
}