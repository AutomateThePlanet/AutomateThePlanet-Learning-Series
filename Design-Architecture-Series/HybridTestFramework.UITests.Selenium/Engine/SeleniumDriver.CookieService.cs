using HybridTestFramework.UITests.Core;
using OpenQA.Selenium;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : ICookieService
    {
        public string GetCookie(string host, string cookieName)
        {
            var myCookie = this.driver.Manage().Cookies.GetCookieNamed(cookieName);
            return myCookie.Value;
        }

        public void AddCookie(string cookieName, string cookieValue, string host)
        {
            Cookie cookie = new Cookie(cookieName, cookieValue);
            this.driver.Manage().Cookies.AddCookie(cookie);
        }

        public void DeleteCookie(string cookieName)
        {
            this.driver.Manage().Cookies.DeleteCookieNamed("CookieName");
        }

        public void CleanAllCookies()
        {
            this.driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
