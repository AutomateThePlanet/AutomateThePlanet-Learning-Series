using HybridTestFramework.UITests.Core;
using System.Collections.Generic;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IElementFinder
    {
        public TElement Find<TElement>(Core.By by) where TElement : class, Core.Controls.IElement
        {
            return this.elementFinderService.Find<TElement>(this.driver, by);
        }

        public IEnumerable<TElement> FindAll<TElement>(Core.By by) where TElement : class, Core.Controls.IElement
        {
            return this.elementFinderService.FindAll<TElement>(this.driver, by);
        }

        public bool IsElementPresent(Core.By by)
        {
            return this.elementFinderService.IsElementPresent(this.driver, by);
        }
    }
}