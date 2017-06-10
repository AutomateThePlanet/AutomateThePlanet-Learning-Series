using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Behaviors.Core;
using AutomationTestDay.Behaviors.Pages;
using Microsoft.Practices.Unity;

namespace AutomationTestDay.Behaviors
{
    public class HomePageNavigateFindHowAssertBehavior : AssertBehavior
    {
        private readonly HomePage _homePage;
        private readonly string _categoryText;

        public HomePageNavigateFindHowAssertBehavior(string categoryText)
        {
            _homePage = UnityContainerFactory.GetContainer().Resolve<HomePage>();
            _categoryText = categoryText;
        }

        protected override void Assert()
        {
            _homePage.AssertFindHowText(_categoryText);
        }
    }
}
