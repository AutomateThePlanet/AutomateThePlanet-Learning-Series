// <copyright file="ReportingView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
	/// <summary>
	/// Contains logic related to the test case detailed(read mode) page
	/// </summary>
	public partial class ReportingView : UserControl, IContent
	{
		/// <summary>
		/// The log
		/// </summary>
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Indicates if the view model is already initialized
		/// </summary>
		private static bool isInitialized;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportingView"/> class.
		/// </summary>
		public ReportingView()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the reporting view model.
		/// </summary>
		/// <value>
		/// The reporting view model.
		/// </value>
		public ReportingViewModel ReportingViewModel { get; set; }

		/// <summary>
		/// Called when navigation to a content fragment begins.
		/// </summary>
		/// <param name="e">An object that contains the navigation data.</param>
		public void OnFragmentNavigation(FragmentNavigationEventArgs e)
		{
			isInitialized = false;
			FragmentManager fm = new FragmentManager(e.Fragment);
		}

		/// <summary>
		/// Called when this instance is no longer the active content in a frame.
		/// </summary>
		/// <param name="e">An object that contains the navigation data.</param>
		public void OnNavigatedFrom(NavigationEventArgs e)
		{
		}

		/// <summary>
		/// Called when a this instance becomes the active content in a frame.
		/// </summary>
		/// <param name="e">An object that contains the navigation data.</param>
		public void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		/// <summary>
		/// Called just before this instance is no longer the active content in a frame.
		/// </summary>
		/// <param name="e">An object that contains the navigation data.</param>
		/// <remarks>
		/// The method is also invoked when parent frames are about to navigate.
		/// </remarks>
		public void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
		}

		/// <summary>
		/// Handles the Loaded event of the UserControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			if (isInitialized)
			{
				return;
			}
			this.ShowProgressBar();
			Task t = Task.Factory.StartNew(() =>
			{
				////log.InfoFormat("Preview test case with id: {0} and suite id {1}", this.TestCaseId, this.TestSuiteId);
				this.ReportingViewModel = new ReportingViewModel();
			});
			t.ContinueWith(antecedent =>
			{
				this.DataContext = this.ReportingViewModel;
				this.HideProgressBar();
				isInitialized = true;
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		/// <summary>
		/// Hides the progress bar.
		/// </summary>
		private void HideProgressBar()
		{
			progressBar.Visibility = System.Windows.Visibility.Hidden;
			mainGrid.Visibility = System.Windows.Visibility.Visible;
		}

		/// <summary>
		/// Shows the progress bar.
		/// </summary>
		private void ShowProgressBar()
		{
			progressBar.Visibility = System.Windows.Visibility.Visible;
			mainGrid.Visibility = System.Windows.Visibility.Hidden;
		}
	}
}