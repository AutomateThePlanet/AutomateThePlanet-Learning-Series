// <copyright file="UndoTransaction.cs" company="Automate The Planet Ltd.">
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