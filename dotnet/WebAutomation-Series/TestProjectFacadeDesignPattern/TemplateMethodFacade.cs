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
    public abstract class TemplateMethodFacade
    {
        public void SearchImage(SearchData searchData, string expectedTitle, string expectedUrl)
        {
            OpenSearchPage();
            Search(searchData.SearchTerm);
            SetSize(searchData.Size);
            SetColor(searchData.Color);
            SetTypes(searchData.Type);
            SetPeople(searchData.People);
            SetDate(searchData.Date);
            SetLicense(searchData.License);
            OpenImageResults(searchData.ResultNumber);
            AssertResult(expectedTitle, expectedUrl);
        }

        protected abstract void OpenSearchPage();
        protected abstract void Search(string searchTerm);
        protected abstract void SetSize(Sizes size);
        protected abstract void SetColor(Colors color);
        protected abstract void SetTypes(Types type);
        protected abstract void SetPeople(People people);
        protected abstract void SetDate(Dates date);
        protected abstract void SetLicense(Licenses license);
        protected abstract void OpenImageResults(int resultNumber);
        protected abstract void AssertResult(string expectedTitle, string expectedLink);
    }
}
