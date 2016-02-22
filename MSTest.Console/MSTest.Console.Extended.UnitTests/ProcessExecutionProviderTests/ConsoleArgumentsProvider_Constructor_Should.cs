using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using log4net;

namespace MSTest.Console.Extended.UnitTests.ProcessExecutionProviderTests
{
    [TestClass]
    public class ConsoleArgumentsProvider_Constructor_Should
    {
        [TestMethod]
        public void StartProcessWithArguments()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var processExecutionProvider = new ProcessExecutionProvider("cmd.exe", null, log);

            processExecutionProvider.ExecuteProcessWithAdditionalArguments("ipconfig");

            Assert.IsNotNull(processExecutionProvider.CurrentProcess);
            Assert.IsNotNull(processExecutionProvider.CurrentProcess.StartInfo);
            Assert.AreEqual("cmd.exe", processExecutionProvider.CurrentProcess.StartInfo.FileName);
            Assert.AreEqual("ipconfig", processExecutionProvider.CurrentProcess.StartInfo.Arguments);
            processExecutionProvider.CurrentProcess.Kill();
        }

        [TestMethod]
        public void StartProcessWithDefaultConsoleArguments_WhenArgumentsNotSet()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            var processExecutionProvider = new ProcessExecutionProvider("cmd.exe", consoleArgumentsProvider, log);

            processExecutionProvider.ExecuteProcessWithAdditionalArguments();

            Assert.IsNotNull(processExecutionProvider.CurrentProcess);
            Assert.IsNotNull(processExecutionProvider.CurrentProcess.StartInfo);
            Assert.AreEqual("cmd.exe", processExecutionProvider.CurrentProcess.StartInfo.FileName);
            Assert.AreEqual("ipconfig", processExecutionProvider.CurrentProcess.StartInfo.Arguments);
            processExecutionProvider.CurrentProcess.Kill();
        }
    }
}