using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Behaviors.Core;
using AutomationTestDay.Behaviors.Pages;
using Microsoft.Practices.Unity;

namespace AutomationTestDay.Behaviors
{
    public class HomePageNavigateBehavior : ActionBehavior
    {
        private readonly HomePage _homePage;

        public HomePageNavigateBehavior()
        {
            _homePage = UnityContainerFactory.GetContainer().Resolve<HomePage>();
        }
        protected override void PerformAct()
        {
            _homePage.Open();
        }
    }
}
