// <copyright file="FragmentManager.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.UI.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contains helper methods for working with WPF URL Parameters
    /// </summary>
    public class FragmentManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FragmentManager"/> class.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        public FragmentManager(string fragment)
        {
            this.Fragments = new Dictionary<string, string>();
            this.InitializeFragmentsDictionary(fragment);
        }

        /// <summary>
        /// Gets or sets the fragments dictionary containing name/object pairs.
        /// </summary>
        /// <value>
        /// The fragments dictionary.
        /// </value>
        public Dictionary<string, string> Fragments { get; set; }

        /// <summary>
        /// Gets the specified query parameter by name from the fragment dictionary.
        /// </summary>
        /// <param name="queryParameterName">Query parameter name</param>
        /// <returns>the corresponding value for the specified query parameter</returns>
        public string Get(string queryParameterName)
        {
            string value = string.Empty;
            try
            {
                value = this.Fragments[queryParameterName];
            }
            catch (KeyNotFoundException)
            {
            }

            return value;
        }

        /// <summary>
        /// Initializes the fragments dictionary by parsing current fragment.
        /// </summary>
        /// <param name="fragment">current fragment.</param>
        private void InitializeFragmentsDictionary(string fragment)
        {
            string[] fragmentParts = fragment.Split('&');
            foreach (var currentFragment in fragmentParts)
            {
                string[] parts = currentFragment.Split('=');
                this.Fragments.Add(parts[0], parts[1]);
            }
        }
    }
}