using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Fidely.Demo.Windows.Model;

namespace Fidely.Demo.Windows.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        public ICollection<Log> Logs { get; private set; }


        public LogViewModel()
        {
            Logs = new ObservableCollection<Log>();

            InMemoryTraceListener.Flashed += (sender, e) =>
            {
                e.Messages.ToList().ForEach(o => Logs.Add(new Log(o)));
            };
        }
    }
}
