using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Fidely.Demo.Windows.ViewModels;
using Fidely.Demo.Windows.Views;
using QDFeedParser;

namespace Fidely.Demo.Windows
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO: enable/disable proxy
            //var proxy =  new System.Net.WebProxy("http://itfproxy.itfrontier.co.jp:8080");
            //proxy.Credentials = new System.Net.NetworkCredential("", "");
            //System.Net.WebRequest.DefaultWebProxy = proxy;

            var model = new FeedReaderViewModel();
            //model.Feeds.Add(new FeedViewModel(new Uri("http://www.codeplex.com/site/feeds/rss")));
            //model.Feeds.Add(new FeedViewModel(new Uri("http://blogs.msdn.com/b/mainfeed.aspx?Type=BlogsOnly")));
            model.Feeds.Add(new FeedViewModel(new Uri("http://social.msdn.microsoft.com/Forums/en/vstscode/threads?outputAs=atom")));
            model.Feeds.ToList().ForEach(o => o.Sync());

            var view = new FeadReaderWindow();
            view.DataContext = model;

            view.Show();

            new LogWindow { DataContext = new LogViewModel() }.Show();
        }
    }
}
