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