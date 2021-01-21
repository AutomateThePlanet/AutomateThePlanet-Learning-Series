﻿// <copyright file="TorTests.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://automatetheplanet.com/</site>
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebDriverSeleniumTorIntegration
{
    [TestClass]
    public class TorTests
    {
        public IWebDriver Driver { get; set; }

        public Process TorProcess { get; set; }

        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // You should set here the path to your Tor browser exe. Mine was installed on my desktop because of that I'm using the below path.
            var torBinaryPath = string.Concat(desktopPath, @"\Tor Browser\Browser\firefox.exe");
            var process = new Process();
            TorProcess = process;
            TorProcess.StartInfo.FileName = torBinaryPath;
            TorProcess.StartInfo.Arguments = "-n";
            TorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            TorProcess.Start();

            var profile = new FirefoxProfile();
            profile.SetPreference("network.proxy.type", 1);
            profile.SetPreference("network.proxy.socks", "127.0.0.1");
            profile.SetPreference("network.proxy.socks_port", 9150);
            var firefoxOptions = new FirefoxOptions
            {
                Profile = profile
            };
            Driver = new FirefoxDriver(firefoxOptions);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.Quit();
            TorProcess.Kill();
        }

        [TestMethod]
        public void Open_Tor_Browser()
        {
            RefreshTorIdentity();
            Driver.Navigate().GoToUrl(@"http://whatismyipaddress.com/");
            var expression = By.XPath("//*[@id='section_left']/div[2]");
            Wait.Until(x => x.FindElement(expression));
            var element = Driver.FindElement(expression);
            Assert.AreNotEqual("84.40.65.000", element.Text);
        }

        public void RefreshTorIdentity()
        {
            Socket server = null;
            try
            {
                var ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9151);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ip);

                // Please be sure that you have executed the part with the creation of an authentication hash, described in my article!
                server.Send(Encoding.ASCII.GetBytes("AUTHENTICATE \"johnsmith\"" + Environment.NewLine));
                var data = new byte[1024];
                var receivedDataLength = server.Receive(data);
                var stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
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