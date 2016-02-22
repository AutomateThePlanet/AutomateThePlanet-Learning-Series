using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver.Tools.Redirects.Core;

namespace WebDriver.Tools
{
    [TestClass]
    public class RedirectsTester
    {
        [TestMethod]
        public void TestRedirects()
        {
            var redirectService = new RedirectService(new WebRequestRedirectStrategy());
            using (redirectService)
            {
                redirectService.TestRedirects();
            }
        }
    }
}
