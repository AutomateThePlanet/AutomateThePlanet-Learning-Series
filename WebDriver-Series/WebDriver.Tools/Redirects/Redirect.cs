using System.Xml.Serialization;

namespace WebDriver.Tools.Redirects
{
    [XmlRoot(ElementName = "redirect")]
    public class Redirect
    {
        [XmlAttribute(AttributeName = "from")]
        public string From { get; set; }

        [XmlAttribute(AttributeName = "to")]
        public string To { get; set; }
    }
}
