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

namespace Fidely.Framework.Integration.WPF
{
    /// <summary>
    /// Provides data for the SearchExecuted event of the autocomplete box.
    /// </summary>
    public class SearchExecutedEventArgs : EventArgs
    {
        /// <summary>
        /// The search query string.
        /// </summary>
        public string Query { get; private set; }


        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="query">The search query string.</param>
        public SearchExecutedEventArgs(string query)
        {
            Query = query;
        }
    }
}
