namespace PatternsInAutomatedTests.Advanced.Core.Fluent
{
    public class BasePageValidator<S, M, V>
        where S : BaseFluentPageSingleton<S, M, V>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<S, M, V>, new()
    {
        protected S pageInstance;

        public BasePageValidator(S currentInstance)
        {
            this.pageInstance = currentInstance;
        }

        public BasePageValidator()
        {
        }

        protected M Map
        {
            get
            {
                return new M();
            }
        }
    }
}