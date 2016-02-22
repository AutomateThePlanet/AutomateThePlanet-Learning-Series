using System.Diagnostics;

namespace MSTest.Console.Extended.Interfaces
{
    public interface IProcessExecutionProvider
    {
        Process CurrentProcess { get; set; }

        string MicrosoftTestConsoleExePath { get; set; }

        void ExecuteProcessWithAdditionalArguments(string arguments = "");  

        void CurrentProcessWaitForExit();
    }
}