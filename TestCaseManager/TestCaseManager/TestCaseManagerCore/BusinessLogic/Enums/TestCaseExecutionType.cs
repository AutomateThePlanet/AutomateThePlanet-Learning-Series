using System;
using System.Linq;

namespace TestCaseManagerCore.BusinessLogic.Enums
{
    public enum TestCaseExecutionType
    {
        All = -2,
        Active = -1,
        Unspecified = 0,
        None = 1,
        Passed = 2,
        Failed = 3,
        Inconclusive = 4,
        Timeout = 5,
        Aborted = 6,
        Blocked = 7,
        NotExecuted = 8,
        Warning = 9,
        Error = 10,
        NotApplicable = 11,
        MaxValue = 12,
        Paused = 12,
    }
}