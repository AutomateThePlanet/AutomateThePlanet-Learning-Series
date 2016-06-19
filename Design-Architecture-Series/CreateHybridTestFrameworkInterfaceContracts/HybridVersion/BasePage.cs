using HybridTestFramework.UITests.Core;

namespace CreateHybridTestFrameworkInterfaceContracts.HybridVersion
{
    public abstract class BasePage
    {
        private readonly IElementFinder elementFinder;
        private readonly INavigationService navigationService;

        public BasePage(IElementFinder elementFinder, INavigationService navigationService)
        {
            this.elementFinder = elementFinder;
            this.navigationService = navigationService;
        }

        protected IElementFinder ElementFinder
        {
            get
            {
                return this.elementFinder;
            }
        }

        protected INavigationService NavigationService
        {
            get
            {
                return this.navigationService;
            }
        }
    }
}