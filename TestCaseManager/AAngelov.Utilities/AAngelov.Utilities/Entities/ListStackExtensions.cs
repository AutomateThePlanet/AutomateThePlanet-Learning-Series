// <copyright file="ListStackExtensions.cs" company="Automate The Planet Ltd.">
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
    using System.Collections.Generic;
    using AAngelov.Utilities.Contracts;

    /// <summary>
    /// Extension methods which allow a List to be used as a stack. This was created as we need to be able to manipulate the stack size dynamically
    /// which is not allowed by the Stack class
    /// </summary>
    public static class ListStackExtensions
    {
        /// <summary>
        /// Pushes the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        public static void Push(this List<IUndoRedoRecord> list, IUndoRedoRecord item)
        {
            list.Insert(0, item);
        }

        /// <summary>
        /// Pops the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>the undo record</returns>
        public static IUndoRedoRecord Pop(this List<IUndoRedoRecord> list)
        {
            IUndoRedoRecord ret = list[0];
            list.RemoveAt(0);
            return ret;
        }
    }
}