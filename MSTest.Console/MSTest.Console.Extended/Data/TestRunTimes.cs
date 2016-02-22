using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTimes
    {
        private System.DateTime creationField;

        private System.DateTime queuingField;

        private System.DateTime startField;

        private System.DateTime finishField;

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime creation
        {
            get
            {
                return this.creationField;
            }
            set
            {
                this.creationField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime queuing
        {
            get
            {
                return this.queuingField;
            }
            set
            {
                this.queuingField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime finish
        {
            get
            {
                return this.finishField;
            }
            set
            {
                this.finishField = value;
            }
        }
    }
}