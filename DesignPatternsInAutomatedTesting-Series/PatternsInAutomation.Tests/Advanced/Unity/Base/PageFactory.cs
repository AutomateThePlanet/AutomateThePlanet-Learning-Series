using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace PatternsInAutomatedTests.Advanced.Unity.Base
{
    public static class PageFactory
    {
        private static IUnityContainer container; 

        static PageFactory()
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = "unity.config" };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            container = new UnityContainer(); 
            container.LoadConfiguration(unitySection);
        }

        public static T Get<T>()
        {
            return container.Resolve<T>();
        }
    }
}
