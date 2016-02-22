using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using CSharp.Series.NLog.Tests.Loggers;
using CSharp.Series.NLog.Tests;

namespace CSharp.Series.NLog.UnitTests
{
    [TestClass]
    public class CalculatorShould
    {
        [TestMethod]
        public void LogMessageWhenSumNumbers()
        {
            //Arrange
            var logger = Mock.Create<ILogger>();
            string loggedMessage = string.Empty;
            Mock.Arrange(() => logger.LogInfo(Arg.AnyString)).DoInstead(() => loggedMessage = "Automate The Planet rocks!");
            Calculator calculator = new Calculator(logger);

            //Act
            calculator.Sum(1, 1);

            //Assert
            Assert.AreEqual<string>("Automate The Planet rocks!", loggedMessage);
        }
    }
}
