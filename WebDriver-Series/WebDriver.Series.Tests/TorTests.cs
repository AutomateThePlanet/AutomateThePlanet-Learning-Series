using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebDriver.Series.Tests
{
    [TestClass]
    class TorTests
    {
        public IWebDriver Driver { get; set; }

        public Process TorProcess { get; set; }

        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            string desktopPath  = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // You should set here the path to your Tor browser exe. Mine was installed on my desktop because of that I'm using the below path.
            String torBinaryPath = string.Concat(desktopPath, @"\Tor Browser\Browser\firefox.exe");
            this.TorProcess = new Process();
            this.TorProcess.StartInfo.FileName = torBinaryPath;
            this.TorProcess.StartInfo.Arguments = "-n";
            this.TorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            this.TorProcess.Start();

            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("network.proxy.type", 1);
            profile.SetPreference("network.proxy.socks", "127.0.0.1");
            profile.SetPreference("network.proxy.socks_port", 9150);
            this.Driver = new FirefoxDriver(profile);
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(60));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
            this.TorProcess.Kill();
        }

        [TestMethod]
        public void Open_Tor_Browser()
        {
            this.RefreshTorIdentity();
            this.Driver.Navigate().GoToUrl(@"http://whatismyipaddress.com/");
            var expression = By.XPath("//*[@id='section_left']/div[2]");
            this.Wait.Until(x => x.FindElement(expression));
            var element = this.Driver.FindElement(expression);
            Assert.AreNotEqual<string>("84.40.65.000", element.Text);
        }

        public void RefreshTorIdentity()
        {
            Socket server = null;
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9151);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip);
                // Please be sure that you have executed the part with the creation of an authentication hash, described in my article!
                server.Send(Encoding.ASCII.GetBytes("AUTHENTICATE \"johnsmith\"" + Environment.NewLine));
                byte[] data = new byte[1024];
                int receivedDataLength = server.Receive(data);
                string stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                server.Send(Encoding.ASCII.GetBytes("SIGNAL NEWNYM" + Environment.NewLine));
                data = new byte[1024];
                receivedDataLength = server.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                if (!stringData.Contains("250"))
                {
                    Console.WriteLine("Unable to signal new user to server.");
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                }
            }
            finally
            {
                server.Close();
            }
        }
    }
}