// <copyright file="IpAddressSettings.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
// <site>http://automatetheplanet.com/</site>
using System;
using System.Net;

namespace MSBuildTcpIPLogger
{
    public class IpAddressSettings
    {
        public string IpString { get; set; }
        public int Port { get; set; }

        public IpAddressSettings(string ipAddress, string port)
        {
            //IPAddress = IPAddress.Parse(ipAddress);
            Port = int.Parse(port);
            IpString = ipAddress;
        }

        public IpAddressSettings()
        {
        }

        public IpAddressSettings(string wholeAddress)
        {
            var IpAddressA = wholeAddress.Split(':');
            //IPAddress = IPAddress.Parse(IpAddressA[0]);
            IpString = IpAddressA[0];
            Port = int.Parse(IpAddressA[1]);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", IpString, Port);
        }

        public IPAddress GetIPAddress()
        {
            return IPAddress.Parse(IpString);
        }
    }
}
