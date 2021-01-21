﻿/*
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
using System.Linq;

namespace Fidely.Framework.Integration
{
    /// <summary>
    /// Represents the autocomplete item that is displayed autocomplete box.
    /// </summary>
    public class RegexAutoCompleteItem : IAutoCompleteItem
    {
        private readonly Func<string, MatchingOption, bool> isMatchProcedure;

        private readonly Func<string, MatchingOption, string> completeProcedure;

        /// <summary>
        ///  Initializes a new instance of this class.
        /// </summary>
        /// <param name="displayName">The display name of the autocomplete item.</param>
        /// <param name="description">The description of the autocomplete item.</param>
        /// <param name="isMatchProcedure">The procedure to check the specified value whether or not can be completed by this item.</param>
        /// <param name="completeProcedure">The procedure to get the completed value.</param>
        public RegexAutoCompleteItem(string displayName, string description, Func<string, MatchingOption, bool> isMatchProcedure, Func<string, MatchingOption, string> completeProcedure)
        {
            if (displayName == null)
            {
                throw new ArgumentNullException("displayName");
            }
            if (isMatchProcedure == null)
            {
                throw new ArgumentNullException("isMatchProcedure");
            }
            if (completeProcedure == null)
            {
                throw new ArgumentNullException("completeProcedure");
            }

            DisplayName = displayName;
            Description = description ?? "";
            this.isMatchProcedure = isMatchProcedure;
            this.completeProcedure = completeProcedure;
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
            return isMatchProcedure(value, matchingOption);
        }

        /// <summary>
        /// Get the completed value.
        /// </summary>
        /// <param name="value">The target value.</param>
        /// <param name="matchingOption">The option to specify matching rule.</param>
        /// <returns>The completed value.</returns>
        public string Complete(string value, MatchingOption matchingOption)
        {
            return completeProcedure(value, matchingOption);
        }
    }
}