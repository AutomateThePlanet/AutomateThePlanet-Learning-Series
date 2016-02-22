using System.Diagnostics;
using log4net;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Infrastructure
{
    public class ProcessExecutionProvider : IProcessExecutionProvider
    {
        private readonly log4net.ILog log;
        private readonly IConsoleArgumentsProvider consoleArgumentsProvider;

        public ProcessExecutionProvider(string microsoftTestConsoleExePath, IConsoleArgumentsProvider consoleArgumentsProvider, ILog log)
        {
            this.MicrosoftTestConsoleExePath = microsoftTestConsoleExePath;
            this.consoleArgumentsProvider = consoleArgumentsProvider;
            this.log = log;
        }

        public string MicrosoftTestConsoleExePath { get; set; }

        public Process CurrentProcess { get; set; }

        public void ExecuteProcessWithAdditionalArguments(string arguments = "")
        {
            if (string.IsNullOrEmpty(arguments))
            {
                arguments = this.consoleArgumentsProvider.ConsoleArguments;
            }
            this.CurrentProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo(this.MicrosoftTestConsoleExePath, arguments);
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            this.CurrentProcess.StartInfo = processStartInfo;
            this.CurrentProcess.OutputDataReceived += (sender, args) =>
            {
                System.Console.WriteLine(args.Data);
                this.log.Info(args.Data);
            };
            this.CurrentProcess.Start();
            if (this.CurrentProcess != null)
            {
                this.CurrentProcess.BeginErrorReadLine();
            }
            if (this.CurrentProcess != null)
            {
                this.CurrentProcess.BeginOutputReadLine();
            }
        }

        public void CurrentProcessWaitForExit()
        {
            this.CurrentProcess.WaitForExit();
        }
    }
}