using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fidely.Demo.DBViewer.Models;
using Fidely.Demo.DBViewer.ViewModels;
using Fidely.Framework.Integration.WPF;

namespace Fidely.Demo.DBViewer.Views
{
    public partial class DatabaseWindow : Window
    {
        public DatabaseWindow()
        {
            InitializeComponent();
        }

        private void Search(object sender, SearchExecutedEventArgs e)
        {
            var model = (DatabaseViewModel)DataContext;
            var table = CollectionViewSource.GetDefaultView(model.Tables).CurrentItem as TableViewModel;
            model.Search(table.Name, e.Query);
        }

        private void OnTableSelected(object sender, SelectionChangedEventArgs e)
        {
            var model = (DatabaseViewModel)DataContext;
            var table = e.AddedItems[0] as TableViewModel;
            model.Search(table.Name, "");
        }
    }
}
