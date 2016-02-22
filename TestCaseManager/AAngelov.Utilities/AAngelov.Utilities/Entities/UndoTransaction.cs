// <copyright file="UndoTransaction.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.Entities
{
    using System;
    using System.Collections.Generic;
    using AAngelov.Utilities.Contracts;
    using AAngelov.Utilities.Managers;

    /// <summary>
    /// This acts as a container for multiple undo/redo records.
    /// </summary>
    public class UndoTransaction : IDisposable, IUndoRedoRecord
    {
        /// <summary>
        /// The name of the transaction
        /// </summary>
        private string name;

        /// <summary>
        /// If it true => queue is initilized and used otherwise stack
        /// </summary>
        private bool isFifo;

        /// <summary>
        /// The undo redo operations queue
        /// </summary>
        private Queue<IUndoRedoRecord> undoRedoOperationsQueue;

        /// <summary>
        /// The undo redo operations stack
        /// </summary>
        private Stack<IUndoRedoRecord> undoRedoOperationsStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoTransaction"/> class.
        /// </summary>
        /// <param name="nameToSet">The name automatic set.</param>
        /// <param name="isFifoToSet">if set to <c>true</c> [is fifo automatic set].</param>
        public UndoTransaction(string nameToSet = "", bool isFifoToSet = false)
        {
            this.name = nameToSet;
            this.isFifo = isFifoToSet;
            UndoRedoManager.Instance().StartTransaction(this);
            this.undoRedoOperationsQueue = new Queue<IUndoRedoRecord>();
            this.undoRedoOperationsStack = new Stack<IUndoRedoRecord>();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the operations count.
        /// </summary>
        /// <value>
        /// The operations count.
        /// </value>
        public int OperationsCount
        {
            get
            {
                if (this.isFifo)
                {
                    return this.undoRedoOperationsQueue.Count;
                }
                else
                {
                    return this.undoRedoOperationsStack.Count;
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            UndoRedoManager.Instance().EndTransaction(this);
        }

        /// <summary>
        /// Adds the undo redo operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public void AddUndoRedoOperation(IUndoRedoRecord operation)
        {
            if (this.isFifo)
            {
                this.undoRedoOperationsQueue.Enqueue(operation);
            }
            else
            {
                this.undoRedoOperationsStack.Push(operation);
            }
        }

        /// <summary>
        /// Executes the operation saved in the collections.
        /// </summary>
        public void Execute()
        {
            if (this.isFifo)
            {
                while (this.undoRedoOperationsQueue.Count > 0)
                {
                    IUndoRedoRecord currentRecord = this.undoRedoOperationsQueue.Dequeue();
                    currentRecord.Execute();
                }
            }
            else
            {
                while (this.undoRedoOperationsStack.Count > 0)
                {
                    IUndoRedoRecord currentRecord = this.undoRedoOperationsStack.Pop();
                    currentRecord.Execute();
                }
            }
        }
    }
}