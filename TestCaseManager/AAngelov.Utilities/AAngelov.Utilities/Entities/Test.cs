// <copyright file="Test.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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