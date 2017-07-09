using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class TimeoutSettingsAsAttributesProvider : ConfigurationSection
    {
        private static readonly TimeoutSettingsAsAttributes TimeoutSettings;

        static TimeoutSettingsAsAttributesProvider()
        {
            try
            {
                TimeoutSettings = 
                    (TimeoutSettingsAsAttributes) 
                    ConfigurationManager.GetSection(sectionName: "TimeoutSettingsAsAttributes");
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new ConfigurationErrorsException(
                    message: "Please configure correctly the TimeoutSettingsAsAttributes section.",
                    inner: ex);
            }
        }

        public static int WaitForAjaxTimeout
        {
            get
            {
                return TimeoutSettings.WaitForAjaxTimeout;
            }
        }

        public static int SleepInterval
        {
            get
            {
                return TimeoutSettings.SleepInterval;
            }
        }

        public static int ElementToBeVisibleTimeout
        {
            get
            {
                return TimeoutSettings.ElementToBeVisibleTimeout;
            }
        }

        public static int ЕlementToExistTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToExistTimeout;
            }
        }

        public static int ЕlementToNotExistTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToNotExistTimeout;
            }
        }

        public static int ЕlementToBeClickableTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToBeClickableTimeout;
            }
        }

        public static int ЕlementNotToBeVisibleTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementNotToBeVisibleTimeout;
            }
        }

        public static int ЕlementToHaveContentTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToHaveContentTimeout;
            }
        }
    }
}
