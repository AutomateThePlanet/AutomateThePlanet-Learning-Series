// <copyright file="CheckedItem.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.UI.Core
{
    /// <summary>
    /// Used in list of checkboxes to get only checked entities
    /// </summary>
    public class CheckedItem
    {
        /// <summary>
        /// The selected
        /// </summary>
        private bool selected;

        /// <summary>
        /// The description
        /// </summary>
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedItem"/> class.
        /// </summary>
        public CheckedItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedItem" /> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="selected">if set to <c>true</c> [selected].</param>
        public CheckedItem(string description, bool selected = true)
        {
            this.Description = description;
            this.Selected = selected;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [selected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [selected]; otherwise, <c>false</c>.
        /// </value>
        public bool Selected
        {
            get 
            {
                return this.selected;
            }

            set
            {
                this.selected = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return this.description; 
            }

            set
            {
                this.description = value;
            }
        }
    }
}