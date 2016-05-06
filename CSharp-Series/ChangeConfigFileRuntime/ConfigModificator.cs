// <copyright file="ConfigModificator.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
using System.Xml;

namespace ChangeConfigFileRuntime
{
    class ConfigModificator
    {
        public static void ChangeValueByKey(string key, string value, string attributeForChange, ConfigModificatorSettings configWriterSettings)
        {
            XmlDocument doc = ConfigModificator.LoadConfigDocument(configWriterSettings.ConfigPath);
            XmlNode rootNode = doc.SelectSingleNode(configWriterSettings.RootNode);

            if (rootNode == null)
            {
                throw new InvalidOperationException("the root node section not found in config file.");
            }
            try
            {
                XmlElement elem = (XmlElement)rootNode.SelectSingleNode(string.Format(configWriterSettings.NodeForEdit, key));
                elem.SetAttribute(attributeForChange, value);
                doc.Save(configWriterSettings.ConfigPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static XmlDocument LoadConfigDocument(string configFilePath)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(configFilePath);
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        public static void RefreshAppSettings()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
