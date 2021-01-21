using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Behaviors.Core;
using AutomationTestDay.Behaviors.Pages;
using Microsoft.Practices.Unity;

namespace AutomationTestDay.Behaviors
{
    public class CategoryPageCategoryBackgroundAssertBehavior : AssertBehavior
    {
        private readonly CategoryPage _categoryPage;
        private readonly string _categoryText;

        public CategoryPageCategoryBackgroundAssertBehavior(string categoryText)
        {
            _categoryPage = UnityContainerFactory.GetContainer().Resolve<CategoryPage>();
            _categoryText = categoryText;
        }

        protected override void Assert()
        {
            _categoryPage.AssertCategoryBackgroundWhenSelected(_categoryText);
        }
    }
}
