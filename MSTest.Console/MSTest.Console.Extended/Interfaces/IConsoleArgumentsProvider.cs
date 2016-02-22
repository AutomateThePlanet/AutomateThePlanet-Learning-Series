namespace MSTest.Console.Extended.Interfaces
{
    public interface IConsoleArgumentsProvider
    {
        string ConsoleArguments { get; set; }
        
        string TestResultPath { get; set; }

        string NewTestResultPath { get; set; }

        int RetriesCount { get; set; }

        bool ShouldDeleteOldTestResultFiles { get; set; }
    
        int FailedTestsThreshold { get; set; }
    }
}