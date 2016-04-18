// <copyright file="TestCasesMigrationViewModel.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using AAngelov.Utilities.Entities;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Provides methods and properties related to the Migration View
    /// </summary>
    public class TestCasesMigrationViewModel : BaseProjectSelectionViewModel
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The source preferences
        /// </summary>
        private Preferences sourcePreferences;

        /// <summary>
        /// The source TFS team project collection
        /// </summary>
        private TfsTeamProjectCollection sourceTfsTeamProjectCollection;

        /// <summary>
        /// The source team project
        /// </summary>
        private ITestManagementTeamProject sourceTeamProject;

        /// <summary>
        /// The destination preferences
        /// </summary>
        private Preferences destinationPreferences;

        /// <summary>
        /// The destination TFS team project collection
        /// </summary>
        private TfsTeamProjectCollection destinationTfsTeamProjectCollection;

        /// <summary>
        /// The destination team project
        /// </summary>
        private ITestManagementTeamProject destinationTeamProject;

        /// <summary>
        /// The source full team project name
        /// </summary>
        private string sourceFullTeamProjectName;

        /// <summary>
        /// The destination full team project name
        /// </summary>
        private string destinationFullTeamProjectName;

        /// <summary>
        /// The suites mapping
        /// </summary>
        private Dictionary<int, int> suitesMapping;

        /// <summary>
        /// The shared steps mapping
        /// </summary>
        private Dictionary<int, int> sharedStepsMapping;

        /// <summary>
        /// The test cases mapping
        /// </summary>
        private Dictionary<int, int> testCasesMapping;

        /// <summary>
        /// The default json folder
        /// </summary>
        private string defaultJsonFolder;

        /// <summary>
        /// The migration shared steps retry json path
        /// </summary>
        private string migrationSharedStepsRetryJsonPath;

        /// <summary>
        /// The migration suites retry json path
        /// </summary>
        private string migrationSuitesRetryJsonPath;

        /// <summary>
        /// The migration test cases retry json path
        /// </summary>
        private string migrationTestCasesRetryJsonPath;

        /// <summary>
        /// The migration add test cases to suites retry json path
        /// </summary>
        private string migrationAddTestCasesToSuitesRetryJsonPath;

        /// <summary>
        /// The is shared steps migration finished
        /// </summary>
        private bool isSharedStepsMigrationFinished;

        /// <summary>
        /// The is suites migration finished
        /// </summary>
        private bool isSuitesMigrationFinished;

        /// <summary>
        /// The is test cases migration finished
        /// </summary>
        private bool isTestCasesMigrationFinished;

        /// <summary>
        /// The shared steps migration log manager
        /// </summary>
        private MigrationLogManager sharedStepsMigrationLogManager;

        /// <summary>
        /// The suites migration log manager
        /// </summary>
        private MigrationLogManager suitesMigrationLogManager;

        /// <summary>
        /// The test cases migration log manager
        /// </summary>
        private MigrationLogManager testCasesMigrationLogManager;

        /// <summary>
        /// The test cases add to suites migration log manager
        /// </summary>
        private MigrationLogManager testCasesAddToSuitesMigrationLogManager;

        /// <summary>
        /// Gets or sets the cancellation token source.
        /// </summary>
        /// <value>
        /// The cancellation token source.
        /// </value>
        private CancellationTokenSource loggingCancellationTokenSource;

        /// <summary>
        /// Gets or sets the cancellation token.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        private CancellationToken loggingCancellationToken;

        /// <summary>
        /// Gets or sets the cancellation token source.
        /// </summary>
        /// <value>
        /// The cancellation token source.
        /// </value>
        private CancellationTokenSource executionCancellationTokenSource;

        /// <summary>
        /// Gets or sets the cancellation token.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        private CancellationToken executionCancellationToken;

        /// <summary>
        /// Gets or sets the default json folder.
        /// </summary>
        /// <value>
        /// The default json folder.
        /// </value>
        public string DefaultJsonFolder
        {
            get
            {
                return this.defaultJsonFolder;
            }

            set
            {
                this.defaultJsonFolder = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the migration shared steps retry json path.
        /// </summary>
        /// <value>
        /// The migration shared steps retry json path.
        /// </value>
        public string MigrationSharedStepsRetryJsonPath
        {
            get
            {
                return this.migrationSharedStepsRetryJsonPath;
            }

            set
            {
                this.migrationSharedStepsRetryJsonPath = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the migration suites retry json path.
        /// </summary>
        /// <value>
        /// The migration suites retry json path.
        /// </value>
        public string MigrationSuitesRetryJsonPath
        {
            get
            {
                return this.migrationSuitesRetryJsonPath;
            }

            set
            {
                this.migrationSuitesRetryJsonPath = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the migration test cases retry json path.
        /// </summary>
        /// <value>
        /// The migration test cases retry json path.
        /// </value>
        public string MigrationTestCasesRetryJsonPath
        {
            get
            {
                return this.migrationTestCasesRetryJsonPath;
            }

            set
            {
                this.migrationTestCasesRetryJsonPath = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the migration add test cases to suites retry json path.
        /// </summary>
        /// <value>
        /// The migration add test cases to suites retry json path.
        /// </value>
        public string MigrationAddTestCasesToSuitesRetryJsonPath
        {
            get
            {
                return this.migrationAddTestCasesToSuitesRetryJsonPath;
            }

            set
            {
                this.migrationAddTestCasesToSuitesRetryJsonPath = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the source full team project.
        /// </summary>
        /// <value>
        /// The name of the source full team project.
        /// </value>
        public string SourceFullTeamProjectName
        {
            get
            {
                return this.sourceFullTeamProjectName;
            }

            set
            {
                this.sourceFullTeamProjectName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the destination full team project.
        /// </summary>
        /// <value>
        /// The name of the destination full team project.
        /// </value>
        public string DestinationFullTeamProjectName
        {
            get
            {
                return this.destinationFullTeamProjectName;
            }

            set
            {
                this.destinationFullTeamProjectName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is shared steps migration finished.
        /// </summary>
        /// <value>
        /// The is shared steps migration finished.
        /// </value>
        public bool IsSharedStepsMigrationFinished
        {
            get
            {
                return this.isSharedStepsMigrationFinished;
            }

            set
            {
                this.isSharedStepsMigrationFinished = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is suites migration finished.
        /// </summary>
        /// <value>
        /// The is suites migration finished.
        /// </value>
        public bool IsSuitesMigrationFinished
        {
            get
            {
                return this.isSuitesMigrationFinished;
            }

            set
            {
                this.isSuitesMigrationFinished = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is test cases migration finished.
        /// </summary>
        /// <value>
        /// The is test cases migration finished.
        /// </value>
        public bool IsTestCasesMigrationFinished
        {
            get
            {
                return this.isTestCasesMigrationFinished;
            }

            set
            {
                this.isTestCasesMigrationFinished = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the progress queue.
        /// </summary>
        /// <value>
        /// The progress queue.
        /// </value>
        public ConcurrentQueue<string> ProgressConcurrentQueue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesMigrationViewModel"/> class.
        /// </summary>
        public TestCasesMigrationViewModel()
        {
            this.ObservableSourceTestPlans = new ObservableCollection<string>();
            this.ObservableDestinationTestPlans = new ObservableCollection<string>();
            this.ObservableSuitesToBeSkipped = new ObservableCollection<TextReplacePair>();
            this.ObservableSuitesToBeSkipped.Add(new TextReplacePair());
            this.suitesMapping = new Dictionary<int, int>();
            this.sharedStepsMapping = new Dictionary<int, int>();
            this.testCasesMapping = new Dictionary<int, int>();
            this.StatusLogQueue = new ConcurrentQueue<string>();
            this.ProgressConcurrentQueue = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// Gets or sets the test service.
        /// </summary>
        /// <value>
        /// The test service.
        /// </value>
        public ITestManagementService SourceTestService { get; set; }

        /// <summary>
        /// Gets or sets the destination test service.
        /// </summary>
        /// <value>
        /// The destination test service.
        /// </value>
        public ITestManagementService DestinationTestService { get; set; }

        /// <summary>
        /// Gets or sets the observable source test plans.
        /// </summary>
        /// <value>
        /// The observable source test plans.
        /// </value>
        public ObservableCollection<string> ObservableSourceTestPlans { get; set; }

        /// <summary>
        /// Gets or sets the observable destination test plans.
        /// </summary>
        /// <value>
        /// The observable destination test plans.
        /// </value>
        public ObservableCollection<string> ObservableDestinationTestPlans { get; set; }

        /// <summary>
        /// Gets or sets the observable suites to be skipped.
        /// </summary>
        /// <value>
        /// The observable suites to be skipped.
        /// </value>
        public ObservableCollection<TextReplacePair> ObservableSuitesToBeSkipped { get; set; }

        /// <summary>
        /// Gets or sets the selected source test plan.
        /// </summary>
        /// <value>
        /// The selected source test plan.
        /// </value>
        public string SelectedSourceTestPlan { get; set; }

        /// <summary>
        /// Gets or sets the selected destination test plan.
        /// </summary>
        /// <value>
        /// The selected destination test plan.
        /// </value>
        public string SelectedDestinationTestPlan { get; set; }

        /// <summary>
        /// Gets or sets the status log queue.
        /// </summary>
        /// <value>
        /// The status log queue.
        /// </value>
        public ConcurrentQueue<string> StatusLogQueue { get; set; }

        /// <summary>
        /// Loads the project settings from user decision source.
        /// </summary>
        /// <param name="projectPicker">The project picker.</param>
        public void LoadProjectSettingsFromUserDecisionSource(TeamProjectPicker projectPicker)
        {
            base.LoadProjectSettingsFromUserDecision(projectPicker, ref this.sourceTfsTeamProjectCollection, ref this.sourceTeamProject, ref this.sourcePreferences, this.SourceTestService, this.SelectedSourceTestPlan, false);
            if (this.sourcePreferences.TfsUri != null && this.sourcePreferences != null)
            {
                this.SourceFullTeamProjectName = base.GenerateFullTeamProjectName(this.sourcePreferences.TfsUri.ToString(), this.sourcePreferences.TestProjectName);
                this.sourcePreferences.TestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
                base.InitializeTestPlans(this.sourceTeamProject, this.ObservableSourceTestPlans);
                if (this.ObservableSourceTestPlans.Count > 0)
                {
                    this.SelectedSourceTestPlan = this.ObservableSourceTestPlans[0];
                }
            }
        }

        /// <summary>
        /// Loads the project settings from user decision destination.
        /// </summary>
        /// <param name="projectPicker">The project picker.</param>
        public void LoadProjectSettingsFromUserDecisionDestination(TeamProjectPicker projectPicker)
        {
            base.LoadProjectSettingsFromUserDecision(projectPicker, ref this.destinationTfsTeamProjectCollection, ref this.destinationTeamProject, ref this.destinationPreferences, this.DestinationTestService, this.SelectedDestinationTestPlan, false);
            if (this.destinationPreferences.TfsUri != null && this.destinationPreferences != null)
            {
                this.DestinationFullTeamProjectName = base.GenerateFullTeamProjectName(this.destinationPreferences.TfsUri.ToString(), this.destinationPreferences.TestProjectName);
                this.destinationPreferences.TestPlan = TestPlanManager.GetTestPlanByName(this.destinationTeamProject, this.SelectedDestinationTestPlan);
                base.InitializeTestPlans(this.destinationTeamProject, this.ObservableDestinationTestPlans);
                if (this.ObservableDestinationTestPlans.Count > 0)
                {
                    this.SelectedDestinationTestPlan = this.ObservableDestinationTestPlans[0];
                }
            }
        }

        /// <summary>
        /// Migrates the shared steps from source to destination.
        /// </summary>
        public void StartSharedStepsFromSourceToDestinationMigration(ProgressBar progressBar)
        {
            this.executionCancellationTokenSource = new CancellationTokenSource();
            this.executionCancellationToken = this.executionCancellationTokenSource.Token;

            Task t = Task.Factory.StartNew(() =>
            {
                this.ShowProgressBar(progressBar);
                this.MigrateSharedStepsFromSourceToDestinationInternal();
            }, this.executionCancellationToken);
            t.ContinueWith(antecedent =>
            {
                this.StopUiProgressLogging();
                this.HideProgressBar(progressBar);
                this.DisplayNotProcessedSharedSteps();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        /// <param name="progressBar">The progress bar.</param>
        private void ShowProgressBar(ProgressBar progressBar)
        {
            progressBar.Dispatcher.InvokeAsync((Action)(() =>
            {
                progressBar.Visibility = Visibility.Visible;
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        /// <param name="progressBar">The progress bar.</param>
        private void HideProgressBar(ProgressBar progressBar)
        {
            progressBar.Dispatcher.InvokeAsync((Action)(() =>
            {
                progressBar.Visibility = Visibility.Hidden;
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        /// <summary>
        /// Stops the shared steps from source to destination migration.
        /// </summary>
        public void StopMigrationExecution()
        {
            log.Info("Stop Shared Steps Migration!");
            if (this.executionCancellationTokenSource != null)
            {
                this.executionCancellationTokenSource.Cancel();
                log.Info("Shared Steps Migration STOPPED!");
            }
        }

        /// <summary>
        /// Starts the suites from source to destination migration.
        /// </summary>
        public void StartSuitesFromSourceToDestinationMigration(ProgressBar progressBar)
        {
            this.executionCancellationTokenSource = new CancellationTokenSource();
            this.executionCancellationToken = this.executionCancellationTokenSource.Token;

            Task t = Task.Factory.StartNew(() =>
            {
                this.ShowProgressBar(progressBar);
                this.MigrateSuitesFromSourceToDestinationInternal(this.sourcePreferences.TestPlan.RootSuite.SubSuites, -1);
            }, this.executionCancellationToken);
            t.ContinueWith(antecedent =>
            {
                this.StopUiProgressLogging();
                this.HideProgressBar(progressBar);
                this.DisplayNotProcessedSuites();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Starts the test cases from source to destination migration.
        /// </summary>
        /// <param name="progressBar">The progress bar.</param>
        public void StartTestCasesFromSourceToDestinationMigration(ProgressBar progressBar)
        {
            this.executionCancellationTokenSource = new CancellationTokenSource();
            this.executionCancellationToken = this.executionCancellationTokenSource.Token;

            Task t = Task.Factory.StartNew(() =>
            {
                this.ShowProgressBar(progressBar);
                this.MigrateTestCasesFromSourceToDestinationInternal();
            }, this.executionCancellationToken);
            t.ContinueWith(antecedent =>
            {
                this.StopUiProgressLogging();
                this.HideProgressBar(progressBar);
                this.DisplayNotProcessedTestCases();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Starts the test cases to suite from source to destination migration.
        /// </summary>
        /// <param name="progressBar">The progress bar.</param>
        public void StartTestCasesToSuiteFromSourceToDestinationMigration(ProgressBar progressBar)
        {
            this.executionCancellationTokenSource = new CancellationTokenSource();
            this.executionCancellationToken = this.executionCancellationTokenSource.Token;

            Task t = Task.Factory.StartNew(() =>
            {
                this.ShowProgressBar(progressBar);
                this.AddNewTestCasesToNewSuitesDestinationInternal();
            }, this.executionCancellationToken);
            t.ContinueWith(antecedent =>
            {
                this.StopUiProgressLogging();
                this.HideProgressBar(progressBar);
                this.DisplayNotProcessedTestCasesToSuites();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the selected source test plan.
        /// </summary>
        public void InitializeSelectedSourceTestPlan()
        {
            this.sourcePreferences.TestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
        }

        /// <summary>
        /// Initializes the selected destination test plan.
        /// </summary>
        public void InitializeSelectedDestinationTestPlan()
        {
            this.destinationPreferences.TestPlan = TestPlanManager.GetTestPlanByName(this.destinationTeamProject, this.SelectedDestinationTestPlan);
        }

        /// <summary>
        /// Migrates the shared steps from source to destination.
        /// </summary>
        private void MigrateSharedStepsFromSourceToDestinationInternal()
        {
            if (!string.IsNullOrEmpty(this.MigrationSharedStepsRetryJsonPath) && File.Exists(this.MigrationSharedStepsRetryJsonPath))
            {
                this.sharedStepsMigrationLogManager = new MigrationLogManager(this.MigrationSharedStepsRetryJsonPath);
                this.sharedStepsMigrationLogManager.LoadCollectionFromExistingFile();
                this.sharedStepsMapping = this.sharedStepsMigrationLogManager.GetProssedItemsMappings();
            }
            else
            {
                this.sharedStepsMigrationLogManager = new MigrationLogManager("sharedSteps", this.DefaultJsonFolder);
            }

            List<SharedStep> sourceSharedSteps = SharedStepManager.GetAllSharedStepsInTestPlan(this.sourceTeamProject);
            foreach (SharedStep currentSourceSharedStep in sourceSharedSteps)
            {
                if (this.executionCancellationToken.IsCancellationRequested)
                {
                    break;
                }

                // If it's already processed skip it
                if (this.sharedStepsMigrationLogManager.MigrationEntries.Count(e => e.SourceId.Equals(currentSourceSharedStep.ISharedStep.Id) && e.IsProcessed.Equals(true)) > 0)
                {
                    continue;
                }
                string infoMessage = String.Empty;
                try
                {
                    infoMessage = String.Format("Start Migrating Shared Step with Source Id= {0}", currentSourceSharedStep.Id);
                    log.Info(infoMessage);
                    this.ProgressConcurrentQueue.Enqueue(infoMessage);

                    List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(this.sourceTeamProject, currentSourceSharedStep.ISharedStep.Actions);
                    SharedStep newSharedStep = currentSourceSharedStep.Save(this.destinationTeamProject, true, testSteps, false);
                    newSharedStep.ISharedStep.Refresh();
                    this.sharedStepsMapping.Add(currentSourceSharedStep.ISharedStep.Id, newSharedStep.ISharedStep.Id);

                    this.sharedStepsMigrationLogManager.Log(currentSourceSharedStep.Id, newSharedStep.ISharedStep.Id, true);
                    infoMessage = String.Format("Shared Step Migrated SUCCESSFULLY: Source Id= {0}, Destination Id= {1}", currentSourceSharedStep.Id, newSharedStep.ISharedStep.Id);
                    log.Info(infoMessage);
                    this.ProgressConcurrentQueue.Enqueue(infoMessage);
                }
                catch (Exception ex)
                {
                    this.sharedStepsMigrationLogManager.Log(currentSourceSharedStep.Id, -1, false, ex.Message);
                    log.Error(ex);
                    this.ProgressConcurrentQueue.Enqueue(ex.Message);
                }
                finally
                {
                    this.sharedStepsMigrationLogManager.Save();
                    this.MigrationSharedStepsRetryJsonPath = this.sharedStepsMigrationLogManager.FullResultFilePath;
                }
            }
            this.IsSharedStepsMigrationFinished = true;
        }

        /// <summary>
        /// Displays the not processed shared steps.
        /// </summary>
        public void DisplayNotProcessedSharedSteps()
        {
            this.DisplayNotProssedEntities(this.sharedStepsMigrationLogManager);
        }

        /// <summary>
        /// Displays the not processed suites.
        /// </summary>
        public void DisplayNotProcessedSuites()
        {
            this.DisplayNotProssedEntities(this.suitesMigrationLogManager);
        }

        /// <summary>
        /// Displays the not processed test cases.
        /// </summary>
        public void DisplayNotProcessedTestCases()
        {
            this.DisplayNotProssedEntities(this.testCasesMigrationLogManager);
        }

        /// <summary>
        /// Displays the not processed test cases to suites.
        /// </summary>
        public void DisplayNotProcessedTestCasesToSuites()
        {
            this.DisplayNotProssedEntities(this.testCasesAddToSuitesMigrationLogManager);
        }

        /// <summary>
        /// Displays the not prossed entities.
        /// </summary>
        /// <param name="logManager">The log manager.</param>
        private void DisplayNotProssedEntities(MigrationLogManager logManager)
        {
            List<MigrationRetryEntry> notProssedEntries = logManager.GetNotProssedEntries();
            ModernDialog.ShowMessage(String.Format("Number of not processed: {0}", notProssedEntries.Count), "Warning", MessageBoxButton.OK);
        }

        /// <summary>
        /// Migrates the suites from source to destination internal.
        /// </summary>
        /// <param name="subSuitesCore">The sub suites core.</param>
        /// <param name="parentId">The parent id.</param>
        private void MigrateSuitesFromSourceToDestinationInternal(ITestSuiteCollection subSuitesCore, int parentId)
        {
            if (!string.IsNullOrEmpty(this.MigrationSuitesRetryJsonPath) && File.Exists(this.MigrationSuitesRetryJsonPath))
            {
                this.suitesMigrationLogManager = new MigrationLogManager(this.MigrationSuitesRetryJsonPath);
                this.suitesMigrationLogManager.LoadCollectionFromExistingFile();
                this.suitesMapping = this.suitesMigrationLogManager.GetProssedItemsMappings();
            }
            else
            {
                this.suitesMigrationLogManager = new MigrationLogManager("suites", this.DefaultJsonFolder);
            }

            if (subSuitesCore == null || subSuitesCore.Count == 0)
            {
                return;
            }
            foreach (ITestSuiteBase currentSuite in subSuitesCore)
            {
                if (this.executionCancellationToken.IsCancellationRequested)
                {
                    break;
                }
                // If it's already processed skip it
                if (this.suitesMigrationLogManager.MigrationEntries.Count(e => e.SourceId.Equals(currentSuite.Id) && e.IsProcessed.Equals(true)) > 0)
                {
                    continue;
                }

                string infoMessage = String.Empty;
                try
                {
                    infoMessage = String.Format("Start Migrating Suite with Source Id= {0}", currentSuite.Id);
                    log.Info(infoMessage);
                    this.ProgressConcurrentQueue.Enqueue(infoMessage);
                    int newSuiteId = 0;
                    if (currentSuite != null)
                    {
                        currentSuite.Refresh();
                        //Don't migrate the suite if its suite is in the exclusion list
                        if (this.ObservableSuitesToBeSkipped.Count(t => t != null && t.NewText != null && t.NewText.Equals(currentSuite.Title)) > 0)
                        {
                            continue;
                        }
                        bool canBeAddedNewSuite;
                        newSuiteId = TestSuiteManager.AddChildSuite(this.destinationTeamProject, this.destinationPreferences.TestPlan, parentId, currentSuite.Title, out canBeAddedNewSuite);
                        if (newSuiteId != 0)
                        {
                            this.suitesMapping.Add(currentSuite.Id, newSuiteId);
                        }

                        if (!(currentSuite is IRequirementTestSuite))
                        {
                            IStaticTestSuite suite = currentSuite as IStaticTestSuite;
                            this.MigrateSuitesFromSourceToDestinationInternal(suite.SubSuites, newSuiteId);
                        }
                    }

                    this.suitesMigrationLogManager.Log(currentSuite.Id, newSuiteId, true);
                    infoMessage = String.Format("Suite Migrated SUCCESSFULLY: Source Id= {0}, Destination Id= {1}", currentSuite.Id, newSuiteId);
                    log.Info(infoMessage);
                    this.ProgressConcurrentQueue.Enqueue(infoMessage);
                }
                catch (Exception ex)
                {
                    this.suitesMigrationLogManager.Log(currentSuite.Id, -1, false, ex.Message);
                    log.Error(ex);
                    this.ProgressConcurrentQueue.Enqueue(ex.Message);
                }
                finally
                {
                    this.suitesMigrationLogManager.Save();
                    this.MigrationSuitesRetryJsonPath = this.suitesMigrationLogManager.FullResultFilePath;
                }
            }
            this.IsSuitesMigrationFinished = true;
        }

        /// <summary>
        /// Migrates the test cases from source to destination.
        /// </summary>
        public void MigrateTestCasesFromSourceToDestinationInternal()
        {
            if (!string.IsNullOrEmpty(this.MigrationTestCasesRetryJsonPath) && File.Exists(this.MigrationTestCasesRetryJsonPath))
            {
                this.testCasesMigrationLogManager = new MigrationLogManager(this.MigrationTestCasesRetryJsonPath);
                this.testCasesMigrationLogManager.LoadCollectionFromExistingFile();
                this.testCasesMapping = this.testCasesMigrationLogManager.GetProssedItemsMappings();
            }
            else
            {
                this.testCasesMigrationLogManager = new MigrationLogManager("testCases", this.DefaultJsonFolder);
            }

            this.ProgressConcurrentQueue.Enqueue("Prepare source test cases...");
            ITestPlan sourceTestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
            List<TestCase> sourceTestCases = TestCaseManager.GetAllTestCasesFromSuiteCollection(this.sourcePreferences.TestPlan, this.sourcePreferences.TestPlan.RootSuite.SubSuites);
            TestCaseManager.AddTestCasesWithoutSuites(this.sourceTeamProject, this.sourcePreferences.TestPlan, sourceTestCases);
            foreach (TestCase currentSourceTestCase in sourceTestCases)
            { 
                if (this.executionCancellationToken.IsCancellationRequested)
                {
                    break;
                }

                // If it's already processed skip it
                if (this.testCasesMigrationLogManager.MigrationEntries.Count(e => e.SourceId.Equals(currentSourceTestCase.ITestCase.Id) && e.IsProcessed.Equals(true)) > 0)
                {
                    continue;
                }
                string infoMessage = String.Empty;
                try
                {
                    infoMessage = String.Format("Start Migrating Test Case with Source Id= {0}", currentSourceTestCase.Id);
                    log.Info(infoMessage);
                    this.ProgressConcurrentQueue.Enqueue(infoMessage);

                    //Don't migrate the test case if its suite is in the exclusion list
                    if (currentSourceTestCase.ITestSuiteBase != null && this.ObservableSuitesToBeSkipped.Count(t => t != null && t.NewText != null && t.NewText.Equals(currentSourceTestCase.ITestSuiteBase.Title)) > 0)
                    {
                        continue;
                    }
                    List<TestStep> currentSourceTestCaseTestSteps = TestStepManager.GetTestStepsFromTestActions(this.sourceTeamProject, currentSourceTestCase.ITestCase.Actions);
                    bool shouldCreateTestCase = true;
                    foreach (TestStep currentTestStep in currentSourceTestCaseTestSteps)
                    {
                        if (currentTestStep.IsShared)
                        {
                            //If the test step is shared we change the current shared step id with the newly created shared step in the destination team project
                            if (this.sharedStepsMapping.ContainsKey(currentTestStep.SharedStepId))
                            {
                                currentTestStep.SharedStepId = this.sharedStepsMapping[currentTestStep.SharedStepId];
                            }
                            else
                            {
                                // Don't save if the required shared steps are missing
                                shouldCreateTestCase = false;
                            }
                        }
                    }
                    if (shouldCreateTestCase)
                    {
                        TestCase newTestCase = currentSourceTestCase.Save(this.destinationTeamProject, this.destinationPreferences.TestPlan, true, null, currentSourceTestCaseTestSteps, false, isMigration: true);
                        this.testCasesMapping.Add(currentSourceTestCase.ITestCase.Id, newTestCase.ITestCase.Id);
                        this.testCasesMigrationLogManager.Log(currentSourceTestCase.ITestCase.Id, newTestCase.ITestCase.Id, true);
                        infoMessage = String.Format("Test Case Migrated SUCCESSFULLY: Source Id= {0}, Destination Id= {1}", currentSourceTestCase.ITestCase.Id, newTestCase.ITestCase.Id);
                        log.Info(infoMessage);
                        this.ProgressConcurrentQueue.Enqueue(infoMessage);
                    }
                }
                catch (Exception ex)
                {
                    if (currentSourceTestCase != null)
                    {
                        this.testCasesMigrationLogManager.Log(currentSourceTestCase.ITestCase.Id, -1, false, ex.Message);
                        log.Error(ex);
                        this.ProgressConcurrentQueue.Enqueue(ex.Message);
                    }
                }
                finally
                {
                    this.testCasesMigrationLogManager.Save();
                    this.MigrationTestCasesRetryJsonPath = this.testCasesMigrationLogManager.FullResultFilePath;
                }
            }
        }

        /// <summary>
        /// Adds the new test cases to new suites destination.
        /// </summary>
        public void AddNewTestCasesToNewSuitesDestinationInternal()
        {
            if (!string.IsNullOrEmpty(this.MigrationAddTestCasesToSuitesRetryJsonPath) && File.Exists(this.MigrationAddTestCasesToSuitesRetryJsonPath))
            {
                this.testCasesAddToSuitesMigrationLogManager = new MigrationLogManager(this.MigrationAddTestCasesToSuitesRetryJsonPath);
                this.testCasesAddToSuitesMigrationLogManager.LoadCollectionFromExistingFile();
                this.sharedStepsMapping = this.testCasesAddToSuitesMigrationLogManager.GetProssedItemsMappings();
            }
            else
            {
                this.testCasesAddToSuitesMigrationLogManager = new MigrationLogManager("testCasesToSuites", this.DefaultJsonFolder);
            }
            this.ProgressConcurrentQueue.Enqueue("Prepare destination test cases...");
            ITestPlan destinationTestPlan = TestPlanManager.GetTestPlanByName(this.destinationTeamProject, this.SelectedDestinationTestPlan);
            List<TestCase> destinationTestCases = TestCaseManager.GetAllTestCasesInTestPlan(this.destinationTeamProject, destinationTestPlan, false);
            this.ProgressConcurrentQueue.Enqueue("Prepare source test cases...");
            ITestPlan sourceTestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
            List<TestCase> sourceTestCases = TestCaseManager.GetAllTestCasesFromSuiteCollection(this.sourcePreferences.TestPlan, this.sourcePreferences.TestPlan.RootSuite.SubSuites);

            foreach (TestCase currentSourceTestCase in sourceTestCases)
            {
                if (this.executionCancellationToken.IsCancellationRequested)
                {
                    break;
                }

                // If it's already processed skip it
                if (this.testCasesAddToSuitesMigrationLogManager.MigrationEntries.Count(e => e.SourceId.Equals(currentSourceTestCase.ITestCase.Id) && e.IsProcessed.Equals(true)) > 0)
                {
                    continue;
                }
                if (currentSourceTestCase.ITestSuiteBase != null && this.ObservableSuitesToBeSkipped.Count(t => t != null && t.NewText != null && t.NewText.Equals(currentSourceTestCase.ITestSuiteBase.Title)) > 0)
                {
                    continue;
                }
                string infoMessage = String.Empty;
                try
                {
                    infoMessage = String.Format("Start Adding to Suite Test Case with Source Id= {0}", currentSourceTestCase.Id);
                    log.Info(infoMessage);
                    this.ProgressConcurrentQueue.Enqueue(infoMessage);

                    if (currentSourceTestCase.ITestSuiteBase == null)
                    {
                        continue;
                    }
                    else
                    {
                        int sourceParentSuiteId = currentSourceTestCase.ITestSuiteBase.Id;
                        if (!this.suitesMapping.ContainsKey(sourceParentSuiteId))
                        {
                            return;
                        }
                        else
                        {
                            int destinationSuiteId = this.suitesMapping[sourceParentSuiteId];
                            ITestSuiteBase destinationSuite = this.destinationTeamProject.TestSuites.Find(destinationSuiteId);
                            if (this.testCasesMapping.ContainsKey(currentSourceTestCase.ITestCase.Id))
                            {
                                TestCase currentDestinationTestCase = destinationTestCases.FirstOrDefault(t => t.Id.Equals(this.testCasesMapping[currentSourceTestCase.ITestCase.Id]));
                                destinationSuite.AddTestCase(currentDestinationTestCase.ITestCase);

                                this.testCasesAddToSuitesMigrationLogManager.Log(currentSourceTestCase.ITestCase.Id, destinationSuite.Id, true);
                                infoMessage = String.Format("Test Case SUCCESSFULLY added to Suite: Test Case Id= {0}, Suite Id= {1}", currentDestinationTestCase.ITestCase.Id, destinationSuite.Id);
                                log.Info(infoMessage);
                                this.ProgressConcurrentQueue.Enqueue(infoMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (currentSourceTestCase != null)
                    {
                        this.testCasesAddToSuitesMigrationLogManager.Log(currentSourceTestCase.ITestCase.Id, -1, false, ex.Message);
                        log.Error(ex);
                        this.ProgressConcurrentQueue.Enqueue(ex.Message);
                    }
                }
                finally
                {
                    this.testCasesAddToSuitesMigrationLogManager.Save();
                    this.MigrationAddTestCasesToSuitesRetryJsonPath = this.testCasesAddToSuitesMigrationLogManager.FullResultFilePath;
                }
            }
        }

        /// <summary>
        /// Starts the UI progress logging.
        /// </summary>
        /// <param name="progressLabel">The progress label.</param>
        public void StartUiProgressLogging(Label progressLabel)
        {
            log.Info("Start UI Progress logging!");
            progressLabel.IsEnabled = true;
            this.loggingCancellationTokenSource = new CancellationTokenSource();
            this.loggingCancellationToken = this.loggingCancellationTokenSource.Token;
            this.LogProgressInternal(this.ProgressConcurrentQueue, progressLabel);
        }

        /// <summary>
        /// Stops the UI progress logging.
        /// </summary>
        public void StopUiProgressLogging()
        {
            log.Info("Stop UI Progress logging!");		
            if (this.loggingCancellationTokenSource != null)
            {
                this.loggingCancellationTokenSource.Cancel();
                log.Info("UI Progress logging STOPPED!");
            }
        }

        /// <summary>
        /// Determines whether this instance [can start migration].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can start migration]; otherwise, <c>false</c>.
        /// </returns>
        public bool CanStartMigration()
        {
            if (this.destinationFullTeamProjectName == null || this.sourceFullTeamProjectName == null)
            {
                ModernDialog.ShowMessage("No Team Projects Specified!.", "Warning", MessageBoxButton.OK);
                return false;
            }
            if (string.IsNullOrEmpty(this.SelectedSourceTestPlan) || string.IsNullOrEmpty(this.SelectedDestinationTestPlan))
            {
                ModernDialog.ShowMessage("No Test Plans Specified!.", "Warning", MessageBoxButton.OK);
                return false;
            }
            if (string.IsNullOrEmpty(this.DefaultJsonFolder))
            {
                ModernDialog.ShowMessage("No Default JSON Folder set!.", "Warning", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logs the execution.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="progressLabel">The progress label.</param>
        private void LogProgressInternal(ConcurrentQueue<string> queue, Label progressLabel)
        {
            Task loggingTask = Task.Factory.StartNew((a) =>
            {
                do
                {
                    if (this.loggingCancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    string currentMessage = String.Empty;
                    bool isLoggingMessageDequeued = queue.TryDequeue(out currentMessage);

                    if (isLoggingMessageDequeued)
                    {
                        progressLabel.Dispatcher.InvokeAsync((Action)(() =>
                        {
                            try
                            {
                                progressLabel.Content = String.Format("\n{0}", currentMessage);
                            }
                            catch
                            {
                            }
                        }), System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                }
                while (true);
            }, this.loggingCancellationToken);
        }
    }
}