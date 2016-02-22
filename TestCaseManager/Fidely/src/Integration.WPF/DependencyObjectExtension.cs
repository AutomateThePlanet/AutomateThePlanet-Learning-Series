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
using System.Windows;
using System.Windows.Media;

namespace Fidely.Framework.Integration.WPF
{
    /// <summary>
    /// Provides a set of extension methods to process a dependency object.
    /// </summary>
    public static class DependencyObjectExtension
    {
        /// <summary>
        /// Finds descendent dependency objects that are type of T.
        /// </summary>
        /// <typeparam name="T">The type that is used to find descendent dependency objects.</typeparam>
        /// <param name="instance">The root dependency object.</param>
        /// <returns>The collection that contains the found dependency objects.</returns>
        public static IEnumerable<T> FindDescendents<T>(this DependencyObject instance)
            where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(instance);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(instance, i);
                if (child is T)
                {
                    yield return child as T;
                }
                foreach (var descedent in child.FindDescendents<T>())
                {
                    yield return descedent as T;
                }
            }
        }
    }
}
