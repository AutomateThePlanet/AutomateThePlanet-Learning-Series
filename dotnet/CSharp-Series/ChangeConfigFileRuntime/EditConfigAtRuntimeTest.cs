// <copyright file="EditConfigAtRuntimeTest.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ChangeConfigFileRuntime
{
    [TestClass]
    public class EditConfigAtRuntimeTest
    {
        [TestMethod]
        public void EditCarsXml()
        {

            var appConfigFilePath = string.Concat(Assembly.GetExecutingAssembly().Location, ".config");
            var appConfigWriterSettings =
                new ConfigModificatorSettings("//appSettings", "//add[@key='{0}']", appConfigFilePath);

            var value = ConfigurationManager.AppSettings["testKey1"];
            Console.WriteLine("Value before modification: {0}", value);

            ConfigModificator.ChangeValueByKey(
            key: "testKey1",
            value: "ChangedValueByModificator",
            attributeForChange: "value",
            configWriterSettings: appConfigWriterSettings);

            ConfigModificator.RefreshAppSettings();
            value = ConfigurationManager.AppSettings["testKey1"];
            Console.WriteLine("Value after modification: {0}", value);

            //Example how to change Custom XML configuration
            var carsConfigFilePath = "Cars.xml";
            var carsConfigWriterSettings =
                new ConfigModificatorSettings("//cars", "//car[@name='{0}']", carsConfigFilePath);

            ConfigModificator.ChangeValueByKey(
            key: "BMW",
            value: "Mazda",
            attributeForChange: "name",
            configWriterSettings: carsConfigWriterSettings);
        }

    }
}