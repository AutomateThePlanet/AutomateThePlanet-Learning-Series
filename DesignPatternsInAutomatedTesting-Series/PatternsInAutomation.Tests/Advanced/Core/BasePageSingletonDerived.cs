using PatternsInAutomatedTests.Advanced.Base;

namespace PatternsInAutomatedTests.Advanced.Core
{
    public abstract class BasePageSingletonDerived<S, M> : ThreadSafeNestedContructorsBaseSingleton<S>
        where M : BasePageElementMap, new()
        where S : BasePageSingletonDerived<S, M>
    {
        public BasePageSingletonDerived()
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

    public abstract class BasePageSingletonDerived<S, M, V> : BasePageSingletonDerived<S, M>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<M>, new()
        where S : BasePageSingletonDerived<S, M, V>
    {
        public V Validate()
        {
            return new V();
        }
    }
}