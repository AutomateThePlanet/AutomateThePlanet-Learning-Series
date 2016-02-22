using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.ProcessExecutionProviderTests
{
    [TestClass]
    public class ProcessExecutionProviderTests_CurrentProcessWaitForExit_Should
    {
        [TestMethod]
        public void FinishCorrectly()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var processExecutionProvider = new ProcessExecutionProvider("cmd.exe", null, log);

            processExecutionProvider.ExecuteProcessWithAdditionalArguments("ipconfig");
           
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(200);
                processExecutionProvider.CurrentProcess.Kill();
            });
            processExecutionProvider.CurrentProcessWaitForExit();
            Assert.IsTrue(processExecutionProvider.CurrentProcess.HasExited);
        }
    }
}