// <copyright file="AssociatedAutomation.cs" company="Automate The Planet Ltd.">
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
    /// <summary>
    /// Contains Associated Automation information properties
    /// </summary>
    public class AssociatedAutomation
    {
        /// <summary>
        /// The none text constant
        /// </summary>
        public const string NONE = "None";

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociatedAutomation"/> class.
        /// </summary>
        public AssociatedAutomation()
        {
            this.TestName = NONE;
            this.Assembly = NONE;
            this.Type = NONE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociatedAutomation"/> class.
        /// </summary>
        /// <param name="testInfo">The test information.</param>
        public AssociatedAutomation(string testInfo)
        {
            string[] infos = testInfo.Split(',');
            this.TestName = infos[1];
            this.Assembly = infos[2];
            this.Type = infos[3];
        }

        /// <summary>
        /// Gets or sets the type of the test.
        /// </summary>
        /// <value>
        /// The test type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the assembly of the associated test.
        /// </summary>
        /// <value>
        /// the assembly of the associated test.
        /// </value>
        public string Assembly { get; set; }

        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>
        /// The name of the test.
        /// </value>
        public string TestName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Type: {0} Assembly: {1} TestName: {2}", this.Type, this.Assembly, this.TestName);
        }
    }
}