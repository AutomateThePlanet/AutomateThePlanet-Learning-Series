// <copyright file="Test.cs" company="Automate The Planet Ltd.">
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
    using System;
    using System.Linq;

    /// <summary>
    /// Contains Test object information properties
    /// </summary>
    [Serializable]
    public class Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodId">The method unique identifier.</param>
        public Test(string fullName, string className, Guid methodId)
        {
            this.FullName = fullName;
            this.ClassName = className;
            this.MethodId = methodId;
        }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the method unique identifier.
        /// </summary>
        /// <value>
        /// The method unique identifier.
        /// </value>
        public Guid MethodId { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}", this.FullName, this.ClassName, this.MethodId);
        }
    }
}