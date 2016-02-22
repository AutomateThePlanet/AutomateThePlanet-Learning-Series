// <copyright file="Suite.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using System.Collections.ObjectModel;
    using AAngelov.Utilities.Contracts;
    using AAngelov.Utilities.Enums;
    using AAngelov.Utilities.Managers;
    using AAngelov.Utilities.UI.Core;

    /// <summary>
    /// Represents TreeView Suite Node Object
    /// </summary>
    [Serializable]
    public class Suite : BaseNotifyPropertyChanged, IClipBoard<Suite>, ICloneable, IComparable<Suite>
    {
        /// <summary>
        /// The is node expanded
        /// </summary>
        private bool isNodeExpanded;

        /// <summary>
        /// The is selected
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// The is copy enabled
        /// </summary>
        private bool isCopyEnabled;

        /// <summary>
        /// The is cut enabled
        /// </summary>
        private bool isCutEnabled;

        /// <summary>
        /// The is paste enabled
        /// </summary>
        private bool isPasteEnabled;

        /// <summary>
        /// The is rename enabled
        /// </summary>
        private bool isRenameEnabled;

        /// <summary>
        /// The is add enabled
        /// </summary>
        private bool isAddEnabled;

        /// <summary>
        /// The is remove enabled
        /// </summary>
        private bool isRemoveEnabled;

        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="Suite"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="id">The unique identifier.</param>
        /// <param name="subSuites">The sub suites.</param>
        /// <param name="parent">The parent.</param>
        public Suite(string title, int id, ObservableCollection<Suite> subSuites = null, Suite parent = null)
        {
            this.Title = title;
            this.Id = id;
            this.SubSuites = subSuites;
            this.Parent = parent;
            this.IsCopyEnabled = true;
            this.IsCutEnabled = true;
            this.IsPasteEnabled = true;
            this.IsRemoveEnabled = true;
            this.IsRenameEnabled = true;
            this.IsSuiteAddEnabled = true;
            this.IsAddSuiteAllowed = true;
            this.IsPasteAllowed = true;
            this.ClipBoardCommand = ClipBoardCommand.None;
            if (subSuites == null)
            {
                this.SubSuites = new ObservableCollection<Suite>();
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Suite Parent { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the sub suites.
        /// </summary>
        /// <value>
        /// The sub suites.
        /// </value>
        public ObservableCollection<Suite> SubSuites { get; set; }

        /// <summary>
        /// Gets or sets the clip board command.
        /// </summary>
        /// <value>
        /// The clip board command.
        /// </value>
        public ClipBoardCommand ClipBoardCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is add suite allowed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is add suite allowed]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAddSuiteAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is paste allowed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is paste allowed]; otherwise, <c>false</c>.
        /// </value>
        public bool IsPasteAllowed { get; set; }

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
        /// Gets or sets a value indicating whether [is copy enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is copy enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsCopyEnabled
        {
            get
            {
                return this.isCopyEnabled;
            }

            set
            {
                this.isCopyEnabled = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is cut enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is cut enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsCutEnabled
        {
            get
            {
                return this.isCutEnabled;
            }

            set
            {
                this.isCutEnabled = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is paste enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is paste enabled]; otherwise, <c>false</c>.
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
        /// Gets or sets a value indicating whether [is rename enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is rename enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsRenameEnabled
        {
            get
            {
                return this.isRenameEnabled;
            }

            set
            {
                this.isRenameEnabled = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is add enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is add enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuiteAddEnabled
        {
            get
            {
                return this.isAddEnabled;
            }

            set
            {
                this.isAddEnabled = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is remove enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is remove enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsRemoveEnabled
        {
            get
            {
                return this.isRemoveEnabled;
            }

            set
            {
                this.isRemoveEnabled = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is node expanded].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is node expanded]; otherwise, <c>false</c>.
        /// </value>
        public bool IsNodeExpanded
        {
            get
            {
                return this.isNodeExpanded;
            }

            set
            {
                this.isNodeExpanded = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is selected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is selected]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets from clipboard.
        /// </summary>
        /// <returns>the retrieved object</returns>
        public static Suite GetFromClipboard()
        {
            Suite suite = ClipBoardManager<Suite>.GetFromClipboard();
            return suite;
        }

        /// <summary>
        /// Copies the automatic clipboard.
        /// </summary>
        /// <param name="isCopy">if set to <c>true</c> [copy].</param>
        public void CopyToClipboard(bool isCopy)
        {
            this.ClipBoardCommand = isCopy ? ClipBoardCommand.Copy : ClipBoardCommand.Cut;
            ClipBoardManager<Suite>.CopyToClipboard(this);            
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            Suite clonedSuite = new Suite(this.Title, this.Id, new ObservableCollection<Suite>());
            if (this.Parent != null)
            {
                clonedSuite.Parent = (Suite)this.Parent.Clone();
            }
            if (this.SubSuites != null)
            {
                foreach (Suite currentSuite in this.SubSuites)
                {
                    clonedSuite.SubSuites.Add(currentSuite);
                }
            }
            clonedSuite.IsAddSuiteAllowed = this.IsAddSuiteAllowed;
            clonedSuite.IsPasteAllowed = this.IsPasteAllowed;
            clonedSuite.IsSelected = this.IsSelected;
            clonedSuite.isNodeExpanded = this.IsNodeExpanded;
            clonedSuite.IsPasteEnabled = this.IsPasteEnabled;
            clonedSuite.IsCopyEnabled = this.IsCopyEnabled;
            clonedSuite.IsCutEnabled = this.IsCutEnabled;
            clonedSuite.IsRemoveEnabled = this.IsRemoveEnabled;
            clonedSuite.IsRenameEnabled = this.IsRenameEnabled;

            return clonedSuite;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(Suite other)
        {
            return this.Title.CompareTo(other.Title);
        }
    }
}