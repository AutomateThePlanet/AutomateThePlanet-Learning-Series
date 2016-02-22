using AAngelov.Utilities.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.BusinessLogic.Enums;

namespace TestCaseManagerCore.BusinessLogic.Managers
{
    /// <summary>
    /// Filters Test Case Collections
    /// </summary>
    public class TestCaseFilterManager
    {
           /// <summary>
        /// The is title text set
        /// </summary>
        public bool IsTitleTextSet;

        /// <summary>
        /// The is suite text set
        /// </summary>
        public bool IsSuiteTextSet;

        /// <summary>
        /// The is unique identifier text set
        /// </summary>
        public bool IsIdTextSet;

        /// <summary>
        /// The is priority text set
        /// </summary>
        public bool IsPriorityTextSet;

        /// <summary>
        /// The is assigned automatic text set
        /// </summary>
        public bool IsAssignedToTextSet;

        /// <summary>
        /// The should filter by automation status
        /// </summary>
        public bool ShouldFilterByAutomationStatus;

        /// <summary>
        /// The should filter by execution status
        /// </summary>
        public bool ShouldFilterByExecutionStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="suite">The suite.</param>
        /// <param name="id">The unique identifier.</param>
        public TestCaseFilterManager(string title, string suite, string id)
        {
            this.TitleFilter = title;
            this.SuiteFilter = suite;
            this.IdFilter = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="suite">The suite.</param>
        /// <param name="id">The unique identifier.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="assignedTo">The assigned automatic.</param>
        public TestCaseFilterManager(string title, string suite, string id, string assignedTo, string priority, TestCaseExecutionType testCaseExecutionType)
            : this(title, suite, id)
        {
            this.PriorityFilter = priority;
            this.AssignedToFilter = assignedTo;
            this.TestCaseExecutionType = testCaseExecutionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        public TestCaseFilterManager()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets or sets the type of the test case execution.
        /// </summary>
        /// <value>
        /// The type of the test case execution.
        /// </value>
        public TestCaseExecutionType TestCaseExecutionType { get; set; }

        /// <summary>
        /// Gets or sets the title filter.
        /// </summary>
        /// <value>
        /// The title filter.
        /// </value>
        public string TitleFilter { get; set; }

        /// <summary>
        /// Gets or sets the suite filter.
        /// </summary>
        /// <value>
        /// The suite filter.
        /// </value>
        public string SuiteFilter { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier filter.
        /// </summary>
        /// <value>
        /// The unique identifier filter.
        /// </value>
        public string IdFilter { get; set; }

        /// <summary>
        /// Gets or sets the priority filter.
        /// </summary>
        /// <value>
        /// The priority filter.
        /// </value>
        public string PriorityFilter { get; set; }

        /// <summary>
        /// Gets or sets the assigned automatic filter.
        /// </summary>
        /// <value>
        /// The assigned automatic filter.
        /// </value>
        public string AssignedToFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide automated].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hide automated]; otherwise, <c>false</c>.
        /// </value>
        public bool HideAutomated { get; set; }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        /// <param name="initialCollection">The initial collection.</param>
        /// <returns>filtered collection</returns>
        public List<TestCase> FilterTestCases(List<TestCase> initialCollection)
        {
            List<TestCase> resultCollection = new List<TestCase>();
           
            bool shouldSetIdFilter = this.IsIdTextSet && !string.IsNullOrEmpty(this.IdFilter);
            string idFilter = this.IdFilter;
            bool shouldSetTextFilter = this.IsTitleTextSet && !string.IsNullOrEmpty(this.TitleFilter);
            string titleFilter = this.TitleFilter.ToLower();
            bool shouldSetSuiteFilter = this.IsSuiteTextSet && !string.IsNullOrEmpty(this.SuiteFilter);
            string suiteFilter = this.SuiteFilter.ToLower();
            bool shouldSetPriorityFilter = this.IsPriorityTextSet && !string.IsNullOrEmpty(this.PriorityFilter);
            string priorityFilter = this.PriorityFilter.ToLower();
            bool shouldSetAssignedToFilter = this.IsAssignedToTextSet && !string.IsNullOrEmpty(this.AssignedToFilter);
            string assignedToFilter = this.AssignedToFilter.ToLower();

            List<TestCase> resultCollection1 = new List<TestCase>();
            List<TestCase> resultCollection2 = new List<TestCase>();
            List<TestCase> resultCollection3 = new List<TestCase>();
            List<TestCase> resultCollection4 = new List<TestCase>();
            List<TestCase> resultCollection5 = new List<TestCase>();
            List<TestCase> resultCollection6 = new List<TestCase>();
            List<TestCase> resultCollection7 = new List<TestCase>();
            ConditionalSearchEngine searchEngine = new ConditionalSearchEngine();
            bool shouldBeAdded = false;
            if (shouldSetIdFilter)
            {                
                if (searchEngine.IsExpressionConditionSearchable(IdFilter))
                {
                    foreach (var item in initialCollection)
                    {
                        shouldBeAdded = searchEngine.ShouldBeAdded(IdFilter, item.Id.ToString());
                        if (shouldBeAdded)
                        {
                            resultCollection1.Add(item);
                        }
                        shouldBeAdded = false;
                    }
                }
            }
            if (shouldSetTextFilter)
            {
                if (searchEngine.IsExpressionConditionSearchable(this.TitleFilter))
                {
                    foreach (var item in initialCollection)
                    {                       
                        shouldBeAdded = searchEngine.ShouldBeAdded(this.TitleFilter, item.Title);
                        if (shouldBeAdded)
                        {
                            resultCollection2.Add(item);
                        }
                        shouldBeAdded = false;
                    }
                }
            }
            //TODO: Optimize the methods add it to begining
            if (shouldSetIdFilter || shouldSetTextFilter)
            {
                resultCollection = resultCollection1.Intersect(resultCollection2).ToList();
            }
            else
            {
                return initialCollection;
            }
                
                
            //var filteredList = initialCollection.Where(t =>
            //                                                            (t.ITestCase != null) &&
            //                                                            (shouldSetIdFilter ? (t.ITestCase.Id.ToString().Contains(idFilter)) : true) &&
            //                                                            (shouldSetTextFilter ? (t.ITestCase.Title.ToLower().Contains(titleFilter)) : true) &&
            //                                                            (this.FilterTestCasesBySuite(shouldSetSuiteFilter, suiteFilter, t)) &&
            //                                                            (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
            //                                                            (t.TeamFoundationIdentityName != null && shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true) &&
            //                                                            (!this.HideAutomated.Equals(t.ITestCase.IsAutomated) || !this.HideAutomated) &&
            //                                                            (this.CurrentExecutionStatusOption.Equals(TestCaseExecutionType.All) || t.LastExecutionOutcome.Equals(this.CurrentExecutionStatusOption))).ToList();
            //this.ObservableTestCases.Clear();
            //filteredList.ForEach(x => this.ObservableTestCases.Add(x));
            //this.TestCasesCount = filteredList.Count.ToString();

            return resultCollection;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            this.IdFilter = string.Empty;
            this.TitleFilter = string.Empty;
            this.SuiteFilter = string.Empty;
            this.PriorityFilter = string.Empty;
            this.AssignedToFilter = string.Empty;
            this.IsIdTextSet = false;
            this.IsSuiteTextSet = false;
            this.IsTitleTextSet = false;
            this.IsPriorityTextSet = false;
            this.IsAssignedToTextSet = false;
        }
    }
}
