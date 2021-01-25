using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TestProjectFacadeDesignPattern.Pages;

namespace TestProjectFacadeDesignPattern
{
    public class ImageSearchFacade
    {
        private readonly MainPage _mainPage;
        private readonly ResultDetailedPage _resultDetailedPage;
        private readonly IWebDriver _driver;

        public ImageSearchFacade(IWebDriver driver, MainPage mainPage, ResultDetailedPage resultDetailedPage)
        {
            _driver = driver;
            _mainPage = mainPage;
            _resultDetailedPage = resultDetailedPage;
        }

        public void SearchImage(SearchData searchData, string expectedTitle, string expectedUrl)
        {
            _mainPage
                  .Open<MainPage>()
                  .Search(searchData.SearchTerm)
                  .ClickImages()
                  .SetSize(searchData.Size)
                  .SetColor(searchData.Color)
                  .SetTypes(searchData.Type)
                  .SetPeople(searchData.People)
                  .SetDate(searchData.Date)
                  .SetLicense(searchData.License)
                  .ClickImageResult(searchData.ResultNumber);
            _resultDetailedPage.AssertResultTitle(expectedTitle)
                .AssertResultLink(expectedUrl)
                .ClickVisitSiteButton();

            Assert.AreEqual(expectedUrl, _driver.Url);
        }
    }
}
