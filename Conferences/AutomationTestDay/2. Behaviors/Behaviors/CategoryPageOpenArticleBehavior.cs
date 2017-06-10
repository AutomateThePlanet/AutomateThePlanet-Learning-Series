using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Behaviors.Core;
using AutomationTestDay.Behaviors.Pages;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;

namespace AutomationTestDay.Behaviors
{
    public class CategoryPageOpenArticleBehavior : ActionBehavior
    {
        private readonly CategoryPage _categoryPage;
        private readonly string _articleText;

        public CategoryPageOpenArticleBehavior(string articleText)
        {
            _categoryPage = UnityContainerFactory.GetContainer().Resolve<CategoryPage>();
            _articleText = articleText;
        }

        protected override void PerformAct()
        {
            var articleAnchor = default(IWebElement);
            do
            {
                try
                {
                    articleAnchor = _categoryPage.GetArticleAnchorByName(_articleText);
                }
                catch(NoSuchElementException)
                {
                    _categoryPage.NavigateToNextPage();
                }
            }
            while(articleAnchor == null);
            articleAnchor.Click();
        }
    }
}
