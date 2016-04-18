// <copyright file="CheckedItemManager.cs" company="Automate The Planet Ltd.">
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
namespace AAngelov.Utilities.UI.Managers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using AAngelov.Utilities.UI.Core;

    /// <summary>
    /// Used to transform observable checked items to list of strings
    /// </summary>
    public static class CheckedItemManager
    {
        /// <summary>
        /// Automatics the list.
        /// </summary>
        /// <param name="checkedItems">The checked items.</param>
        /// <returns>list of string representation of all checked items</returns>
        public static List<string> ToList(this ObservableCollection<CheckedItem> checkedItems)
        {
            List<string> selectedDescriptions = new List<string>();
            foreach (var item in checkedItems)
            {
                if (item.Selected)
                {
                    selectedDescriptions.Add(item.Description);
                }
            }

            return selectedDescriptions;
        }
    }
}