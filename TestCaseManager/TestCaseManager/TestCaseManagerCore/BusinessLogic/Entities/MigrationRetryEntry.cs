// <copyright file="MigrationRetryEntry.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Used to log the current migration process
    /// </summary>
    [DataContract]
    public class MigrationRetryEntry
    {
        /// <summary>
        /// Gets or sets the source id.
        /// </summary>
        /// <value>
        /// The source id.
        /// </value>
        [DataMember]
        public int SourceId { get; set; }

        /// <summary>
        /// Gets or sets the destination id.
        /// </summary>
        /// <value>
        /// The destination id.
        /// </value>
        [DataMember]
        public int DestinationId { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        [DataMember]
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsProcessed { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRetryEntry"/> class.
        /// </summary>
        /// <param name="sourceId">The source id.</param>
        /// <param name="destinationId">The destination id.</param>
        /// <param name="isProcessed">if set to <c>true</c> [is processed].</param>
        /// <param name="exception">The exception.</param>
        public MigrationRetryEntry(int sourceId, int destinationId, bool isProcessed, string exception)
        {
            this.SourceId = sourceId;
            this.DestinationId = destinationId;
            this.IsProcessed = isProcessed;
            this.Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRetryEntry"/> class.
        /// </summary>
        /// <param name="sourceId">The source id.</param>
        /// <param name="destinationId">The destination id.</param>
        /// <param name="isProcessed">if set to <c>true</c> [is processed].</param>
        public MigrationRetryEntry(int sourceId, int destinationId, bool isProcessed) : this(sourceId, destinationId, isProcessed, "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRetryEntry"/> class.
        /// </summary>
        public MigrationRetryEntry()
        {
        }
    }
}