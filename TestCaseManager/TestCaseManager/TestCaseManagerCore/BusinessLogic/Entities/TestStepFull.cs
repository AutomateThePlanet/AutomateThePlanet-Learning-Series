// <copyright file="TestStepFull.cs" company="Automate The Planet Ltd.">
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