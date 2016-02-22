// <copyright file="IUndoRedoRecord.cs" company="CodeProject">
// http://www.codeproject.com/Articles/456591/Simple-Undo-redo-library-for-Csharp-NET?msg=4572235#xx4572235xx All rights reserved.
// </copyright>
// <author>Y Sujan</author>

namespace AAngelov.Utilities.Contracts
{
    /// <summary>
    /// This is implemented by classes which act as records for storage of undo/redo records/
    /// </summary>
    public interface IUndoRedoRecord
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute();        
    }
}