using System;

namespace SimpleFactoryDesignPatternProxies
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
