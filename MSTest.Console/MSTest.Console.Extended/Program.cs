using System;
using System.Configuration;
using log4net;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Services;

namespace MSTest.Console.Extended
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            string microsoftTestConsoleExePath = ConfigurationManager.AppSettings["MSTestConsoleRunnerPath"]; 
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            var engine = new TestExecutionService(
                new MsTestTestRunProvider(consoleArgumentsProvider, LogManager.GetLogger(typeof(MsTestTestRunProvider))),
                new FileSystemProvider(consoleArgumentsProvider),
                new ProcessExecutionProvider(microsoftTestConsoleExePath, consoleArgumentsProvider, LogManager.GetLogger(typeof(ProcessExecutionProvider))),
                consoleArgumentsProvider,
                LogManager.GetLogger(typeof(TestExecutionService)));
            try
            {
                int result = engine.ExecuteWithRetry();
                Environment.Exit(result);
            }
            catch (Exception ex)
            {
                log.Error(string.Concat(ex.Message, ex.StackTrace));
            }
        }
    }
}