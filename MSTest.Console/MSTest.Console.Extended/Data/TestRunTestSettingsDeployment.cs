using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettingsDeployment
    {
        private string userDeploymentRootField;

        private bool useDefaultDeploymentRootField;

        private string runDeploymentRootField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string userDeploymentRoot
        {
            get
            {
                return this.userDeploymentRootField;
            }
            set
            {
                this.userDeploymentRootField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool useDefaultDeploymentRoot
        {
            get
            {
                return this.useDefaultDeploymentRootField;
            }
            set
            {
                this.useDefaultDeploymentRootField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string runDeploymentRoot
        {
            get
            {
                return this.runDeploymentRootField;
            }
            set
            {
                this.runDeploymentRootField = value;
            }
        }
    }
}