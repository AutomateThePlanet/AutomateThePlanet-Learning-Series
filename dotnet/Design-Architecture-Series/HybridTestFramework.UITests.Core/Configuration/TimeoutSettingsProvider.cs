using System.Configuration;

namespace HybridTestFramework.UITests.Core.Configuration
{
    public class TimeoutSettingsProvider : ConfigurationSection
    {
        private static readonly TimeoutSettings TimeoutSettings;

        static TimeoutSettingsProvider()
        {
            try
            {
                TimeoutSettings = 
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
                return TimeoutSettings.WaitForAjaxTimeout.Value;
            }
        }

        public static int SleepInterval
        {
            get
            {
                return TimeoutSettings.SleepInterval.Value;
            }
        }

        public static int ElementToBeVisibleTimeout
        {
            get
            {
                return TimeoutSettings.ElementToBeVisibleTimeout.Value;
            }
        }

        public static int ЕlementToExistTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToExistTimeout.Value;
            }
        }

        public static int ЕlementToNotExistTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToNotExistTimeout.Value;
            }
        }

        public static int ЕlementToBeClickableTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToBeClickableTimeout.Value;
            }
        }

        public static int ЕlementNotToBeVisibleTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementNotToBeVisibleTimeout.Value;
            }
        }

        public static int ЕlementToHaveContentTimeout
        {
            get
            {
                return TimeoutSettings.ЕlementToHaveContentTimeout.Value;
            }
        }
    }
}
