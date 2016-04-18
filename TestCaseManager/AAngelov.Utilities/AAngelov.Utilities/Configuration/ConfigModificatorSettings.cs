// <copyright file="ConfigModificatorSettings.cs" company="Automate The Planet Ltd.">
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

namespace AAngelov.Utilities.Configuration
{
    /// <summary>
    /// Contains Config Writer Setting Properties
    /// </summary>
    public class ConfigModificatorSettings
    {
        /// <summary>
        /// Gets or sets the application settings node.
        /// </summary>
        /// <value>
        /// The application settings node.
        /// </value>
        public string RootNode { get; set; }

        /// <summary>
        /// Gets or sets the node for edit.
        /// </summary>
        /// <value>
        /// The node for edit.
        /// </value>
        public string NodeForEdit { get; set; }

        /// <summary>
        /// Gets or sets the configuration path.
        /// </summary>
        /// <value>
        /// The configuration path.
        /// </value>
        public string ConfigPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigModificatorSettings"/> class.
        /// </summary>
        /// <param name="appSettingsNode">The application settings node.</param>
        /// <param name="nodeForEdit">The node for edit.</param>
        /// <param name="configPath">The configuration path.</param>
        public ConfigModificatorSettings(String appSettingsNode, String nodeForEdit, string configPath)
        {
            this.RootNode = appSettingsNode;
            this.NodeForEdit = nodeForEdit;
            this.ConfigPath = configPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigModificatorSettings"/> class.
        /// </summary>
        /// <param name="appSettingsNode">The application settings node.</param>
        /// <param name="configPath">The configuration path.</param>
        public ConfigModificatorSettings(String appSettingsNode, string configPath)
        {
            this.RootNode = appSettingsNode;
            this.ConfigPath = configPath;
        }
    }
}