using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Fidely.Demo.DBViewer.ViewModels;
using Fidely.Demo.DBViewer.Views;

namespace Fidely.Demo.DBViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var model = new DatabaseViewModel();
            new DatabaseWindow { DataContext = model }.Show();
        }
    }
}
