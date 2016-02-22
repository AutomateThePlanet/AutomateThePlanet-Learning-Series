// <copyright file="MigrationLogManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using AAngelov.Utilities.UI.Enums;
    using AAngelov.Utilities.UI.Managers;
    using TestCaseManagerCore.BusinessLogic.Entities;

    /// <summary>
    /// Used to log the current process of the migration and supports the retry logic
    /// </summary>
    public class MigrationLogManager : BaseLogManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationLogManager"/> class.
        /// </summary>
        public MigrationLogManager()
        {
            this.MigrationEntries = new List<MigrationRetryEntry>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationLogManager"/> class.
        /// </summary>
        /// <param name="fullResultsJsonFilePath">The results json path.</param>
        public MigrationLogManager(string fullResultsJsonFilePath) : this()
        { 
            base.FullResultFilePath = fullResultsJsonFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationLogManager"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="folderPath">The folder path.</param>
        public MigrationLogManager(string prefix, string folderPath) : this()
        {
            base.Initialize(FileType.JSON, prefix, folderPath);
        }

        /// <summary>
        /// Gets or sets the migration entries.
        /// </summary>
        /// <value>
        /// The migration entries.
        /// </value>
        public List<MigrationRetryEntry> MigrationEntries { get; set; }

        /// <summary>
        /// Logs the specified source id.
        /// </summary>
        /// <param name="sourceId">The source id.</param>
        /// <param name="destinationId">The destination id.</param>
        /// <param name="isProcessed">if set to <c>true</c> [is processed].</param>
        /// <param name="exception">The exception.</param>
        public void Log(int sourceId, int destinationId, bool isProcessed, string exception = "")
        {
            this.MigrationEntries.Add(new MigrationRetryEntry(sourceId, destinationId, isProcessed, exception));
        }

        /// <summary>
        /// Load the collection from existing file.
        /// </summary>
        public void LoadCollectionFromExistingFile()
        {
            if (File.Exists(base.FullResultFilePath))
            {
                var serializer = new JavaScriptSerializer();
                string text = File.ReadAllText(base.FullResultFilePath);
                this.MigrationEntries = serializer.Deserialize<List<MigrationRetryEntry>>(text);
            }
        }

        /// <summary>
        /// Saves the log collection to JSON file.
        /// </summary>
        public void Save()
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(this.MigrationEntries);
            TextWriter writer = new StreamWriter(base.FullResultFilePath);
            writer.Write(serializedResult);
            writer.Close();
        }

        /// <summary>
        /// Gets the not prossed entries.
        /// </summary>
        /// <returns></returns>
        public List<MigrationRetryEntry> GetNotProssedEntries()
        {
            return this.MigrationEntries.Where(m => !string.IsNullOrEmpty(m.Exception) && m.IsProcessed.Equals(false)).ToList();
        }

        /// <summary>
        /// Gets the prossed items mappings.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetProssedItemsMappings()
        {
            Dictionary<int, int> prossedItemsMappings = new Dictionary<int, int>();
            List<MigrationRetryEntry> prossedItems = this.MigrationEntries.Where(m => string.IsNullOrEmpty(m.Exception) && m.IsProcessed.Equals(true)).ToList();
            foreach (MigrationRetryEntry currentItem in prossedItems)
            {
                if (!prossedItemsMappings.ContainsKey(currentItem.SourceId))
                {
                    prossedItemsMappings.Add(currentItem.SourceId, currentItem.DestinationId);
                }
            }
            return prossedItemsMappings;
        }
    }
}