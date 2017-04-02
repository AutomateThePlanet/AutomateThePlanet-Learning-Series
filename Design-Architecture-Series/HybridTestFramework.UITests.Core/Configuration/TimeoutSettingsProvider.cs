using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class TimeoutSettingsProvider : ConfigurationSection
    {
        private static readonly TimeoutSettings timeoutSettings;

        static TimeoutSettingsProvider()
        {
            try
            {
                timeoutSettings = 
                    (TimeoutSettings) 
                    ConfigurationManager.GetSection(sectionName: "TimeoutSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new ConfigurationErrorsException(
                    message: "Please configure correctly the TimeoutSettings section.",
                    inner: ex);
            }
        }

        public static int WaitForAjaxTimeout
        {
            get
            {
                return timeoutSettings.WaitForAjaxTimeout.Value;
            }
        }

        public static int SleepInterval
        {
            get
            {
                return timeoutSettings.SleepInterval.Value;
            }
        }

        public static int ElementToBeVisibleTimeout
        {
            get
            {
                return timeoutSettings.ElementToBeVisibleTimeout.Value;
            }
        }

        public static int ЕlementToExistTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToExistTimeout.Value;
            }
        }

        public static int ЕlementToNotExistTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToNotExistTimeout.Value;
            }
        }

        public static int ЕlementToBeClickableTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToBeClickableTimeout.Value;
            }
        }

        public static int ЕlementNotToBeVisibleTimeout
        {
            get
            {
                return timeoutSettings.ЕlementNotToBeVisibleTimeout.Value;
            }
        }

        public static int ЕlementToHaveContentTimeout
        {
            get
            {
                return timeoutSettings.ЕlementToHaveContentTimeout.Value;
            }
        }
    }
}
