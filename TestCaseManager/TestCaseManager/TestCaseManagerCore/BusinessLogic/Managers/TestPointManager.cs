// <copyright file="TestPointManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains helper methods for working with ITestPoint objects
    /// </summary>
    public static class TestPointManager
    {
        private const string allTestPointsQueryExpression = "SELECT * FROM TestPoint WHERE TestCaseId = {0}";

        /// <summary>
        /// Gets the test points by test case unique identifier.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static ITestPointCollection GetTestPointsByTestCaseId(ITestPlan testPlan, int testCaseId)
        {
            return testPlan.QueryTestPoints(string.Format(allTestPointsQueryExpression, testCaseId));
        }
    }
}