// <copyright file="AssociatedAutomation.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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