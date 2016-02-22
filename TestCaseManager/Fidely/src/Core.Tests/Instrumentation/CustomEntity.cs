using System;
using System.ComponentModel;

namespace Fidely.Framework.Tests.Instrumentation
{
    public class CustomEntity
    {
        [Description("Entity Id")]
        public int Id { get; set; }

        [Alias("nm")]
        [Description("Entity Name")]
        public string Name { get; set; }

        [Alias("d")]
        [Alias("desc", Description = "Entity Description")]
        public string Description { get; set; }

        private string getOnlyField = "GetOnly Value";
        [Alias("get", Description="Description Property")]
        [Description("Description Attribute")]
        public string GetOnly { get { return getOnlyField; } }

        [NotEvaluate]
        public float Ignored { get; set; }

        private double setOnlyField;
        [Alias("set")]
        public double SetOnly { set { setOnlyField = value; } }

        [Alias("private")]
        private byte PrivateProperty { get; set; }

        [Alias("protected")]
        protected double ProtectedProperty { get; set; }

        [Alias("internal")]
        internal bool InternalProperty { get; set; }

        [Alias("protected_internal")]
        protected internal decimal ProtectedInternalProperty { get; set; }
    }
}
