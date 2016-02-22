// <copyright file="AssembliesLoaderManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using System;
using System.Reflection;

namespace AAngelov.Utilities.Managers
{
    /// <summary>
    /// Contains help methods to load given assembly in custom App Domain
    /// </summary>
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