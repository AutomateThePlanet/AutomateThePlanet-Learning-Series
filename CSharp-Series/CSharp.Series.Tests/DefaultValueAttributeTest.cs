using System.ComponentModel;

namespace CSharp.Series.Tests
{
    public class DefaultValueAttributeTest
    {
        public DefaultValueAttributeTest()
        {
            // Use the DefaultValue propety of each property to actually set it, via reflection.
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
            {
                DefaultValueAttribute attr = (DefaultValueAttribute)prop.Attributes[typeof(DefaultValueAttribute)];
                if (attr != null)
                {
                    prop.SetValue(this, attr.Value);
                }
            }
        }

        [DefaultValue(25)]
        public int Age { get; set; }

        [DefaultValue("Anton")]
        public string FirstName { get; set; }

        [DefaultValue("Angelov")]
        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} is {2}.", this.FirstName, this.LastName, this.Age);
        }
    }
}