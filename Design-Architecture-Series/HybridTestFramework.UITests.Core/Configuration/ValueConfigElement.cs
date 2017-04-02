using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class ValueConfigElement<T> : ConfigurationElement
    {
        public ValueConfigElement()
        {
        }

        [ConfigurationProperty("value")]
        public T Value
        {
            get
            {
                return (T) this["value"];
            }
        }
    }
}
