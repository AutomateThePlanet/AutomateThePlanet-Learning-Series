// <copyright file="InitialViewFilters.cs" company="Automate The Planet Ltd.">
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
    using AAngelov.Utilities.UI.Core;

    /// <summary>
    /// Contains search filters for the initial app view
    /// </summary>
    public class InitialViewFilters : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The detault unique identifier
        /// </summary>
        public string DetaultId = "ID";

        /// <summary>
        /// The detault title
        /// </summary>
        public string DetaultTitle = "Title";

        /// <summary>
        /// The detault suite
        /// </summary>
        public string DetaultSuite = "Suite";

        /// <summary>
        /// The detault priority
        /// </summary>
        public string DetaultPriority = "Priority";

        /// <summary>
        /// The detault assigned automatic
        /// </summary>
        public string DetaultAssignedTo = "Assigned To";

        /// <summary>
        /// The detault advanced search
        /// </summary>
        public string DetaultAdvancedSearch = "Perform Advanced Search Query        [ title:Navigate OR (createdBy: Anton AND createdOn >= “2013/10/10″) ]";

        /// <summary>
        /// The is title text set
        /// </summary>
        public bool IsTitleTextSet;

        /// <summary>
        /// The is suite text set
        /// </summary>
        public bool IsSuiteTextSet;

        /// <summary>
        /// The is unique identifier text set
        /// </summary>
        public bool IsIdTextSet;

        /// <summary>
        /// The is priority text set
        /// </summary>
        public bool IsPriorityTextSet;

        /// <summary>
        /// The is assigned automatic text set
        /// </summary>
        public bool IsAssignedToTextSet;

        /// <summary>
        /// The is advanced search text set
        /// </summary>
        public bool IsAdvancedSearchTextSet;

        /// <summary>
        /// The title filter
        /// </summary>
        private string titleFilter;

        /// <summary>
        /// The suite filter
        /// </summary>
        private string suiteFilter;

        /// <summary>
        /// The unique identifier filter
        /// </summary>
        private string idFilter;

        /// <summary>
        /// The priority filter
        /// </summary>
        private string priorityFilter;

        /// <summary>
        /// The assigned automatic filter
        /// </summary>
        private string assignedToFilter;

        /// <summary>
        /// The advanced search filter
        /// </summary>
        private string advancedSearchFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="suite">The suite.</param>
        /// <param name="id">The unique identifier.</param>
        public InitialViewFilters(string title, string suite, string id)
        {
            this.TitleFilter = title;
            this.SuiteFilter = suite;
            this.IdFilter = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="suite">The suite.</param>
        /// <param name="id">The unique identifier.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="assignedTo">The assigned automatic.</param>
        public InitialViewFilters(string title, string suite, string id, string priority, string assignedTo, string advancedSearch) : this(title, suite, id)
        {
            this.PriorityFilter = priority;
            this.AssignedToFilter = assignedTo;
            this.AdvancedSearchFilter = advancedSearch;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        public InitialViewFilters()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets or sets the title filter.
        /// </summary>
        /// <value>
        /// The title filter.
        /// </value>
        public string TitleFilter
        {
            get
            {
                return this.titleFilter;
            }

            set
            {
                this.titleFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the advanced search filter.
        /// </summary>
        /// <value>
        /// The advanced search filter.
        /// </value>
        public string AdvancedSearchFilter
        {
            get
            {
                return this.advancedSearchFilter;
            }

            set
            {
                this.advancedSearchFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the suite filter.
        /// </summary>
        /// <value>
        /// The suite filter.
        /// </value>
        public string SuiteFilter
        {
            get
            {
                return this.suiteFilter;
            }

            set
            {
                this.suiteFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier filter.
        /// </summary>
        /// <value>
        /// The unique identifier filter.
        /// </value>
        public string IdFilter
        {
            get
            {
                return this.idFilter;
            }

            set
            {
                this.idFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the priority filter.
        /// </summary>
        /// <value>
        /// The priority filter.
        /// </value>
        public string PriorityFilter
        {
            get
            {
                return this.priorityFilter;
            }

            set
            {
                this.priorityFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the assigned automatic filter.
        /// </summary>
        /// <value>
        /// The assigned automatic filter.
        /// </value>
        public string AssignedToFilter
        {
            get
            {
                return this.assignedToFilter;
            }

            set
            {
                this.assignedToFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            this.IdFilter = this.DetaultId;
            this.TitleFilter = this.DetaultTitle;
            this.SuiteFilter = this.DetaultSuite;
            this.PriorityFilter = this.DetaultPriority;
            this.AssignedToFilter = this.DetaultAssignedTo;
            this.AdvancedSearchFilter = this.DetaultAdvancedSearch;
            this.IsIdTextSet = false;
            this.IsSuiteTextSet = false;
            this.IsTitleTextSet = false;
            this.IsPriorityTextSet = false;
            this.IsAssignedToTextSet = false;
            this.IsAdvancedSearchTextSet = false;
        }
    }
}