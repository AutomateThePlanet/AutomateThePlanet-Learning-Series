using System;
using System.Linq;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResultOutput
    {
        private string stdOutField;

        private string debugTraceField;

        private TestRunUnitTestResultOutputErrorInfo errorInfoField;

        /// <remarks/>
        public string StdOut
        {
            get
            {
                return this.stdOutField;
            }
            set
            {
                this.stdOutField = value;
            }
        }

        /// <remarks/>
        public string DebugTrace
        {
            get
            {
                return this.debugTraceField;
            }
            set
            {
                this.debugTraceField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestResultOutputErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }
    }
}