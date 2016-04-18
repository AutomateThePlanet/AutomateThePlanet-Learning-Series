// <copyright file="CheckedItem.cs" company="Automate The Planet Ltd.">
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