using AAngelov.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConfigModificator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example how to change app.config file associated to your application. 
            //You can use the same approach for web.configs and other type of configurations
            string appConfigFilePath = string.Concat(Assembly.GetExecutingAssembly().Location, ".config");
            ConfigModificatorSettings appConfigWriterSettings = 
                new ConfigModificatorSettings("//appSettings", "//add[@key='{0}']", appConfigFilePath);
          
            string value = ConfigurationManager.AppSettings["testKey1"];
            System.Console.WriteLine("Value before modification: {0}", value);

            ConfigModificator.ChangeValueByKey(
                                                key: "testKey1", 
                                                value: "ChangedValueByModificator", 
                                                attributeForChange: "value", 
                                                configWriterSettings: appConfigWriterSettings);

            ConfigModificator.RefreshAppSettings();
            value = ConfigurationManager.AppSettings["testKey1"];
            System.Console.WriteLine("Value after modification: {0}", value);

            //Example how to change Custom XML configuration
            string carsConfigFilePath = "Cars.xml";
            ConfigModificatorSettings carsConfigWriterSettings = 
                new ConfigModificatorSettings("//cars", "//car[@name='{0}']", carsConfigFilePath);

            ConfigModificator.ChangeValueByKey(
                                               key: "BMW",
                                               value: "Mazda",
                                               attributeForChange: "name",
                                               configWriterSettings: carsConfigWriterSettings);
        }
    }
}
