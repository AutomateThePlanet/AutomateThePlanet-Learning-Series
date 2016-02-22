using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebDriver.Series.Tests.EdgeTests.Pages
{
    public class HealthyDietGeneratorPage
    {
        public readonly string Url = @"http://automatetheplanet.com/healthy-diet-menu-generator/";

        public HealthyDietGeneratorPage(IWebDriver browser)
        {
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_18")]
        public IWebElement AddAdditionalSugarCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_19_1")]
        public IWebElement VentiCoffeeRadioButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ninja_forms_field_21']")]
        public IWebElement BurgersDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_27_2")]
        public IWebElement SmotheredChocolateCakeCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_22")]
        public IWebElement AddSomethingToDietTextArea { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ninja_forms_field_20_div_wrap']/span/div[11]/a")]
        public IWebElement RockStarRating { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_23")]
        public IWebElement FirstNameTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_24")]
        public IWebElement LastNameTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_25")]
        public IWebElement EmailTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ninja_forms_field_28")]
        public IWebElement AwsomeDietSubmitButton { get; set; }
    }
}