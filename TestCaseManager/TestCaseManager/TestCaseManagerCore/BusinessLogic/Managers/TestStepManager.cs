// <copyright file="TestStepManager.cs" company="Automate The Planet Ltd.">
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
    using System.Text;
    using System.Text.RegularExpressions;
    using AAngelov.Utilities.Enums;
    using AAngelov.Utilities.Managers;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;

    /// <summary>
    /// Contains helper methods for working with TestStep entities
    /// </summary>
    public static class TestStepManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The default unique identifier string
        /// </summary>
        private static readonly string DefaultGuidString = default(Guid).ToString();

        /// <summary>
        /// The regex pattern namespace initializations
        /// </summary>
        private static string RegexPatternNamespaceInitializations = @"\s*(?<Namespace>[\w.]{1,})\((?<GenParam>[a-zA-Z1-9\-]{1,})\)\s*=\s*(?<NewValue>[\W\w\s]*);{1}";
        /// <summary>
        /// The regext pattern no namespace initializations
        /// </summary>
        private static string RegextPatternNoNamespaceInitializations = @"\s*\((?<GenParam>[a-zA-Z1-9\-]{1,})\)\s*=\s*(?<NewValue>[\W\w\s]*);{1}";

        /// <summary>
        /// The regext pattern step title
        /// </summary>
        private static string RegextPatternStepTitle = @"\s*(?<Namespace>[\w.]{1,})\((?<GenParam>[a-zA-Z,1-9\-]{1,})\)\s*:\s*(?<ShareStepTitle>[\w\W]*)";

        /// <summary>
        /// Gets the test steps from test actions.
        /// </summary>
        /// <param name="testActions">The test actions.</param>
        /// <param name="alreadyAddedSharedSteps">The already added shared steps.</param>
        /// <param name="sharedSteps">The shared steps.</param>
        /// <returns>list of all test steps</returns>
        public static List<TestStep> GetTestStepsFromTestActions(ITestManagementTeamProject testManagementTeamProject, ICollection<ITestAction> testActions)
        {
            List<TestStep> testSteps = new List<TestStep>();

            foreach (var currentAction in testActions)
            {
                if (currentAction is ITestStep)
                {
                    Guid testStepGuid = Guid.NewGuid();
                    testSteps.Add(new TestStep(false, string.Empty, testStepGuid, currentAction as ITestStep));
                }
                else if (currentAction is ISharedStepReference)
                {
                    ISharedStepReference currentSharedStepReference = currentAction as ISharedStepReference;
                    ISharedStep currentSharedStep = testManagementTeamProject.SharedSteps.Find(currentSharedStepReference.SharedStepId);
                    testSteps.AddRange(TestStepManager.GetAllTestStepsInSharedStep(currentSharedStep));
                }
            }

            return testSteps;
        }

        /// <summary>
        /// Copies the automatic clipboard.
        /// </summary>
        /// <param name="isCopy">if set to <c>true</c> [copy].</param>
        /// <param name="testSteps">The test steps.</param>
        public static void CopyToClipboardTestSteps(bool isCopy, List<TestStep> testSteps)
        {
            ClipBoardCommand clipBoardCommand = isCopy ? ClipBoardCommand.Copy : ClipBoardCommand.Cut;
            ClipBoardTestStep clipBoardTestStep = new ClipBoardTestStep(testSteps, clipBoardCommand);
            ClipBoardManager<ClipBoardTestStep>.CopyToClipboard(clipBoardTestStep);
        }

        /// <summary>
        /// Gets from clipboard the test steps
        /// </summary>
        /// <returns>the retrieved test steps</returns>
        public static ClipBoardTestStep GetFromClipboardTestSteps()
        {
            ClipBoardTestStep clipBoardTestStep = ClipBoardManager<ClipBoardTestStep>.GetFromClipboard();

            if (clipBoardTestStep != null)
            {
                return clipBoardTestStep;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines whether [is there shared step selected] from [the specified selected test steps].
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>is there shared step selected</returns>
        public static bool IsThereSharedStepSelected(List<TestStep> selectedTestSteps)
        {
            bool isThereSharedStepSelected = false;
            foreach (TestStep currentStep in selectedTestSteps)
            {
                if (currentStep.IsShared)
                {
                    isThereSharedStepSelected = true;
                    break;
                }
            }

            return isThereSharedStepSelected;
        }

        /// <summary>
        /// Gets the test steps from shared step.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        /// <returns>list of all test steps in the specified shared step</returns>
        public static List<TestStep> GetAllTestStepsInSharedStep(ISharedStep currentSharedStep, bool includeSharedStep = true)
        {
            List<TestStep> testSteps = new List<TestStep>();
            Guid sharedStepUniqueGuid = Guid.NewGuid();
            if (currentSharedStep != null && currentSharedStep.Actions != null)
            {
                foreach (var currentSharedStepAction in currentSharedStep.Actions)
                {
                    if (includeSharedStep)
                    {
                        testSteps.Add(new TestStep(true, currentSharedStep.Title, sharedStepUniqueGuid, currentSharedStepAction as ITestStep, currentSharedStep.Id));
                    }
                    else
                    {
                        testSteps.Add(new TestStep(false, currentSharedStep.Title, Guid.NewGuid(), currentSharedStepAction as ITestStep));
                    }
                }
            }

            return testSteps;
        }

        /// <summary>
        /// Generates the test steps text - action + expected result.
        /// </summary>
        /// <param name="testSteps">The test steps.</param>
        /// <returns>test steps text</returns>
        public static string GenerateTestStepsText(List<TestStep> testSteps)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < testSteps.Count; i++)
            {
                sb.AppendLine(string.Format("{0}    {1}", testSteps[i].ActionTitle, testSteps[i].ActionExpectedResult));
                if (i < testSteps.Count - 1)
                {
                    sb.AppendLine(new string('-', 70));
                }
            }

            string result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Creates the new test step.
        /// </summary>
        /// <param name="testBase">The test case.</param>
        /// <param name="stepTitle">The step title.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <param name="testStepGuid">The unique identifier.</param>
        /// <returns>the test step object</returns>
        public static TestStep CreateNewTestStep(ITestBase testBase, string stepTitle, string expectedResult, Guid testStepGuid)
        {
            ITestStep testStepCore = testBase.CreateTestStep();
            testStepCore.ExpectedResult = expectedResult;
            testStepCore.Title = stepTitle;
            if (testStepGuid == default(Guid))
            {
                testStepGuid = Guid.NewGuid();
            }

            TestStep testStepToInsert = new TestStep(false, string.Empty, testStepGuid, testStepCore);

            return testStepToInsert;
        }

        /// <summary>
        /// Creates the new shared step.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="sharedStepTitle">The shared step title.</param>
        /// <param name="stepTitle">The step title.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <returns>the shared step core object</returns>
        public static ISharedStep CreateNewSharedStep(TestCase testCase, string sharedStepTitle, string stepTitle, string expectedResult)
        {
            ISharedStepReference sharedStepReferenceCore = testCase.ITestCase.CreateSharedStepReference();
            ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
            sharedStepReferenceCore.SharedStepId = sharedStepCore.Id;
            sharedStepCore.Title = sharedStepTitle;
            ITestStep testStepCore = sharedStepCore.CreateTestStep();
            testStepCore.ExpectedResult = expectedResult;
            testStepCore.Title = stepTitle;
            sharedStepCore.Actions.Add(testStepCore);

            return sharedStepCore;
        }

        /// <summary>
        /// Creates the new shared step.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="sharedStepTitle">The shared step title.</param>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>the shared step core object</returns>
        public static ISharedStep CreateNewSharedStep(this TestCase testCase, string sharedStepTitle, List<TestStep> selectedTestSteps)
        {
            log.InfoFormat("Create New Shared Step with Title= {0}", sharedStepTitle);
            ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
            sharedStepCore.Title = sharedStepTitle;

            sharedStepCore.Save();
            AddTestStepsToSharedStep(sharedStepCore, Guid.NewGuid(), selectedTestSteps, sharedStepTitle);
            sharedStepCore.Save();

            return sharedStepCore;
        }

        /// <summary>
        /// Gets all shared step core objects.
        /// </summary>
        /// <returns>list of all shared step core objects</returns>
        public static List<ISharedStep> GetAllSharedSteps()
        {
            return ExecutionContext.TestManagementTeamProject.SharedSteps.Query("select * from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Shared Steps'").ToList();
        }

        /// <summary>
        /// Updates the generic shared steps.
        /// </summary>
        /// <param name="testSteps">The test steps.</param>
        public static void UpdateGenericSharedSteps(ICollection<TestStep> testSteps)
        {
            Dictionary<string, Dictionary<string, string>> genericParameters = new Dictionary<string, Dictionary<string, string>>();
            foreach (TestStep currentTestStep in testSteps)
            {
                ExtractGenericParameteresFromNonSharedStep(currentTestStep, genericParameters);
                ReplaceGenericParametersWithSpecifiedValues(currentTestStep, genericParameters);
            }
        }

        /// <summary>
        /// Extracts the generic parameteres from non shared step.
        /// </summary>
        /// <param name="currentTestStep">The current test step.</param>
        /// <param name="genericParameters">The generic parameters.</param>
        private static void ExtractGenericParameteresFromNonSharedStep(TestStep currentTestStep, Dictionary<string, Dictionary<string, string>> genericParameters)
        {
            Regex regexNamespaceInitializations = new Regex(RegexPatternNamespaceInitializations, RegexOptions.None);
            Regex regexNoNamespaceInitializations = new Regex(RegextPatternNoNamespaceInitializations, RegexOptions.None);
            string[] lines = null;
            if (currentTestStep.ActionTitle != null)
            {
                lines = currentTestStep.ActionTitle.Split(new string[] { "\n" }, StringSplitOptions.None);
            }
            if (lines != null)
            {
                foreach (string currentLine in lines)
                {
                    Match m = regexNamespaceInitializations.Match(currentLine);
                    if (m.Success)
                    {
                        if (!genericParameters.Keys.Contains(m.Groups["Namespace"].Value))
                        {
                            Dictionary<string, string> genericTypesDictionary = new Dictionary<string, string>();
                            genericTypesDictionary.Add(m.Groups["GenParam"].Value, m.Groups["NewValue"].Value.Trim().TrimEnd(';'));
                            genericParameters.Add(m.Groups["Namespace"].Value, genericTypesDictionary);
                        }
                        else
                        {
                            if (!genericParameters[m.Groups["Namespace"].Value].Keys.Contains(m.Groups["GenParam"].Value))
                            {
                                genericParameters[m.Groups["Namespace"].Value].Add(m.Groups["GenParam"].Value, m.Groups["NewValue"].Value.Trim());
                            }
                            else
                            {
                                genericParameters[m.Groups["Namespace"].Value][m.Groups["GenParam"].Value] = m.Groups["NewValue"].Value.Trim();
                            }
                        }
                    }
                    else if (regexNoNamespaceInitializations.Match(currentLine).Success)
                    {
                        Match matchNoNamespace = regexNoNamespaceInitializations.Match(currentLine);

                        if (!genericParameters.Keys.Contains(default(Guid).ToString()))
                        {
                            Dictionary<string, string> genericTypesDictionary = new Dictionary<string, string>();
                            genericTypesDictionary.Add(matchNoNamespace.Groups["GenParam"].Value, matchNoNamespace.Groups["NewValue"].Value.Trim());
                            genericParameters.Add(DefaultGuidString, genericTypesDictionary);
                        }
                        else
                        {
                            if (!genericParameters[DefaultGuidString].Keys.Contains(matchNoNamespace.Groups["GenParam"].Value))
                            {
                                genericParameters[DefaultGuidString].Add(matchNoNamespace.Groups["GenParam"].Value, matchNoNamespace.Groups["NewValue"].Value.Trim());
                            }
                            else
                            {
                                genericParameters[DefaultGuidString][matchNoNamespace.Groups["GenParam"].Value] = matchNoNamespace.Groups["NewValue"].Value.Trim();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether [is initialization test step] [the specified current test step].
        /// </summary>
        /// <param name="currentTestStep">The current test step.</param>
        /// <returns></returns>
        public static bool IsInitializationTestStep(TestStep currentTestStep)
        {
            bool isInitializationTestStep = false;
            //if (currentTestStep.IsShared)
            //{
            //    return isInitializationTestStep;
            //}
            Regex regexNamespaceInitializations = new Regex(RegexPatternNamespaceInitializations, RegexOptions.None);
            Regex regexNoNamespaceInitializations = new Regex(RegextPatternNoNamespaceInitializations, RegexOptions.None);
            string[] lines = currentTestStep.ActionTitle.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string currentLine in lines)
            {
                Match m = regexNamespaceInitializations.Match(currentLine);
                if (m.Success)
                {
                    isInitializationTestStep = true;
                    break;
                }
                else if (regexNoNamespaceInitializations.Match(currentLine).Success)
                {
                    Match matchNoNamespace = regexNoNamespaceInitializations.Match(currentLine);
                    isInitializationTestStep = true;
                    break;
                }
            }

            return isInitializationTestStep;
        }

        /// <summary>
        /// Replaces the generic parameters with specified values.
        /// </summary>
        /// <param name="currentTestStep">The current test step.</param>
        /// <param name="genericParameters">The generic parameters.</param>
        private static void ReplaceGenericParametersWithSpecifiedValues(TestStep currentTestStep, Dictionary<string, Dictionary<string, string>> genericParameters)
        {
            Regex r1 = new Regex(RegextPatternStepTitle, RegexOptions.Singleline);
            if (genericParameters.Keys.Count > 0)
            {
                Match currentMatch = r1.Match(currentTestStep.Title);
                string genParamsStr = currentMatch.Groups["GenParam"].Value;
                string[] genParams = genParamsStr.Split(',');
                int initializeCount = genParams.Length;
                bool reinitialized = false;
                foreach (string currentNamespace in genericParameters.Keys)
                {
                    if (currentMatch.Success)
                    {
                        if (currentMatch.Groups["Namespace"].Value.EndsWith(currentNamespace))
                        {
                            currentTestStep.ActionTitle = currentTestStep.OriginalActionTitle;
                            currentTestStep.ActionExpectedResult = currentTestStep.OriginalActionExpectedResult;
                            currentTestStep.Title = currentTestStep.OriginalTitle;
                            reinitialized = true;
                            foreach (string currentKey in genericParameters[currentNamespace].Keys)
                            {
                                initializeCount--;
                                string strToBeReplaced = string.Concat("(", currentKey, ")");
                                currentTestStep.ActionTitle = currentTestStep.ActionTitle.Replace(strToBeReplaced, genericParameters[currentNamespace][currentKey]);
                                currentTestStep.ActionExpectedResult = currentTestStep.ActionExpectedResult.Replace(strToBeReplaced, genericParameters[currentNamespace][currentKey]);
                            }
                            foreach (string currentGenParam in genParams)
                            {
                                if (!genericParameters[currentNamespace].Keys.Contains(currentGenParam) && genericParameters.Keys.Contains(DefaultGuidString) && genericParameters[DefaultGuidString].Keys.Contains(currentGenParam))
                                {
                                    if (!reinitialized)
                                    {
                                        currentTestStep.ActionTitle = currentTestStep.OriginalActionTitle;
                                        currentTestStep.ActionExpectedResult = currentTestStep.OriginalActionExpectedResult;
                                        currentTestStep.Title = currentTestStep.OriginalTitle;
                                    }
                                    initializeCount--;
                                    string strToBeReplaced = string.Concat("(", currentGenParam, ")");

                                    currentTestStep.ActionTitle = currentTestStep.ActionTitle.Replace(strToBeReplaced, genericParameters[DefaultGuidString][currentGenParam]);
                                    currentTestStep.ActionExpectedResult = currentTestStep.ActionExpectedResult.Replace(strToBeReplaced, genericParameters[DefaultGuidString][currentGenParam]);
                                }
                            }
                        }
                    }
                }
                if (initializeCount != 0)
                {
                    foreach (string currentGenParam in genParams)
                    {
                        if (genericParameters.Keys.Contains(DefaultGuidString) && genericParameters[DefaultGuidString].Keys.Contains(currentGenParam) && initializeCount != 0)
                        {
                            if (!reinitialized)
                            {
                                currentTestStep.ActionTitle = currentTestStep.OriginalActionTitle;
                                currentTestStep.ActionExpectedResult = currentTestStep.OriginalActionExpectedResult;
                                currentTestStep.Title = currentTestStep.OriginalTitle;
                                reinitialized = true;
                            }
                            initializeCount--;
                            string strToBeReplaced = string.Concat("(", currentGenParam, ")");

                            currentTestStep.ActionTitle = currentTestStep.ActionTitle.Replace(strToBeReplaced, genericParameters[DefaultGuidString][currentGenParam]);
                            currentTestStep.ActionExpectedResult = currentTestStep.ActionExpectedResult.Replace(strToBeReplaced, genericParameters[DefaultGuidString][currentGenParam]);
                        }
                    }
                }
            }
            else
            {
                currentTestStep.ActionTitle = currentTestStep.OriginalActionTitle;
                currentTestStep.ActionExpectedResult = currentTestStep.OriginalActionExpectedResult;
            }
        }

        /// <summary>
        /// Adds the parentheses automatic parameter.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        private static string AddParenthesesToParam(string param)
        {
            return string.Concat("(", param, ")");
        }

        /// <summary>
        /// Reinitializes the test step.
        /// </summary>
        /// <param name="currentTestStep">The current test step.</param>
        private static void ReinitializeTestStep(TestStep currentTestStep)
        {
            currentTestStep.ActionTitle = currentTestStep.OriginalActionTitle;
            currentTestStep.ActionExpectedResult = currentTestStep.OriginalActionExpectedResult;
        }

        /// <summary>
        /// Adds the test steps to shared steps actions.
        /// </summary>
        /// <param name="sharedStepCore">The core shared step object.</param>
        /// <param name="sharedStepGuid">The shared step unique identifier.</param>
        /// <param name="selectedTestSteps">The test steps to add.</param>
        /// <param name="sharedStepTitle">The shared step title.</param>
        private static void AddTestStepsToSharedStep(ISharedStep sharedStepCore, Guid sharedStepGuid, List<TestStep> selectedTestSteps, string sharedStepTitle)
        {
            foreach (TestStep currentTestStep in selectedTestSteps)
            {
                ITestStep testStepCore = sharedStepCore.CreateTestStep();
                testStepCore.ExpectedResult = currentTestStep.ActionExpectedResult;
                testStepCore.Title = currentTestStep.ActionTitle;
                sharedStepCore.Actions.Add(testStepCore);
                currentTestStep.TestStepGuid = sharedStepGuid;
                currentTestStep.Title = sharedStepTitle;
                currentTestStep.IsShared = true;
                currentTestStep.SharedStepId = sharedStepCore.Id;
            }
        }

        /// <summary>
        /// Edits the test step action title.
        /// </summary>
        /// <param name="currentTestStep">The current test step.</param>
        /// <param name="newActionTitle">The new action title.</param>
        public static void EditTestStepActionTitle(TestStep currentTestStep, string newActionTitle)
        {
            UndoRedoManager.Instance().Push((step, t) => EditTestStepActionTitle(currentTestStep, t), currentTestStep, currentTestStep.OriginalActionTitle, "Change the test step action title");
            log.InfoFormat("Change ActionTitle from {0} to {1}", currentTestStep.ActionTitle, newActionTitle);
            currentTestStep.ActionTitle = newActionTitle;
            currentTestStep.OriginalActionTitle = newActionTitle;
        }

        /// <summary>
        /// Edits the test step action expected result.
        /// </summary>
        /// <param name="currentTestStep">The current test step.</param>
        /// <param name="newActionExpectedResult">The new action expected result.</param>
        public static void EditTestStepActionExpectedResult(TestStep currentTestStep, string newActionExpectedResult)
        {
            UndoRedoManager.Instance().Push((step, t) => EditTestStepActionExpectedResult(currentTestStep, t), currentTestStep, currentTestStep.OriginalActionExpectedResult, "Change the test step expected result");
            log.InfoFormat("Change ActionTitle from {0} to {1}", currentTestStep.ActionExpectedResult, newActionExpectedResult);
            currentTestStep.ActionExpectedResult = newActionExpectedResult;
            currentTestStep.OriginalActionExpectedResult = newActionExpectedResult;
        }
    }
}