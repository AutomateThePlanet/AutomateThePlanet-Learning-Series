using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace HuddlePageObjectsPageSections.Pages.Sections
{
    public partial class MainNavigationSection
    {
        private readonly IWebDriver _driver;

        public MainNavigationSection(IWebDriver browser)
        {
            _driver = browser;
        }
    }
}
