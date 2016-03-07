using System;

namespace PatternsInAutomatedTests.Advanced.Observer.Advanced.DotNetEvents
{
    public interface IExecutionProvider
    {
        event EventHandler<TestExecutionEventArgs> TestInstantiatedEvent;

        event EventHandler<TestExecutionEventArgs> PreTestInitEvent;

        event EventHandler<TestExecutionEventArgs> PostTestInitEvent;

        event EventHandler<TestExecutionEventArgs> PreTestCleanupEvent;

        event EventHandler<TestExecutionEventArgs> PostTestCleanupEvent;
    }
}
