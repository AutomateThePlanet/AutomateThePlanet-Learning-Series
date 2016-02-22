using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WebDriver.Tools.Redirects
{
    [XmlRoot(ElementName = "sites")]
    public class Sites
    {
        [XmlElement(ElementName = "site")]
        public List<Site> Site { get; set; }
    }
}
