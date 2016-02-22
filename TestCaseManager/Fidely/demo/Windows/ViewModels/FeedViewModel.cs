using QDFeedParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fidely.Demo.Windows.Model;

namespace Fidely.Demo.Windows.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        private IFeed model;

        private Uri feedUri;

        private string group;


        public string Title
        {
            get
            {
                return model.Title;
            }
        }

        public Uri FeedUri
        {
            get
            {
                return new Uri(model.FeedUri);
            }
        }

        public Uri SiteUri
        {
            get
            {
                return new Uri(model.Link);
            }
        }

        public DateTime LastUpdated
        {
            get
            {
                return model.LastUpdated;
            }
        }

        public string Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }

        public ICollection<EntryViewModel> Entries { get; private set; }


        public FeedViewModel(IFeed model)
        {
            this.model = model;
            Entries = new ObservableCollection<EntryViewModel>();
            Group = "Special";
        }

        public FeedViewModel(Uri feedUri)
        {
            this.feedUri = feedUri;
            Entries = new ObservableCollection<EntryViewModel>();
            Group = "Feed";
        }


        public void Sync()
        {
            var factory = new HttpFeedFactory();
            model = factory.CreateFeed(feedUri);
            OnPropertyChanged("Title");
            OnPropertyChanged("FeedUri");
            OnPropertyChanged("SiteUri");
            OnPropertyChanged("LastUPdated");

            Entries.Clear();
            model.Items.ToList().ForEach(o => Entries.Add(new EntryViewModel(o)));
        }
    }
}
