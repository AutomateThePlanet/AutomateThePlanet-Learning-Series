using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fidely.Demo.Windows.ViewModels;
using System.Windows.Input;
using Fidely.Framework;
using System.ComponentModel;
using System.Windows.Data;

namespace Fidely.Demo.Windows.Views
{
    public partial class FeadReaderWindow : Window
    {
        public FeadReaderWindow()
        {
            InitializeComponent();
        }


        private void OnEntryDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            contentWindow.Show();
        }
    }
}
