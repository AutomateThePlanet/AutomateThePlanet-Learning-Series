using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WebDriver.Tools.Redirects
{
    [XmlRoot(ElementName = "redirects")]
    public class Redirects
    {
        [XmlElement(ElementName = "redirect")]
        public List<Redirect> Redirect { get; set; }
    }
}
