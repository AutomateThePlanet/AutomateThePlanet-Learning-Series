using PatternsInAutomatedTests.Advanced.Base;

namespace PatternsInAutomatedTests.Advanced.Core
{
    public abstract class BasePageSingleton<S, M> : ThreadSafeLazyBaseSingleton<S>
        where M : BasePageElementMap, new()
        where S : BasePageSingleton<S, M>, new()
    {
        public BasePageSingleton()
        {
        }

        protected M Map
        {
            get
            {
                return new M();
            }
        }

        public virtual void Navigate(string url = "")
        {
            Driver.Browser.Navigate().GoToUrl(string.Concat(url));
        }
    }

    public abstract class BasePageSingleton<S, M, V> : BasePageSingleton<S, M>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<M>, new()
        where S : BasePageSingleton<S, M, V>, new()
    {
        public V Validate()
        {
            return new V();
        }
    }
}