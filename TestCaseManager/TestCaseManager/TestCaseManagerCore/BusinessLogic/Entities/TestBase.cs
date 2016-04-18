// <copyright file="TestBase.cs" company="Automate The Planet Ltd.">
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
    using TestCaseManagerCore.BusinessLogic.Enums;
    using Fidely.Framework;

    /// <summary>
    /// Contains Base Test Entities properties
    /// </summary>
    [Serializable]
    public class TestBase : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The owner
        /// </summary>
        [NonSerialized]
        private TeamFoundationIdentityName teamFoundationIdentityName;

        /// <summary>
        /// The unique identifier
        /// </summary>
        private int id;

        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The area
        /// </summary>
        private string area;

        /// <summary>
        /// The created by
        /// </summary>
        private string createdBy;

        /// <summary>
        /// The date created
        /// </summary>
        private DateTime dateCreated;

        /// <summary>
        /// The date modified
        /// </summary>
        private DateTime dateModified; 

        /// <summary>
        /// The priority
        /// </summary>
        private Priority priority;

        /// <summary>
        /// The is initialized
        /// </summary>
        protected bool isInitialized;

        /// <summary>
        /// Gets the display name of the owner.
        /// </summary>
        /// <value>
        /// The display name of the owner.
        /// </value>
        [Alias("assignedTo", Description = "Assigned To Person Name")]
        public string OwnerDisplayName { get; set; }

        /// <summary>
        /// Gets the team foundation unique identifier.
        /// </summary>
        /// <value>
        /// The team foundation unique identifier.
        /// </value>
        public Guid TeamFoundationId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Alias("title", Description = "The title of item")]
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t => this.Title = t, this.title, "Change the test case title");
                }
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [Alias("createdOn", Description = "The creation date of item")]
        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated;
            }

            set
            {
                this.dateCreated = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [Alias("createdBy", Description = "The person who created item")]
        public string CreatedBy
        {
            get
            {
                return this.createdBy;
            }

            set
            {
                this.createdBy = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        [Alias("modifiedOn", Description = "The last modification date of item")]
        public DateTime DateModified
        {
            get
            {
                return this.dateModified;
            }

            set
            {
                this.dateModified = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        [Alias("area", Description = "Area of the item")]
        public string Area
        {
            get
            {
                return this.area;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(a => this.Area = a, this.area, "Change the test case area");
                }
                this.area = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public TeamFoundationIdentityName TeamFoundationIdentityName
        {
            get
            {
                return this.teamFoundationIdentityName;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t => this.TeamFoundationIdentityName = t, this.teamFoundationIdentityName, "Change the test case owner");
                }
                this.teamFoundationIdentityName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        [Alias("priority", Description = "Priority of the item")]
        public Priority Priority
        {
            get
            {
                return this.priority;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(p => this.Priority = p, this.priority, "Change the test case priority");
                }
                this.priority = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}