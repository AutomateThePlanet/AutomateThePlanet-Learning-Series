// <copyright file="TextReplacePair.cs" company="Automate The Planet Ltd.">
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
namespace AAngelov.Utilities.Entities
{
    /// <summary>
    /// Contains old/new text pair used to change specific text
    /// </summary>
    public class TextReplacePair
    {
        /// <summary>
        /// Gets or sets the old text.
        /// </summary>
        /// <value>
        /// The old text.
        /// </value>
        public string OldText { get; set; }

        /// <summary>
        /// Gets or sets the new text.
        /// </summary>
        /// <value>
        /// The new text.
        /// </value>
        public string NewText { get; set; }
    }
}