using System;
using System.Collections.Generic;
using System.Linq;
using QDFeedParser;

namespace Fidely.Demo.Windows.Model
{
    public class DummyFeed : IFeed
    {
        public string Title { get; set; }

        public FeedType FeedType
        {
            get { throw new NotImplementedException(); }
        }

        public string FeedUri
        {
            get { throw new NotImplementedException(); }
        }

        public string Generator
        {
            get { throw new NotImplementedException(); }
        }

        public List<BaseFeedItem> Items
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime LastUpdated
        {
            get { throw new NotImplementedException(); }
        }

        public string Link
        {
            get { throw new NotImplementedException(); }
        }

    }
}
