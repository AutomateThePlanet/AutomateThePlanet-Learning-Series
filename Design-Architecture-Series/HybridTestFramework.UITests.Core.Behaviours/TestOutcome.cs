namespace HybridTestFramework.UITests.Core.Behaviours
{
    public enum TestOutcome : int 
    {
        /// <summary>The test failed.</summary>
        Failed = 0,
        /// <summary>An Assert.<see cref="M:Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Inconclusive"/>
        /// was raised. </summary>
        Inconclusive = 1,
        /// <summary>The test passed.</summary>
        Passed = 2,
        /// <summary>The test is currently running.</summary>
        InProgress = 3,
        /// <summary/>
        Error = 4,
        /// <summary/>
        Timeout = 5,
        /// <summary/>
        Aborted = 6,
        /// <summary>The outcome of the test is unknown. </summary>
        Unknown = 7,
    }
}