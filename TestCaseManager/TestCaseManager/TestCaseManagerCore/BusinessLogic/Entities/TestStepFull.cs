// <copyright file="TestStepFull.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using System.Linq;

    /// <summary>
    /// Extends the Test Step object- adds index
    /// </summary>
    public class TestStepFull : TestStep, IEquatable<TestStepFull>, IComparable<TestStepFull>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStepFull"/> class.
        /// </summary>
        /// <param name="testStep">The test step.</param>
        /// <param name="index">The index.</param>
        public TestStepFull(TestStep testStep, int index) : base(testStep)
        {
            this.Index = index;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TestStepFull other)
        {
            bool result = base.Equals(other);

            return result;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(TestStepFull other)
        {
            return this.Index.CompareTo(other.Index);
        }
    }
}