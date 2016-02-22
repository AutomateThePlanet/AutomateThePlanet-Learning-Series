using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fidely.Demo.Windows.Model
{
    public class FlashedEventArgs : EventArgs
    {
        public IEnumerable<string> Messages { get; private set; }


        public FlashedEventArgs(IList<string> messages)
        {
            Messages = new ReadOnlyCollection<string>(messages);
        }
    }
}
