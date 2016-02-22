using PatternsInAutomation.Tests.Advanced.Base;

namespace PatternsInAutomation.Tests.Advanced.Core.Fluent
{
    public abstract class BaseFluentPageSingleton<S, M> : ThreadSafeNestedContructorsBaseSingleton<S>
        where M : BasePageElementMap, new()
        where S : BaseFluentPageSingleton<S, M>
    {
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

    public abstract class BaseFluentPageSingleton<S, M, V> : BaseFluentPageSingleton<S, M>
        where M : BasePageElementMap, new()
        where S : BaseFluentPageSingleton<S, M, V>
        where V : BasePageValidator<S, M, V>, new()
    {
        public V Validate()
        {
            return new V();
        }
    }
}