using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestDataDrivenResults
    {
        private TestRunUnitTestResultOutput outputField;

        private string executionIdField;

        private string testIdField;

        private string testNameField;

        private string computerNameField;

        private System.TimeSpan durationField;

        private System.DateTime startTimeField;

        private System.DateTime endTimeField;

        private string testTypeField;

        private string outcomeField;

        private string testListIdField;

        private string relativeResultsDirectoryField;
        

        /// <remarks/>
        public TestRunUnitTestResultOutput Output
        {
            get
            {
                return this.outputField;
            }
            set
            {
                this.outputField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttributeAttribute]
        public string executionId
        {
            get
            {
                return this.executionIdField;
            }
            set
            {
                this.executionIdField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string testId
        {
            get
            {
                return this.testIdField;
            }
            set
            {
                this.testIdField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string testName
        {
            get
            {
                return this.testNameField;
            }
            set
            {
                this.testNameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string computerName
        {
            get
            {
                return this.computerNameField;
            }
            set
            {
                this.computerNameField = value;
            }
        }

        [XmlIgnore]
        public TimeSpan duration
        {
            get
            { 
                return this.durationField;
            }
            set
            {
                this.durationField = value;
            }
        }
        
        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlAttributeAttribute(DataType = "duration", AttributeName = "duration")]
        public string DurationString
        {
            get 
            { 
                return this.duration.ToString(); 
            }
            set 
            {
                this.duration = string.IsNullOrEmpty(value) ? TimeSpan.Zero : TimeSpan.Parse(value);
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime startTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime endTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string testType
        {
            get
            {
                return this.testTypeField;
            }
            set
            {
                this.testTypeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string outcome
        {
            get
            {
                return this.outcomeField;
            }
            set
            {
                this.outcomeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string testListId
        {
            get
            {
                return this.testListIdField;
            }
            set
            {
                this.testListIdField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string relativeResultsDirectory
        {
            get
            {
                return this.relativeResultsDirectoryField;
            }
            set
            {
                this.relativeResultsDirectoryField = value;
            }
        }
    }
}