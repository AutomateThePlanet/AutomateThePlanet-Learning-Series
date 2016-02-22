using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTest
    {
        private TestRunUnitTestExecution executionField;

        private TestRunUnitTestTestMethod testMethodField;

        private TestRunUnitTestOwners ownersField;

        private TestRunUnitTestTestCategory testCategoryField;

        private string nameField;

        private string storageField;

        private string idField;

        /// <remarks/>
        public TestRunUnitTestOwners Owners
        {
            get
            {
                return this.ownersField;
            }
            set
            {
                this.ownersField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestTestCategory TestCategory
        {
            get
            {
                return this.testCategoryField;
            }
            set
            {
                this.testCategoryField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestExecution Execution
        {
            get
            {
                return this.executionField;
            }
            set
            {
                this.executionField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestTestMethod TestMethod
        {
            get
            {
                return this.testMethodField;
            }
            set
            {
                this.testMethodField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string storage
        {
            get
            {
                return this.storageField;
            }
            set
            {
                this.storageField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}