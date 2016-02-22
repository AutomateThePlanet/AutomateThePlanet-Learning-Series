using log4net;
using System.Reflection;

namespace CSharp.Series.Log4Net.Tests
{
    class Program
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log.Info("log4net log message to Kaspersky event log.");
        }
    }
}
