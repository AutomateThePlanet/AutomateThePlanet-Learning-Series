using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium;

namespace WebDriverTestsAzureFunctions.Http
{
    public class RunStandaloneChrome
    {
        [FunctionName("RunStandaloneChrome")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger logger)
        {
            string result = "Nothing Happend";
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--headless", "--no-sandbox", "--disable-gpu", "--whitelisted-ips");
                var service = ChromeDriverService.CreateDefaultService("/usr/bin/", "chromedriver");
                using IWebDriver driver = new ChromeDriver(service, chromeOptions);
                driver.Navigate().GoToUrl(new Uri("http://demos.bellatrix.solutions/"));
                result = driver.Title;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
           
            return new OkObjectResult(result);
        }
    }
}
