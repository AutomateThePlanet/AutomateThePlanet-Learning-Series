using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Behaviors.Core;
using AutomationTestDay.Behaviors.Pages;
using Microsoft.Practices.Unity;

namespace AutomationTestDay.Behaviors
{
    public class ArticlePageDownloadSourceCodeBehavior : ActionBehavior
    {
        private readonly ArticlePage _articlePage;
        private readonly string _articleText;

        public ArticlePageDownloadSourceCodeBehavior()
        {
            _articlePage = UnityContainerFactory.GetContainer().Resolve<ArticlePage>();
        }

        protected override void PerformAct()
        {
            _articlePage.DownloadFullSourceCode.Click();
        }
    }
}
