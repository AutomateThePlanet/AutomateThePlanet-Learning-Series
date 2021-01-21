using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactoryDesignPatternReverseProxy
{
    public class TimeBoundProxy
    {
        public TimeBoundProxy(string proxyIp)
        {
            ProxyIp = proxyIp;
            LastlyUsed = DateTime.MinValue;
        }

        public string ProxyIp { get; set; }
        public DateTime LastlyUsed { get; set; }
    }
}
