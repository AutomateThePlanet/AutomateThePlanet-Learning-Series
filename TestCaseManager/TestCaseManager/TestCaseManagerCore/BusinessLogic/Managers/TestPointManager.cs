// <copyright file="TestPointManager.cs" company="Automate The Planet Ltd.">
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