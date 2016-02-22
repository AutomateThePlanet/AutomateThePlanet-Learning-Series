// <copyright file="Navigator.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore
{
    using System;
    using System.Windows;
    using AAngelov.Utilities.UI.Core;

    /// <summary>
    /// Contains methods which navigate to different views with option to set different parameters
    /// </summary>
    public class Navigator : BaseNavigator
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static Navigator instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Navigator Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new Navigator();
                }

                return instance;
            }
        }

        /// <summary>
        /// Navigates to test cases initial view.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToTestCasesInitialView(FrameworkElement source)
        {
            string url = "/Views/TestCasesInitialView.xaml";
            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic shared steps initial view.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToSharedStepsInitialView(FrameworkElement source)
        {
            string url = "/Views/SharedStepsInitialView.xaml";
            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public void NavigateToTestCasesEditView(FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to test cases detailed view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public void NavigateToTestCasesDetailedView(FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = string.Format("/Views/TestCaseDetailedView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to appearance settings view.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToAppearanceSettingsView(FrameworkElement source)
        {
            this.Navigate(source, "/Views/SettingsView.xaml");
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public void NavigateToTestCasesEditView(FrameworkElement source, int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="isSharedStep">if set to <c>true</c> [is shared step].</param>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public void NavigateToTestCasesEditView(FrameworkElement source, bool isSharedStep, int sharedStepId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#isSharedStep={0}&sharedStepId={1}&createNew={2}&duplicate={3}", isSharedStep, sharedStepId, createNew, duplicate);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="isSharedStep">if set to <c>true</c> [is shared step].</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public void NavigateToTestCasesEditView(FrameworkElement source, bool isSharedStep, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#isSharedStep={0}&createNew={1}&duplicate={2}", isSharedStep, createNew, duplicate);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic test cases edit view. Shared step edit.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="sharedStepId">The test step unique identifier.</param>
        public void NavigateToTestCasesEditView(FrameworkElement source, bool isShared, int sharedStepId)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#isSharedStep={0}&sharedStepId={1}", isShared, sharedStepId);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to associate automation view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public void NavigateToAssociateAutomationView(FrameworkElement source, int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/AssociateTestView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public void NavigateToTestCasesEditView(FrameworkElement source, int suiteId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#suiteId={0}&createNew={1}&duplicate={2}", suiteId, createNew, duplicate);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic test case batch duplicate view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="loadTestCases">if set to <c>true</c> [load test cases].</param>
        /// <param name="testCaseIds">The test case ids.</param>
        public void NavigateToTestCaseBatchDuplicateView(FrameworkElement source, bool loadTestCases, bool loadSpecificTestCases)
        {
            string url = string.Format("/Views/TestCaseBatchDuplicateView.xaml#loadTestCases={0}&loadSpecificTestCases={1}", loadTestCases, loadSpecificTestCases);

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to test cases edit view from associated automation.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToTestCasesEditViewFromAssociatedAutomation(FrameworkElement source)
        {
            string url = "/Views/TestCaseEditView.xaml#comesFromAssociatedAutomation=true";

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates to test case run statistics view.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToTestCaseRunStatisticsView(FrameworkElement source)
        {
            string url = "/Views/TestCaseRunStatisticsView.xaml";

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic project selection.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToProjectSelection(FrameworkElement source)
        {
            string url = "/Views/ProjectSelectionView.xaml#skipAutoLoad=true";

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic test plans edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        public void NavigateToTestPlansEdit(FrameworkElement source)
        {
            string url = "/Views/TestPlansEditView.xaml";

            this.Navigate(source, url);
        }

        /// <summary>
        /// Navigates the automatic test cases execution arrangement.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public void NavigateToTestCasesExecutionArrangement(FrameworkElement source, int suiteId)
        {
            string url = string.Format("/Views/TestCaseExecutionArrangmentView.xaml#suiteId={0}", suiteId);
            this.Navigate(source, url);
        }
    }
}