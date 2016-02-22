using CSharp.Series.NLog.Tests.Loggers;

namespace CSharp.Series.NLog.Tests
{
    public class Calculator
    {
        private readonly ILogger logger;

        public Calculator(ILogger logger)
        {
            this.logger = logger;
        }

        public int Sum(int a, int b)
        {
            logger.LogInfo(string.Format("Sum {0} and {1}", a, b));
            return a + b;
        }
    }
}
