using HybridTestFramework.UITests.Core;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public class SeleniumFrame : IFrame
    {
        private readonly string name;

        public SeleniumFrame(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}