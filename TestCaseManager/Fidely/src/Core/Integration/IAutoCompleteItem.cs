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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fidely.Framework.Integration
{
    /// <summary>
    /// Represents the autocomplete item that is displayed autocomplete box.
    /// </summary>
    public interface IAutoCompleteItem
    {
        /// <summary>
        /// The display name of the autocomplete item.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The description of the autocomplete item.
        /// </summary>
        string Description { get; }


        /// <summary>
        /// Checks the specified value whether or not can be completed by this item.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <param name="matchingOption">The option to specify matching rule.</param>
        /// <returns>True if the specified value can be completed by this item, otherwise false.</returns>
        bool IsMatch(string value, MatchingOption matchingOption);

        /// <summary>
        /// Get the completed value.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <param name="matchingOption">The option to specify matching rule.</param>
        /// <returns>The completed value.</returns>
        string Complete(string value, MatchingOption matchingOption);
    }
}
