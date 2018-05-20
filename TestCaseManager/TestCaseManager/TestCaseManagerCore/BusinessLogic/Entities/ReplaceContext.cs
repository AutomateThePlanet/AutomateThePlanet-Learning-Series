// <copyright file="ReplaceContext.cs" company="Automate The Planet Ltd.">
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
    using System.Collections.ObjectModel;
    using System.Linq;
    using AAngelov.Utilities.Entities;
    using AAngelov.Utilities.UI.Core;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Enums;

    /// <summary>
    /// Used in the Find Replace View to pass which data should be changed and the data itself
    /// </summary>
    public class ReplaceContext : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// Defines if the text in titles should be replaced
        /// </summary>
        private bool replaceInTitle;

        /// <summary>
        /// Defines if the text in the test steps in the current test case shared steps should be replaced
        /// </summary>
        private bool replaceSharedSteps;

        /// <summary>
        /// Defines if the text in the test steps in the current test case should be replaced
        /// </summary>
        private bool replaceInTestSteps;

        /// <summary>
        /// The change owner
        /// </summary>
        private bool changeOwner;

        /// <summary>
        /// The change priorities
        /// </summary>
        private bool changePriorities;

        /// <summary>
        /// The selected priority
        /// </summary>
        private Priority selectedPriority;

        /// <summary>
        /// The selected suite
        /// </summary>
        private ITestSuiteBase selectedSuite;

        /// <summary>
        /// The selected team foundation identity name
        /// </summary>
        private TeamFoundationIdentityName selectedTeamFoundationIdentityName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceContext"/> class.
        /// </summary>
        public ReplaceContext()
        {
            this.ReplaceInTitles = true;
            this.ReplaceInTestSteps = true;
            this.ReplaceSharedSteps = true;
            this.ChangeOwner = true;
            this.ChangePriorities = true;
            this.ObservableTextReplacePairs = new ObservableCollection<TextReplacePair>();
            this.ObservableTextReplacePairs.Add(new TextReplacePair());
            this.ObservableSharedStepIdReplacePairs = new ObservableCollection<SharedStepIdReplacePair>();
            this.ObservableSharedStepIdReplacePairs.Add(new SharedStepIdReplacePair());
            this.SelectedEntities = new List<object>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [the text in the test case title should be replaced].
        /// </summary>
        /// <value>
        /// <c>true</c> if [the text in the test case title should be replaced]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceInTitles
        {
            get
            {
                return this.replaceInTitle;
            }

            set
            {
                this.replaceInTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [text in the test steps in the shared steps should be replaced].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [text in the test steps in the shared steps should be replaced]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceSharedSteps
        {
            get
            {
                return this.replaceSharedSteps;
            }

            set
            {
                this.replaceSharedSteps = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [text in the test steps of the current test case should be replaced].
        /// </summary>
        /// <value>
        /// <c>true</c> if [text in the test steps of the current test case should be replaced]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceInTestSteps
        {
            get
            {
                return this.replaceInTestSteps;
            }

            set
            {
                this.replaceInTestSteps = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [change priorities].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change priorities]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangePriorities
        {
            get
            {
                return this.changePriorities;
            }

            set
            {
                this.changePriorities = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [change owner].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change owner]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangeOwner
        {
            get
            {
                return this.changeOwner;
            }

            set
            {
                this.changeOwner = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public Priority SelectedPriority
        {
            get
            {
                return this.selectedPriority;
            }

            set
            {
                this.selectedPriority = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected suite.
        /// </summary>
        /// <value>
        /// The selected suite.
        /// </value>
        public ITestSuiteBase SelectedSuite
        {
            get
            {
                return this.selectedSuite;
            }

            set
            {
                this.selectedSuite = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the selected team foundation identity.
        /// </summary>
        /// <value>
        /// The name of the selected team foundation identity.
        /// </value>
        public TeamFoundationIdentityName SelectedTeamFoundationIdentityName
        {
            get
            {
                return this.selectedTeamFoundationIdentityName;
            }

            set
            {
                this.selectedTeamFoundationIdentityName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the observable text replace pairs.
        /// </summary>
        /// <value>
        /// The observable text replace pairs.
        /// </value>
        public ObservableCollection<TextReplacePair> ObservableTextReplacePairs { get; set; }

        /// <summary>
        /// Gets or sets the observable shared step unique identifier replace pairs.
        /// </summary>
        /// <value>
        /// The observable shared step unique identifier replace pairs.
        /// </value>
        public ObservableCollection<SharedStepIdReplacePair> ObservableSharedStepIdReplacePairs { get; set; }

        /// <summary>
        /// Gets or sets the selected test cases/shared steps.
        /// </summary>
        /// <value>
        /// The selected test cases/shared steps.
        /// </value>
        public List<object> SelectedEntities { get; set; }
    }
}