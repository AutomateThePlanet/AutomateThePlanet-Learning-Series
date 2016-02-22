using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WebDriver.Tools.Redirects
{
    [XmlRoot(ElementName = "site")]
    public class Site
    {
        [XmlElement(ElementName = "redirects")]
        public Redirects Redirects { get; set; }

        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}