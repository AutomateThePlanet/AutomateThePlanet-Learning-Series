using HybridTestFramework.UITests.Core;

namespace CreateHybridTestFrameworkInterfaceContracts.HybridVersion
{
    public abstract class BasePage
    {
        private readonly IElementFinder _elementFinder;
        private readonly INavigationService _navigationService;

        public BasePage(IElementFinder elementFinder, INavigationService navigationService)
        {
            this._elementFinder = elementFinder;
            this._navigationService = navigationService;
        }

        protected IElementFinder ElementFinder
        {
            get
            {
                return _elementFinder;
            }
        }

        protected INavigationService NavigationService
        {
            get
            {
                return _navigationService;
            }
        }
    }
}