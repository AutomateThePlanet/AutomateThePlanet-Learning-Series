using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettingsExecution
    {
        private object testTypeSpecificField;

        private TestRunTestSettingsExecutionAgentRule agentRuleField;

        /// <remarks/>
        public object TestTypeSpecific
        {
            get
            {
                return this.testTypeSpecificField;
            }
            set
            {
                this.testTypeSpecificField = value;
            }
        }

        /// <remarks/>
        public TestRunTestSettingsExecutionAgentRule AgentRule
        {
            get
            {
                return this.agentRuleField;
            }
            set
            {
                this.agentRuleField = value;
            }
        }
    }
}