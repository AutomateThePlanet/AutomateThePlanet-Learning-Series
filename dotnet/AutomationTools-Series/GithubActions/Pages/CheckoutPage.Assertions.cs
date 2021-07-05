using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace GithubActions.Pages
{
    public partial class CheckoutPage
    {
        public void AssertFormSent()
        {
            Assert.True(_driver.Url.Contains("paymentMethod=on"), "Form not sent");
        }

        public void AssertFirstNameValidationDisplayed()
        {
            Assert.True(FirstNameValidation.Displayed);
        }

        public void AssertLastNameValidationDisplayed()
        {
            Assert.True(LastNameValidation.Displayed);
        }

        public void AssertUsernameValidationDisplayed()
        {
            Assert.True(UsernameValidation.Displayed);
        }

        public void AssertEmailValidationDisplayed()
        {
            Assert.True(EmailValidation.Displayed);
        }

        public void AssertAddress1ValidationDisplayed()
        {
            Assert.True(Address1Validation.Displayed);
        }

        public void AssertCountryValidationDisplayed()
        {
            Assert.True(CountryValidation.Displayed);
        }

        public void AssertStateValidationDisplayed()
        {
            Assert.True(StateValidation.Displayed);
        }

        public void AssertZipValidationDisplayed()
        {
            Assert.True(ZipValidation.Displayed);
        }

        public void AssertCardNameValidationDisplayed()
        {
            Assert.True(CardNameValidation.Displayed);
        }

        public void AssertCardNumberValidationDisplayed()
        {
            Assert.True(CardNumberValidation.Displayed);
        }

        public void AssertCardExpirationValidationDisplayed()
        {
            Assert.True(CardExpirationValidation.Displayed);
        }

        public void AssertCardCVVValidationDisplayed()
        {
            Assert.True(CardCVVValidation.Displayed);
        }
    }
}
