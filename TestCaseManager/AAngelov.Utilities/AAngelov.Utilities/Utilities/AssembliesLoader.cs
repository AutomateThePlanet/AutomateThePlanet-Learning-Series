// <copyright file="AssembliesLoader.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Reflection;

namespace AAngelov.Utilities.Utilities
{
    /// <summary>
    /// Contains help methods to load given assembly in custom App Domain
    [Serializable]
    public class AssembliesLoader
    {
        /// <summary>
        /// Loads the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Load(string path)
        {
            this.ValidatePath(path);
            Assembly.Load(path);
        }

        /// <summary>
        /// Loads from.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadFrom(string path)
        {
            this.ValidatePath(path);
            Assembly.LoadFrom(path);
        }

        /// <summary>
        /// Validates the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentNullException">path</exception>
        /// <exception cref="System.ArgumentException"></exception>
        private void ValidatePath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            if (!System.IO.File.Exists(path))
            {
                throw new ArgumentException(String.Format("path \"{0}\" does not exist", path));
            }
        }
    }
}