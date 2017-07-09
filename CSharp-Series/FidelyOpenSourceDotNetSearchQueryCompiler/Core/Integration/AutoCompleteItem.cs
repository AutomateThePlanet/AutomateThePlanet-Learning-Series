/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace Fidely.Framework.Integration
{
    /// <summary>
    /// Represents the autocomplete item that is displayed autocomplete box.
    /// </summary>
    public class AutoCompleteItem : IAutoCompleteItem
    {
        /// <summary>
        /// Initializes a new instance of this class with the specified display name and description.
        /// </summary>
        /// <param name="displayName">The display name of the autocomplete item.</param>
        /// <param name="description">The description of the autocomplete item.</param>
        public AutoCompleteItem(string displayName, string description)
        {
            if (displayName == null)
            {
                throw new ArgumentNullException("displayName");
            }

            DisplayName = displayName;
            Description = description ?? "";
        }

        /// <summary>
        /// The display name of the autocomplete item.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// The description of the autocomplete item.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Checks the specified value whether or not can be completed by this item.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <param name="matchingOption">The option to specify matching rule.</param>
        /// <returns>True if the specified value can be completed by this item, otherwise false.</returns>
        public bool IsMatch(string value, MatchingOption matchingOption)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (matchingOption == null)
            {
                matchingOption = new MatchingOption();
            }

            if (matchingOption.Mode == MatchingMode.Partial)
            {
                return DisplayName.ToUpperInvariant().Contains(value.ToUpperInvariant());
            }
            else
            {
                return DisplayName.ToUpperInvariant().StartsWith(value.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Get the completed value.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <param name="matchingOption">The option to specify matching rule.</param>
        /// <returns>The completed value.</returns>
        public string Complete(string value, MatchingOption matchingOption)
        {
            return DisplayName;
        }
    }
}