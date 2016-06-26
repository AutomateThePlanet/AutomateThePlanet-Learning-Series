using HybridTestFramework.UITests.Core;
using OpenQA.Selenium;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IJavaScriptInvoker
    {
        public string InvokeScript(string script)
        {
            IJavaScriptExecutor javaScriptExecutor = 
                driver as IJavaScriptExecutor;
            return (string)javaScriptExecutor.ExecuteScript(script);
        }
    }
}