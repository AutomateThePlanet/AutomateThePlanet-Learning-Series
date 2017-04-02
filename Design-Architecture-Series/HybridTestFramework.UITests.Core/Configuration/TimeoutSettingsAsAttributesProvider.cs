using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class TimeoutSettingsAsAttributesProvider : ConfigurationSection
    {
        private static readonly TimeoutSettingsAsAttributes timeoutSettings;

        static TimeoutSettingsAsAttributesProvider()
        {
            try
            {
                timeoutSettings = 
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
                return timeoutSettings.WaitForAjaxTimeout;
            }
        }

        public static int SleepInterval
        {
            get
            {
                return timeoutSettings.SleepInterval;
            }
        }

        public static int ElementToBeVisibleTimeout
        {
            get
            {
                return timeoutSettings.ElementToBeVisibleTimeout;
            }
        }

        public static int ЕlementToExistTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToExistTimeout;
            }
        }

        public static int ЕlementToNotExistTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToNotExistTimeout;
            }
        }

        public static int ЕlementToBeClickableTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToBeClickableTimeout;
            }
        }

        public static int ЕlementNotToBeVisibleTimeout
        {
            get
            {
                return timeoutSettings.ЕlementNotToBeVisibleTimeout;
            }
        }

        public static int ЕlementToHaveContentTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToHaveContentTimeout;
            }
        }
    }
}
