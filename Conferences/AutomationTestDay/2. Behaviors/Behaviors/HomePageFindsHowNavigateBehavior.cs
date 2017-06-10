using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Behaviors.Core;
using AutomationTestDay.Behaviors.Pages;
using Microsoft.Practices.Unity;

namespace AutomationTestDay.Behaviors
{
    public class HomePageFindsHowNavigateBehavior : ActionBehavior
    {
        private readonly HomePage _homePage;
        private readonly string _categoryText;

        public HomePageFindsHowNavigateBehavior(string categoryText)
        {
            _homePage = UnityContainerFactory.GetContainer().Resolve<HomePage>();
            _categoryText = categoryText;
        }
        protected override void PerformAct()
        {
            var findHowButton = _homePage.GetFindHowButtonByText(_categoryText);
            findHowButton.Click();
        }
    }
}
