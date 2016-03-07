using System;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Observer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ExecutionBrowserAttribute : Attribute
    {
        public ExecutionBrowserAttribute(BrowserTypes browser)
        {
            this.BrowserType = browser;
        }

        public BrowserTypes BrowserType { get; set; }
    }
}
