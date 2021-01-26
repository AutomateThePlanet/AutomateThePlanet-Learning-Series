using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TestProjectFacadeDesignPattern.Enums;
using TestProjectFacadeDesignPattern.Pages;

namespace TestProjectFacadeDesignPattern
{
    public class TemplateMethodSearchFacade : TemplateMethodFacade
    {
        private readonly MainPage _mainPage;
        private readonly ResultDetailedPage _resultDetailedPage;
        private readonly IWebDriver _driver;

        public TemplateMethodSearchFacade(IWebDriver driver, MainPage mainPage, ResultDetailedPage resultDetailedPage)
        {
            _driver = driver;
            _mainPage = mainPage;
            _resultDetailedPage = resultDetailedPage;
        }

        protected override void OpenSearchPage()
        {
            _mainPage.Open<MainPage>();
        }

        protected override void Search(string searchTerm)
        {
            _mainPage.Search(searchTerm).ClickImages();
        }

        protected override void SetSize(Sizes size)
        {
            _mainPage.SetSize(size);
        }

        protected override void SetColor(Colors color)
        {
            _mainPage.SetColor(color);
        }

        protected override void SetTypes(Types type)
        {
            _mainPage.SetTypes(type);
        }

        protected override void SetPeople(People people)
        {
            _mainPage.SetPeople(people);
        }

        protected override void SetDate(Dates date)
        {
            _mainPage.SetDate(date);
        }

        protected override void SetLicense(Licenses license)
        {
            _mainPage.SetLicense(license);
        }

        protected override void OpenImageResults(int resultNumber)
        {
            _mainPage.ClickImageResult(resultNumber);
        }

        protected override void AssertResult(string expectedTitle, string expectedUrl)
        {
            _resultDetailedPage.AssertResultTitle(expectedTitle)
              .AssertResultLink(expectedUrl)
              .ClickVisitSiteButton();

            Assert.AreEqual(expectedUrl, _driver.Url);
        }
    }
}
