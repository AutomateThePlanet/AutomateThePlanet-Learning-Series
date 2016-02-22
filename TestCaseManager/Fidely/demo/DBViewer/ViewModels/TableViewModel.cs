using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fidely.Demo.DBViewer.Models;

namespace Fidely.Demo.DBViewer.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        private string name;


        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
