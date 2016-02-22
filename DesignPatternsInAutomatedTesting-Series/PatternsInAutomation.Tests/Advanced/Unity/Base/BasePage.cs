using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Unity.Base
{
    public class BasePage<M>
        where M : BasePageElementMap, new()
    {
        protected readonly string url;

        public BasePage(string url)
        {
            this.url = url;
        }

        public BasePage()
        {
            this.url = null;
        }

        protected M Map
        {
            get
            {
                return new M();
            }
        }

        public virtual void Navigate(string part = "")
        {
            Driver.Browser.Navigate().GoToUrl(string.Concat(url, part));
        }
    }

    public class BasePage<M, V> : BasePage<M>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<M>, new()
    {
        public BasePage(string url) : base(url)
        {
        }

        public BasePage()
        {
        }

        public V Validate()
        {
            return new V();
        }
    }
}