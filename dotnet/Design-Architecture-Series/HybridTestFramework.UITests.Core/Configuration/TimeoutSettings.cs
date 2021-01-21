using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class TimeoutSettings : ConfigurationSection
    {
        public TimeoutSettings()
        {
        }

        [ConfigurationProperty("waitForAjaxTimeout")]
        public ValueConfigElement<int> WaitForAjaxTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["waitForAjaxTimeout"];
            }
        }

        [ConfigurationProperty("sleepInterval")]
        public ValueConfigElement<int> SleepInterval
        {
            get
            {
                return (ValueConfigElement<int>) this["sleepInterval"];
            }
        }

        [ConfigurationProperty("elementToBeVisibleTimeout")]
        public ValueConfigElement<int> ElementToBeVisibleTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["elementToBeVisibleTimeout"];
            }
        }

        [ConfigurationProperty("elementToExistTimeout")]
        public ValueConfigElement<int> ЕlementToExistTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["elementToExistTimeout"];
            }
        }

        [ConfigurationProperty("elementToNotExistTimeout")]
        public ValueConfigElement<int> ЕlementToNotExistTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["elementToNotExistTimeout"];
            }
        }

        [ConfigurationProperty("elementToBeClickableTimeout")]
        public ValueConfigElement<int> ЕlementToBeClickableTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["elementToBeClickableTimeout"];
            }
        }

        [ConfigurationProperty("elementNotToBeVisibleTimeout")]
        public ValueConfigElement<int> ЕlementNotToBeVisibleTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["elementNotToBeVisibleTimeout"];
            }
        }

        [ConfigurationProperty("elementToHaveContentTimeout")]
        public ValueConfigElement<int> ЕlementToHaveContentTimeout
        {
            get
            {
                return (ValueConfigElement<int>) this["elementToHaveContentTimeout"];
            }
        }
    }
}
