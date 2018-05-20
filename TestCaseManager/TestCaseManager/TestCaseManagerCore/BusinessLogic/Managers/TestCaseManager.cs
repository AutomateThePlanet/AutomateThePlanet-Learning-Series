// <copyright file="TestCaseManager.cs" company="Automate The Planet Ltd.">
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
    using System.Collections.Generic;
    using System.Linq;
    using AAngelov.Utilities.Entities;
    using AAngelov.Utilities.Enums;
    using AAngelov.Utilities.Managers;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Enums;

    /// <summary>
    /// Contains helper methods for working with TestCase entities
    /// </summary>
    public static class TestCaseManager
    {
        /// <summary>
        /// All test cases in team project query expression
        /// </summary>
        private const string AllTestCasesInTeamProjectQueryExpression = "SELECT [System.Id], [System.Title] FROM WorkItems WHERE [System.WorkItemType] = 'Test Case' AND [Team Project] = '{0}'";

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets all test cases from all test suites.d
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="suiteEntries">The test suite collection.</param>
        /// <param name="alreadyCheckedSuitesIds">The already checked suites ids.</param>
        /// <returns>
        /// list with all test cases
        /// </returns>
        public static List<TestCase> GetAllTestCasesFromSuiteCollection(ITestPlan testPlan, ITestSuiteCollection suiteEntries, List<int> alreadyCheckedSuitesIds = null)
        {
            if (alreadyCheckedSuitesIds == null)
            {
                alreadyCheckedSuitesIds = new List<int>();
            }
            List<TestCase> testCases = new List<TestCase>();

            foreach (ITestSuiteBase currentSuite in suiteEntries)
            {
                if (currentSuite != null && !alreadyCheckedSuitesIds.Contains(currentSuite.Id))
                {
                    alreadyCheckedSuitesIds.Add(currentSuite.Id);
                    currentSuite.Refresh();
                    foreach (var currentTestCase in currentSuite.TestCases)
                    {
                        TestCase testCaseToAdd = new TestCase(currentTestCase.TestCase, currentSuite, testPlan);
                        if (!testCases.Contains(testCaseToAdd))
                        {
                            testCases.Add(testCaseToAdd);
                        }
                    }

                    if (currentSuite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite staticTestSuite = currentSuite as IStaticTestSuite;
                        if (staticTestSuite != null && (staticTestSuite.SubSuites.Count > 0))
                        {
                            List<TestCase> testCasesInternal = GetAllTestCasesFromSuiteCollection(testPlan, staticTestSuite.SubSuites, alreadyCheckedSuitesIds);
                            foreach (var currentTestCase in testCasesInternal)
                            {
                                if (!testCases.Contains(currentTestCase))
                                {
                                    testCases.Add(currentTestCase);
                                }
                            }
                        }
                    }
                }
            }
            return testCases;
        }

        /// <summary>
        /// Gets the most recent test case result.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static string GetMostRecentTestCaseResult(ITestPlan testPlan, int testCaseId)
        {
            var testPoints = TestPointManager.GetTestPointsByTestCaseId(testPlan, testCaseId);
            ITestPoint lastTestPoint = null;
            if (testPoints.Count > 0)
            {
                lastTestPoint = testPoints.Last();
            }
            string mostRecentResult = "Active";
            ITestCaseResult lastTestCaseResult = null;
            if (lastTestPoint != null)
            {
                lastTestCaseResult = lastTestPoint.MostRecentResult;
            }
            if (lastTestCaseResult != null)
            {
                mostRecentResult = lastTestCaseResult.Outcome.ToString();
            }

            return mostRecentResult;
        }

        /// <summary>
        /// Gets the most recent execution comment.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static string GetMostRecentExecutionComment(ITestPlan testPlan, int testCaseId)
        {
            var testPoints = TestPointManager.GetTestPointsByTestCaseId(testPlan, testCaseId);
            ITestPoint lastTestPoint = null;
            if (testPoints.Count > 0)
            {
                lastTestPoint = testPoints.Last();
            }
            string mostRecentExecutionComment = string.Empty;

            if (lastTestPoint != null && lastTestPoint.MostRecentResult != null && !string.IsNullOrEmpty(lastTestPoint.MostRecentResult.Comment))
            {
                mostRecentExecutionComment = lastTestPoint.MostRecentResult.Comment;
            }

            return mostRecentExecutionComment;
        }

        /// <summary>
        /// Gets the latest execution times.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>
        /// latest execution times
        /// </returns>
        public static List<TestCaseRunResult> GetLatestExecutionTimes(ITestManagementTeamProject project, ITestPlan testPlan, int testCaseId)
        {
            List<TestCaseRunResult> executionTimes = new List<TestCaseRunResult>();
            var testPoints = TestPointManager.GetTestPointsByTestCaseId(testPlan, testCaseId);
            List<TestCaseResultIdentifier> alreadyAddedRuns = new List<TestCaseResultIdentifier>();
            if (testPoints != null && testPoints.Count > 0)
            {
                foreach (ITestPoint currentTestPoint in testPoints)
                {
                    if (currentTestPoint.History != null)
                    {
                        foreach (var currentHistoryTestPoint in currentTestPoint.History)
                        {
                            if (currentHistoryTestPoint != null &&
                                currentHistoryTestPoint.MostRecentResultId != 0 &&
                                currentHistoryTestPoint.MostRecentResultId != 0 &&
                                currentHistoryTestPoint.MostRecentResultOutcome.Equals(TestOutcome.Passed))
                            {
                                ITestCaseResult testRun = project.TestResults.Find(currentHistoryTestPoint.MostRecentRunId, currentHistoryTestPoint.MostRecentResultId);
                                if (testRun.Duration.Ticks > 0 && !alreadyAddedRuns.Contains(testRun.Id))
                                {
                                   executionTimes.Add(new TestCaseRunResult(
                                   testRun.DateStarted,
                                   testRun.DateCompleted,
                                   testRun.Duration,
                                   testRun.RunByName));
                                   alreadyAddedRuns.Add(testRun.Id);
                                }
                               
                            }
                        }
                    }
                   
                }
            }

            return executionTimes;
        }

        /// <summary>
        /// Sets the new execution outcome.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="newExecutionOutcome">The new execution outcome.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="testCaseRuns">The test case runs.</param>
        public static void SetNewExecutionOutcome(this TestCase currentTestCase, ITestPlan testPlan, TestCaseExecutionType newExecutionOutcome, string comment, Dictionary<int, TestCaseRun> testCaseRuns)
        {
            if (currentTestCase.ITestCase.Owner == null)
            {
                return;
            }
            var testPoints = testPlan.QueryTestPoints(string.Format("SELECT * FROM TestPoint WHERE TestCaseId = {0} ", currentTestCase.Id));
            var testRun = testPlan.CreateTestRun(false);
            currentTestCase.IsRunning = string.Empty;
            DateTime startedDate = DateTime.Now;
            DateTime lastStartedDate = DateTime.Now;

            DateTime endDate = DateTime.Now;
            TimeSpan durationBeforePauses = new TimeSpan();
            if (testCaseRuns.ContainsKey(currentTestCase.Id))
            {
                lastStartedDate = testCaseRuns[currentTestCase.Id].LastStartedTime;
                startedDate = testCaseRuns[currentTestCase.Id].StartTime;
                durationBeforePauses = testCaseRuns[currentTestCase.Id].Duration;
                testCaseRuns.Remove(currentTestCase.Id);
            }
            testRun.DateStarted = startedDate;
            testRun.AddTestPoint(testPoints.Last(), ExecutionContext.TestManagementTeamProject.TestManagementService.AuthorizedIdentity);
            TimeSpan totalDuration = new TimeSpan((DateTime.Now - lastStartedDate).Ticks + durationBeforePauses.Ticks);
            testRun.DateCompleted = endDate;
            testRun.Save();

            var result = testRun.QueryResults()[0];
            result.Owner = ExecutionContext.TestManagementTeamProject.TestManagementService.AuthorizedIdentity;
            result.RunBy = ExecutionContext.TestManagementTeamProject.TestManagementService.AuthorizedIdentity;
            result.State = TestResultState.Completed;
            result.DateStarted = startedDate;
            result.Duration = totalDuration;
            result.DateCompleted = endDate;
            result.Comment = comment;
            switch (newExecutionOutcome)
            {
                case TestCaseExecutionType.Active:
                    result.Outcome = TestOutcome.None;
                    result.Duration = new TimeSpan();
                    break;
                case TestCaseExecutionType.Passed:
                    result.Outcome = TestOutcome.Passed;
                    break;
                case TestCaseExecutionType.Failed:
                    result.Outcome = TestOutcome.Failed;
                    break;
                case TestCaseExecutionType.Blocked:
                    result.Outcome = TestOutcome.Blocked;
                    break;
            }
            result.Save();
        }

        /// <summary>
        /// Gets the type of the test case execution.
        /// </summary>
        /// <param name="executionTypeStr">The execution type string.</param>
        /// <returns></returns>
        public static TestCaseExecutionType GetTestCaseExecutionType(string executionTypeStr)
        {
            TestCaseExecutionType testCaseExecutionType = (TestCaseExecutionType)Enum.Parse(typeof(TestCaseExecutionType), executionTypeStr);
            switch (testCaseExecutionType)
            {
                case TestCaseExecutionType.All:
                    break;
                case TestCaseExecutionType.Active:
                    break;
                case TestCaseExecutionType.Passed:
                    break;
                case TestCaseExecutionType.Failed:
                    break;
                case TestCaseExecutionType.Blocked:
                    break;
                default:
                    testCaseExecutionType = TestCaseExecutionType.Active;
                    break;
            }
            return testCaseExecutionType;
        }

        /// <summary>
        /// Gets all test case from suite.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="includeExecutionStatus">if set to <c>true</c> [include execution status].</param>
        /// <returns>
        /// list of all test cases in the list
        /// </returns>
        public static List<TestCase> GetAllTestCaseFromSuite(ITestPlan testPlan, int suiteId, bool includeExecutionStatus = true)
        {
            List<TestCase> testCases = new List<TestCase>();
            testPlan.Refresh();
            ITestSuiteBase currentSuite = testPlan.Project.TestSuites.Find(suiteId);
            currentSuite.Refresh();
            foreach (var currentTestCase in currentSuite.TestCases)
            {
                TestCase testCaseToAdd = new TestCase(currentTestCase.TestCase, currentSuite, testPlan, includeExecutionStatus);
                if (!testCases.Contains(testCaseToAdd))
                {
                    testCases.Add(testCaseToAdd);
                }
            }
            log.InfoFormat("Load all test cases in the suite with Title= \"{0}\" id = \"{1}\"", currentSuite.Title, currentSuite.Id);
            return testCases;
        }

        /// <summary>
        /// Gets the associated automation.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <returns>the associated automation</returns>
        public static AssociatedAutomation GetAssociatedAutomation(this ITestCase testCase)
        {
            AssociatedAutomation associatedAutomation;
            if (testCase.Implementation == null)
            {
                associatedAutomation = new AssociatedAutomation();
            }
            else
            {
                associatedAutomation = new AssociatedAutomation(testCase.Implementation.ToString());
            }

            return associatedAutomation;
        }

        /// <summary>
        /// Sets the associated automation of specific test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="testForAssociation">The test to be associated with.</param>
        /// <param name="testType">Type of the test.</param>
        public static void SetAssociatedAutomation(this ITestCase testCase, ITestManagementTeamProject testManagementTeamProject, Test testForAssociation, string testType)
        {
            try
            {
                log.InfoFormat("Associate test case with title= {0}, id= {1} to test: {2}, test type= {3}", testCase.Title, testCase.Id, testForAssociation, testType);
                ITmiTestImplementation imp = testManagementTeamProject.CreateTmiTestImplementation(testForAssociation.FullName, testType, testForAssociation.ClassName, testForAssociation.MethodId);
                testCase.Implementation = imp;
            }
            catch (NullReferenceException ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Gets all test cases in current test plan.
        /// </summary>
        /// <param name="initializeTestCaseStatus">if set to <c>true</c> [initialize test case status].</param>
        /// <returns>
        /// list of all test cases
        /// </returns>
        public static List<TestCase> GetAllTestCasesInTestPlan(ITestManagementTeamProject testManagementTeamProject, ITestPlan testPlan, bool initializeTestCaseStatus = true)
        {
            testPlan.Refresh();
            List<TestCase> testCasesList;
            testCasesList = new List<TestCase>();
            string fullQuery = string.Format(AllTestCasesInTeamProjectQueryExpression, testManagementTeamProject.TeamProjectName);
            IEnumerable<ITestCase> allTestCases = testManagementTeamProject.TestCases.Query(fullQuery);
            foreach (var currentTestCase in allTestCases)
            {
                TestCase testCaseToAdd = new TestCase(currentTestCase, currentTestCase.TestSuiteEntry.ParentTestSuite, testPlan, initializeTestCaseStatus);
                if (!testCasesList.Contains(testCaseToAdd))
                {
                    testCasesList.Add(testCaseToAdd);
                }
            }

            return testCasesList;
        }

        /// <summary>
        /// Saves the specified test case.
        /// </summary>
        /// <param name="sourceTestCase">The test case.</param>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="createNew">should be saved as new test case.</param>
        /// <param name="suiteId">The suite identifier.</param>
        /// <param name="testSteps">The test steps.</param>
        /// <param name="shouldAssignArea">if set to <c>true</c> [should assign area].</param>
        /// <param name="isMigration">if set to <c>true</c> [is migration].</param>
        /// <returns>
        /// the saved test case
        /// </returns>
        public static TestCase Save(
            this TestCase sourceTestCase, 
            ITestManagementTeamProject testManagementTeamProject, 
            ITestPlan testPlan,
            bool createNew, 
            int? suiteId, 
            ICollection<TestStep> testSteps, 
            bool shouldAssignArea = true, 
            bool isMigration = false)
        {
            TestCase currentTestCase = sourceTestCase;
            if (createNew)
            {
                ITestCase testCaseCore = testManagementTeamProject.TestCases.Create();
                currentTestCase = new TestCase(testCaseCore, sourceTestCase.ITestSuiteBase, testPlan);
            }
            if (shouldAssignArea)
            {
                currentTestCase.ITestCase.Area = sourceTestCase.Area;
            }
            currentTestCase.ITestCase.Description = sourceTestCase.ITestCase.Description;
            currentTestCase.ITestCase.Title = sourceTestCase.Title;
            currentTestCase.ITestCase.Priority = (int)sourceTestCase.Priority;
            currentTestCase.ITestCase.Actions.Clear();
            currentTestCase.ITestCase.Owner = testManagementTeamProject.TfsIdentityStore.FindByTeamFoundationId(sourceTestCase.TeamFoundationId);
            if (sourceTestCase.ITestCase.Implementation != null && isMigration)
            {
                currentTestCase.ITestCase.Implementation = sourceTestCase.ITestCase.Implementation;
            }
            List<Guid> addedSharedStepGuids = new List<Guid>();
            foreach (TestStep currentStep in testSteps)
            {
                if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.TestStepGuid))
                {
                    ISharedStep sharedStepCore = testManagementTeamProject.SharedSteps.Find(currentStep.SharedStepId);
                    ISharedStepReference sharedStepReferenceCore = currentTestCase.ITestCase.CreateSharedStepReference();
                    sharedStepReferenceCore.SharedStepId = sharedStepCore.Id;
                    currentTestCase.ITestCase.Actions.Add(sharedStepReferenceCore);
                    addedSharedStepGuids.Add(currentStep.TestStepGuid);
                }
                else if (!currentStep.IsShared)
                {
                    ITestStep testStepCore = currentTestCase.ITestCase.CreateTestStep();
                    testStepCore.Title = currentStep.ActionTitle;
                    testStepCore.ExpectedResult = currentStep.ActionExpectedResult;
                    currentTestCase.ITestCase.Actions.Add(testStepCore);
                }
            }

            if (suiteId != null)
            {
                var newSuite = TestSuiteManager.GetTestSuiteById(testManagementTeamProject, testPlan, (int)suiteId);
                sourceTestCase.ITestSuiteBase = newSuite;
            }
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
            if (suiteId != null)
            {
                SetTestCaseSuite(testManagementTeamProject, testPlan, (int)suiteId, currentTestCase);
            }

            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();

            return currentTestCase;
        }

        /// <summary>
        /// Copies the automatic clipboard.
        /// </summary>
        /// <param name="isCopy">if set to <c>true</c> [copy].</param>
        /// <param name="testCases">The test cases.</param>
        public static void CopyToClipboardTestCases(bool isCopy, List<TestCase> testCases)
        {
            ClipBoardCommand clipBoardCommand = isCopy ? ClipBoardCommand.Copy : ClipBoardCommand.Cut;
            ClipBoardTestCase clipBoardTestCase = new ClipBoardTestCase(testCases, clipBoardCommand);
            ClipBoardManager<ClipBoardTestCase>.CopyToClipboard(clipBoardTestCase);
            SerializationManager.IsSerializable(clipBoardTestCase);
        }

        /// <summary>
        /// Gets from clipboard the test cases
        /// </summary>
        /// <returns>the retrieved test cases</returns>
        public static ClipBoardTestCase GetFromClipboardTestCases()
        {
            ClipBoardTestCase clipBoardTestCase = ClipBoardManager<ClipBoardTestCase>.GetFromClipboard();

            if (clipBoardTestCase != null)
            {
                return clipBoardTestCase;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the test case core object by unique identifier.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static ITestCase GetTestCaseCoreObjectById(ITestManagementTeamProject testManagementTeamProject, int testCaseId)
        {
            ITestCase testCaseCore = null;
            try
            {
                testCaseCore = testManagementTeamProject.TestCases.Find(testCaseId);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return testCaseCore;
        }

        /// <summary>
        /// Finds all reference test cases for specific shared step.
        /// </summary>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        /// <returns></returns>
        public static List<TestCase> FindAllReferenceTestCasesForShareStep(ITestManagementTeamProject testManagementTeamProject, ITestPlan testPlan, int sharedStepId)
        {
            List<TestCase> filteredTestCases = new List<TestCase>();
            List<TestCase> allTestCases = GetAllTestCasesInTestPlan(testManagementTeamProject, testPlan);
            foreach (var currentTestCase in allTestCases)
            {
                foreach (var currentAction in currentTestCase.ITestCase.Actions)
                {
                    if (currentAction is ISharedStepReference)
                    {
                        ISharedStepReference currentSharedStepReference = currentAction as ISharedStepReference;
                        if (currentSharedStepReference.SharedStepId.Equals(sharedStepId))
                        {
                            filteredTestCases.Add(currentTestCase);
                            break;
                        }
                    }
                }
            }

            return filteredTestCases;
        }

        /// <summary>
        /// Adds the test cases without suites.
        /// </summary>
        /// <param name="testCasesList">The test cases list.</param>
        public static void AddTestCasesWithoutSuites(ITestManagementTeamProject testManagementTeamProject, ITestPlan testPlan, List<TestCase> testCasesList)
        {
            testPlan.Refresh();
            string fullQuery = string.Format(AllTestCasesInTeamProjectQueryExpression, testManagementTeamProject.TeamProjectName);
            IEnumerable<ITestCase> allTestCases = testManagementTeamProject.TestCases.Query(fullQuery);
            foreach (var currentTestCase in allTestCases)
            {
                TestCase testCaseToAdd = new TestCase(currentTestCase, null, ExecutionContext.Preferences.TestPlan);
                if (!testCasesList.Contains(testCaseToAdd))
                {
                    testCasesList.Add(testCaseToAdd);
                }
            }
        }

        /// <summary>
        /// Sets the test case suite.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="testCase">The test case.</param>
        private static void SetTestCaseSuite(ITestManagementTeamProject testManagementTeamProject, ITestPlan testPlan, int suiteId, TestCase testCase)
        {
            var newSuite = TestSuiteManager.GetTestSuiteById(testManagementTeamProject, testPlan, suiteId);
            if (newSuite != null)
            {
                newSuite.AddTestCase(testCase.ITestCase);
                testCase.ITestSuiteBase = newSuite;
            }
        }
    }
}