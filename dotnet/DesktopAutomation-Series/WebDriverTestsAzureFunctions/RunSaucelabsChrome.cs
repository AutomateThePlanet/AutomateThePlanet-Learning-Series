using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Remote;

namespace WebDriverTestsAzureFunctions.Http
{
    public class RunSauceLabsChrome
    {
        [FunctionName("RunSauceLabsChrome")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger logger)
        {
            string result = "Nothing Happend";
            try
            {
                var options = new ChromeOptions();
                options.AddAdditionalOption("browserName", "Chrome");
                options.AddAdditionalOption("platform", "Windows 8.1");
                options.AddAdditionalOption("version", "49.0");

                options.AddAdditionalOption("username", "autoCloudTester");
                options.AddAdditionalOption("accessKey", "70dccdcf-a9fd-4f55-aa07-12b051f6c83e");
                using var _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), options);
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                _driver.Navigate().GoToUrl(new Uri("http://demos.bellatrix.solutions/"));
                result = _driver.Title;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
           
            return new OkObjectResult(result);
        }
    }
}