using Fidely.Framework;
using Fidely.Framework.Compilation.Evaluators;
using Fidely.Demo.Windows.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Fidely.Framework.Compilation.Object.Operators;
using Fidely.Framework.Compilation.Object.Evaluators;

namespace Fidely.Demo.Windows.ViewModels
{
    public class FeedReaderViewModel : BaseViewModel
    {
        public event EventHandler FocusRequired;

        public event EventHandler SelectAutocompleteItemRequired;

        public event EventHandler CancelAutocompleteRequired;


        public CompilerSetting CompilerSetting { get; private set; }


        private ICollectionView feedsCollectionView;

        private SearchQueryCompiler<EntryViewModel> compiler;


        public ICollection<FeedViewModel> Feeds { get; private set; }

        public ICollectionView FeedsCollectionView
        {
            get
            {
                return feedsCollectionView;
            }
        }

        public RelayCommand SearchCommand { get; private set; }

        public RelayCommand FocusCommand { get; private set; }

        public RelayCommand SelectAutocompleteItemCommand { get; private set; }

        public RelayCommand CancelAutocompleteCommand { get; private set; }

        public FeedReaderViewModel()
        {
            Feeds = new ObservableCollection<FeedViewModel>();
            SearchCommand = new RelayCommand(o => Search(o.ToString()));
            FocusCommand = new RelayCommand(o => OnFocusRequired(EventArgs.Empty));
            SelectAutocompleteItemCommand = new RelayCommand(o => OnSelectAutocompleteItemRequired(EventArgs.Empty));
            CancelAutocompleteCommand = new RelayCommand(o => OnCancelAutocompleteRequired(EventArgs.Empty));

            feedsCollectionView = CollectionViewSource.GetDefaultView(Feeds);
            feedsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            CompilerSetting = new CompilerSetting();
            CompilerSetting.Operators.Add(new PartialMatch<EntryViewModel>(":", true));
            CompilerSetting.Evaluators.Add(new PropertyEvaluator<EntryViewModel>());
            compiler = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<EntryViewModel>(CompilerSetting);
        }


        protected virtual void OnFocusRequired(EventArgs e)
        {
            if (FocusRequired != null)
            {
                FocusRequired(this, e);
            }
        }

        protected virtual void OnSelectAutocompleteItemRequired(EventArgs e)
        {
            if (SelectAutocompleteItemRequired != null)
            {
                SelectAutocompleteItemRequired(this, e);
            }
        }

        protected virtual void OnCancelAutocompleteRequired(EventArgs e)
        {
            if (CancelAutocompleteRequired != null)
            {
                CancelAutocompleteRequired(this, e);
            }
        }

        private void Search(string query)
        {
            var searchResult = Feeds.FirstOrDefault(o => o.Title == "Search Result");
            if (searchResult != null)
            {
                searchResult.Entries.Clear();
            }
            else
            {
                searchResult = new FeedViewModel(new DummyFeed { Title = "Search Result" });
                var feeds = (ObservableCollection<FeedViewModel>)Feeds;
                feeds.Insert(0, searchResult);
            }

            var filter = compiler.Compile(query).Compile();
            Feeds.SelectMany(o => o.Entries).Where(filter).ToList().ForEach(o => searchResult.Entries.Add(o));

            feedsCollectionView.MoveCurrentTo(searchResult);
        }
    }
}
