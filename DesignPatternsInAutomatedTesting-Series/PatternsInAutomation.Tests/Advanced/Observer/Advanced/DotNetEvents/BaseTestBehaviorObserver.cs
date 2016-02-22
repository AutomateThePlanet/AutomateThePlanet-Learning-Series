
namespace PatternsInAutomation.Tests.Advanced.Observer.Advanced.DotNetEvents
{
    public class BaseTestBehaviorObserver
    {
        public void Subscribe(IExecutionProvider provider)
        {
            provider.TestInstantiatedEvent += this.TestInstantiated;
            provider.PreTestInitEvent += this.PreTestInit;
            provider.PostTestInitEvent += this.PostTestInit;
            provider.PreTestCleanupEvent += this.PreTestCleanup;
            provider.PostTestCleanupEvent += this.PostTestCleanup;
        }

        public void Unsubscribe(IExecutionProvider provider)
        {
            provider.TestInstantiatedEvent -= this.TestInstantiated;
            provider.PreTestInitEvent -= this.PreTestInit;
            provider.PostTestInitEvent -= this.PostTestInit;
            provider.PreTestCleanupEvent -= this.PreTestCleanup;
            provider.PostTestCleanupEvent -= this.PostTestCleanup;
        }

        protected virtual void TestInstantiated(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PreTestInit(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PostTestInit(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PreTestCleanup(object sender, TestExecutionEventArgs e)
        {
        }

        protected virtual void PostTestCleanup(object sender, TestExecutionEventArgs e)
        {
        }
    }
}
