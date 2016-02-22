using System;

namespace Fidely.Demo.DBViewer.ViewModels.Events
{
    public class ConnectedEventArgs : EventArgs
    {
        public string ConnectionString { get; private set; }


        public ConnectedEventArgs(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
