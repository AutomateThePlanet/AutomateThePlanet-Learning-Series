using CSharp.Series.NLog.Tests.Loggers;
using Microsoft.Practices.Unity;
using NLog;
using Loggers= CSharp.Series.NLog.Tests.Loggers;

namespace CSharp.Series.NLog.Tests
{
    public class Program
    {
        ////private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            // Register a type to have a singleton lifetime without mapping the type
            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<Loggers.ILogger, EventLogger>(new ContainerControlledLifetimeManager());
            Loggers.ILogger eventLogger = unityContainer.Resolve<Loggers.ILogger>(); 
            eventLogger.LogInfo("EventLogger log message to Kaspersky event log.");
        }
    }
}