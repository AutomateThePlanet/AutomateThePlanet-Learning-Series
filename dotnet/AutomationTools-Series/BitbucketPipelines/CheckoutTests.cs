using BitbucketPipelines.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace BitbucketPipelines
{
    public class CheckoutTests
    {
        private IWebDriver _driver;
        private CheckoutPage _checkoutPage;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");
            _driver = new ChromeDriver(options);
            _checkoutPage = new CheckoutPage(_driver);
            _checkoutPage.Navigate();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void FormSent_When_InfoValid()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
                LastName: "Angelov",
                Username: "aangelov",
                Email: "info@berlinspaceflowers.com",
                Address1: "1 Willi Brandt Avenue Tiergarten",
                Address2: "Lützowplatz 17",
                Country: 1,
                State: 1,
                Zip: "10115",
                CardName: "Anton Angelov",
                CardNumber: "1234567890123456",
                CardExpiration: "12/23",
                CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertFormSent();
        }

        [Test]
        public void ValidatedFirstName_When_FirstNameNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "",
                LastName: "Angelov",
                Username: "aangelov",
                Email: "info@berlinspaceflowers.com",
                Address1: "1 Willi Brandt Avenue Tiergarten",
                Address2: "Lützowplatz 17",
                Country: 1,
                State: 1,
                Zip: "10115",
                CardName: "Anton Angelov",
                CardNumber: "1234567890123456",
                CardExpiration: "12/23",
                CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertFirstNameValidationDisplayed();
        }

        [Test]
        public void ValidatedLastName_When_FirstNameNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
                LastName: "",
                Username: "aangelov",
                Email: "info@berlinspaceflowers.com",
                Address1: "1 Willi Brandt Avenue Tiergarten",
                Address2: "Lützowplatz 17",
                Country: 1,
                State: 1,
                Zip: "10115",
                CardName: "Anton Angelov",
                CardNumber: "1234567890123456",
                CardExpiration: "12/23",
                CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertLastNameValidationDisplayed();
        }

        [Test]
        public void ValidatedUsername_When_UsernameNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
            LastName: "Angelov",
            Username: "",
            Email: "info@berlinspaceflowers.com",
            Address1: "1 Willi Brandt Avenue Tiergarten",
            Address2: "Lützowplatz 17",
            Country: 1,
            State: 1,
            Zip: "10115",
            CardName: "Anton Angelov",
            CardNumber: "1234567890123456",
            CardExpiration: "12/23",
            CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertUsernameValidationDisplayed();
        }

        [Test]
        public void ValidatedEmail_When_EmailNotValid()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
            LastName: "Angelov",
            Username: "aangelov",
            Email: "asdasd",
            Address1: "1 Willi Brandt Avenue Tiergarten",
            Address2: "Lützowplatz 17",
            Country: 1,
            State: 1,
            Zip: "10115",
            CardName: "Anton Angelov",
            CardNumber: "1234567890123456",
            CardExpiration: "12/23",
            CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertEmailValidationDisplayed();
        }

        [Test]
        public void ValidatedAddress1_When_Address1NotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
             LastName: "Angelov",
             Username: "aangelov",
             Email: "info@berlinspaceflowers.com",
             Address1: "",
             Address2: "Lützowplatz 17",
             Country: 1,
             State: 1,
             Zip: "10115",
             CardName: "Anton Angelov",
             CardNumber: "1234567890123456",
             CardExpiration: "12/23",
             CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertAddress1ValidationDisplayed();
        }

        [Test]
        public void ValidatedZip_When_ZipNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
            LastName: "Angelov",
            Username: "aangelov",
            Email: "info@berlinspaceflowers.com",
            Address1: "1 Willi Brandt Avenue Tiergarten",
            Address2: "Lützowplatz 17",
            Country: 1,
            State: 1,
            Zip: "",
            CardName: "Anton Angelov",
            CardNumber: "1234567890123456",
            CardExpiration: "12/23",
            CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertZipValidationDisplayed();
        }

        [Test]
        public void ValidatedCardName_When_CardNameNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
            LastName: "Angelov",
            Username: "aangelov",
            Email: "info@berlinspaceflowers.com",
            Address1: "1 Willi Brandt Avenue Tiergarten",
            Address2: "Lützowplatz 17",
            Country: 1,
            State: 1,
            Zip: "10115",
            CardName: "",
            CardNumber: "1234567890123456",
            CardExpiration: "12/23",
            CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertCardNameValidationDisplayed();
        }

        [Test]
        public void ValidatedCardExpiration_When_CardExpirationNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
            LastName: "Angelov",
            Username: "aangelov",
            Email: "info@berlinspaceflowers.com",
            Address1: "1 Willi Brandt Avenue Tiergarten",
            Address2: "Lützowplatz 17",
            Country: 1,
            State: 1,
            Zip: "10115",
            CardName: "Anton Angelov",
            CardNumber: "1234567890123456",
            CardExpiration: "",
            CardCVV: "123");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertCardExpirationValidationDisplayed();
        }

        [Test]
        public void ValidatedCardCVV_When_CardCVVNotSet()
        {
            var clientInfo = new ClientInfo(FirstName: "Anton",
            LastName: "Angelov",
            Username: "aangelov",
            Email: "info@berlinspaceflowers.com",
            Address1: "1 Willi Brandt Avenue Tiergarten",
            Address2: "Lützowplatz 17",
            Country: 1,
            State: 1,
            Zip: "10115",
            CardName: "Anton Angelov",
            CardNumber: "1234567890123456",
            CardExpiration: "12/23",
            CardCVV: "");

            _checkoutPage.FillInfo(clientInfo);

            _checkoutPage.AssertCardCVVValidationDisplayed();
        }
    }
}