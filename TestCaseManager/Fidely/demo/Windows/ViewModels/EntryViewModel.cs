using QDFeedParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Fidely.Framework;

namespace Fidely.Demo.Windows.ViewModels
{
    public class EntryViewModel : BaseViewModel
    {
        private BaseFeedItem model;


        [NotEvaluate]
        public string Id
        {
            get
            {
                return model.Id;
            }
        }

        [Description("Feed Title")]
        public string Title
        {
            get
            {
                return model.Title;
            }
        }

        [Description("Feed Author")]
        public string Author
        {
            get
            {
                return model.Author;
            }
        }

        [Description("Published Date")]
        public DateTime DatePublished
        {
            get
            {
                return model.DatePublished;
            }
        }

        [NotEvaluate]
        public string Content
        {
            get
            {
                return model.Content;
            }
        }

        [NotEvaluate]
        public Uri EntryUri
        {
            get
            {
                return new Uri(model.Link);
            }
        }


        public EntryViewModel(BaseFeedItem model)
        {
            this.model = model;
        }
    }
}
