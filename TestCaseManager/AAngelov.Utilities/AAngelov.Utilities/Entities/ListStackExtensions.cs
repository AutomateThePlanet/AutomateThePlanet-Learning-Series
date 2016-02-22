// <copyright file="ListStackExtensions.cs" company="CodeProject">
// http://www.codeproject.com/Articles/456591/Simple-Undo-redo-library-for-Csharp-NET?msg=4572235#xx4572235xx All rights reserved.
// </copyright>
// <author>Y Sujan</author>

namespace AAngelov.Utilities.Entities
{
    using System.Collections.Generic;
    using AAngelov.Utilities.Contracts;

    /// <summary>
    /// Extension methods which allow a List to be used as a stack. This was created as we need to be able to manipulate the stack size dynamically
    /// which is not allowed by the Stack class
    /// </summary>
    public static class ListStackExtensions
    {
        /// <summary>
        /// Pushes the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        public static void Push(this List<IUndoRedoRecord> list, IUndoRedoRecord item)
        {
            list.Insert(0, item);
        }

        /// <summary>
        /// Pops the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>the undo record</returns>
        public static IUndoRedoRecord Pop(this List<IUndoRedoRecord> list)
        {
            IUndoRedoRecord ret = list[0];
            list.RemoveAt(0);
            return ret;
        }
    }
}