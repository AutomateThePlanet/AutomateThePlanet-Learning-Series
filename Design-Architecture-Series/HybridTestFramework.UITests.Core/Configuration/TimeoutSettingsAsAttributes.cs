using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class TimeoutSettingsAsAttributes : ConfigurationSection
    {
        public TimeoutSettingsAsAttributes()
        {
        }

        [ConfigurationProperty("waitForAjaxTimeout", DefaultValue = "10000", IsRequired = true)]
        public int WaitForAjaxTimeout
        {
            get
            {
                return (int)this["waitForAjaxTimeout"];
            }
            set
            {
                this["waitForAjaxTimeout"] = value;
            }
        }

        [ConfigurationProperty("sleepInterval", IsRequired = true)]
        public int SleepInterval
        {
            get
            {
                return (int) this["sleepInterval"];
            }
        }

        [ConfigurationProperty("elementToBeVisibleTimeout", IsRequired = true)]
        public int ElementToBeVisibleTimeout
        {
            get
            {
                return (int) this["elementToBeVisibleTimeout"];
            }
        }

        [ConfigurationProperty("elementToExistTimeout", IsRequired = true)]
        public int ЕlementToExistTimeout
        {
            get
            {
                return (int) this["elementToExistTimeout"];
            }
        }

        [ConfigurationProperty("elementToNotExistTimeout", IsRequired = true)]
        public int ЕlementToNotExistTimeout
        {
            get
            {
                return (int) this["elementToNotExistTimeout"];
            }
        }

        [ConfigurationProperty("elementToBeClickableTimeout", IsRequired = true)]
        public int ЕlementToBeClickableTimeout
        {
            get
            {
                return (int) this["elementToBeClickableTimeout"];
            }
        }

        [ConfigurationProperty("elementNotToBeVisibleTimeout", IsRequired = true)]
        public int ЕlementNotToBeVisibleTimeout
        {
            get
            {
                return (int) this["elementNotToBeVisibleTimeout"];
            }
        }

        [ConfigurationProperty("elementToHaveContentTimeout", IsRequired = true)]
        public int ЕlementToHaveContentTimeout
        {
            get
            {
                return (int) this["elementToHaveContentTimeout"];
            }
        }
    }
}
