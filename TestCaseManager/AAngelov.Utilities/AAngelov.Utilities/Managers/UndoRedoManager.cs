// <copyright file="UndoRedoManager.cs" company="CodeProject">
// http://www.codeproject.com/Articles/456591/Simple-Undo-redo-library-for-Csharp-NET?msg=4572235#xx4572235xx All rights reserved.
// </copyright>
// <author>Y Sujan</author>

namespace AAngelov.Utilities.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using AAngelov.Utilities.Contracts;
    using AAngelov.Utilities.Entities;

    /// <summary>
    /// This is a singleton class which stores undo/redo records and executes the undo/redo operations specified in these records
    /// </summary>
    public class UndoRedoManager
    { 
        /// <summary>
        /// Stores instance of this singleton object
        /// </summary>
        private static volatile UndoRedoManager thisObject = new UndoRedoManager();

        /// <summary>
        /// Stores undo records
        /// </summary>
        private List<IUndoRedoRecord> undoStack = new List<IUndoRedoRecord>();
        
        /// <summary>
        /// Stores redo records 
        /// </summary>
        private List<IUndoRedoRecord> redoStack = new List<IUndoRedoRecord>();

        /// <summary>
        /// This is used to determine if an undo operation is going on
        /// </summary>
        private bool isUndoGoingOn = false;

        /// <summary>
        /// This is used to determine if a redo operation is going on
        /// </summary>
        private bool isRedoGoingOn = false;

        /// <summary>
        /// Maximum items to store in undo redo stack
        /// </summary>
        private int maxItems = 10;

        /// <summary>
        /// stores the transaction (if any) under which the current undo/redo operation(s) are occuring.
        /// </summary>
        private UndoTransaction currentTransaction;

        /// <summary>
        /// Delegate used when status of the stack is changed
        /// </summary>
        /// <param name="hasItems">if set to <c>true</c> [has items].</param>
        public delegate void OnStackStatusChanged(bool hasItems);

        /// <summary>
        /// Is fired when the undo stack status is changed
        /// </summary>
        public event OnStackStatusChanged UndoStackStatusChanged;

        /// <summary>
        /// Is fired when the redo stack status is changed
        /// </summary>
        public event OnStackStatusChanged RedoStackStatusChanged;

        /// <summary>
        /// Gets the undo operation count.
        /// </summary>
        /// <value>
        /// The undo operation count.
        /// </value>
        public int UndoOperationCount
        {
            get
            {
                return this.undoStack.Count;
            }
        }

        /// <summary>
        /// Gets the redo operation count.
        /// </summary>
        /// <value>
        /// The redo operation count.
        /// </value>
        public int RedoOperationCount
        {
            get
            {
                return this.redoStack.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [has undo operations].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has undo operations]; otherwise, <c>false</c>.
        /// </value>
        public bool HasUndoOperations
        {
            get
            {
                return this.undoStack.Count != 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [has redo operations].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has redo operations]; otherwise, <c>false</c>.
        /// </value>
        public bool HasRedoOperations
        {
            get
            {
                return this.redoStack.Count != 0;
            }
        }

        /// <summary>
        /// Gets or sets maximum items to be stored in the stack. Note that the change takes effect the next time an item is added to the undo/redo stack
        /// </summary>
        /// <value>
        /// The maximum items.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Max items can't be less or equal to zero</exception>
        public int MaxItems
        {
            get
            {
                return this.maxItems;
            }

            set
            {
                if (this.maxItems <= 0)
                {
                    throw new ArgumentOutOfRangeException("Max items can't be <= 0");
                }

                this.maxItems = value;
            }
        }

        /// <summary>
        /// Returns instance of this singleton object
        /// </summary>
        /// <returns>the UndoRedoManager instance</returns>
        public static UndoRedoManager Instance()
        {
            return thisObject;
        }

        /// <summary>
        /// Starts a transaction under which all undo redo operations take place
        /// </summary>
        /// <param name="undoTransaction">the undo transaction</param>
        public void StartTransaction(UndoTransaction undoTransaction)
        {
            if (this.currentTransaction == null)
            {
                this.currentTransaction = undoTransaction;

                // push an empty undo operation
                this.undoStack.Push(new UndoTransaction(undoTransaction.Name));
                this.redoStack.Push(new UndoTransaction(undoTransaction.Name));
            }
        }

        /// <summary>
        /// Ends the transaction under which all undo/redo operations take place
        /// </summary>
        /// <param name="transaction">the transaction to be ended</param>
        public void EndTransaction(UndoTransaction transaction)
        {
            if (this.currentTransaction == transaction)
            {
                this.currentTransaction = null;

                // now we might have had no items added to undo and redo stack as a part of this transaction. Check empty transaction at top and remove them
                if (this.undoStack.Count > 0)
                {
                    UndoTransaction t = this.undoStack[0] as UndoTransaction;
                    if (t != null && t.OperationsCount == 0)
                    {
                        this.undoStack.Pop();
                    }
                }

                if (this.redoStack.Count > 0)
                {
                    UndoTransaction t = this.redoStack[0] as UndoTransaction;
                    if (t != null && t.OperationsCount == 0)
                    {
                        this.redoStack.Pop();
                    }
                }
            }
        }

        /// <summary>
        /// Pushes an item onto the undo/redo stack. 
        /// 1) If this is called outside the context of a undo/redo operation, the item is added to the undo stack.
        /// 2) If this is called in the context of an undo operation, the item is added to redo stack.
        /// 3) If this is called in context of an redo operation, item is added to undo stack.
        /// </summary>
        /// <typeparam name="T">type of the first parameter of the operation to be performed</typeparam>
        /// <param name="undoOperation">undo operation</param>
        /// <param name="undoData">first parameter of the undo method</param>
        /// <param name="description">description of performed operation</param>
        public void Push<T>(UndoRedoOperation<T> undoOperation, T undoData, string description = "")
        {
            Action eventToFire = this.GetEventToBeFired();
            List<IUndoRedoRecord> stack = this.GetStackToAddTo();

            this.ClearRedoStackIfAddedItemToUndo();

            // If a transaction is going on, add the operation as a entry to the transaction operation
            if (this.currentTransaction == null)
            {
                stack.Push(new UndoRedoRecord<T>(undoOperation, undoData, description));
            }
            else
            {
                (stack[0] as UndoTransaction).AddUndoRedoOperation(new UndoRedoRecord<T>(undoOperation, undoData, description));
            }

            this.TrimStackMaximumExceed(stack);

            // Fire event to inform consumers that the stack size has changed
            eventToFire();
        }

        /// <summary>
        /// Pushes the specified undo operation.
        /// </summary>
        /// <typeparam name="T">type of the first paramater of the operation to be performed</typeparam>
        /// <typeparam name="K">type of the second paramater of the operation to be performed</typeparam>
        /// <param name="undoOperation">The undo operation.</param>
        /// <param name="undoData">The undo data.</param>
        /// <param name="undoData1">The undo data1.</param>
        /// <param name="description">The description.</param>
        public void Push<T, K>(UndoRedoOperation<T, K> undoOperation, T undoData, K undoData1, string description = "")
        {
            Action eventToFire = this.GetEventToBeFired();
            List<IUndoRedoRecord> stack = this.GetStackToAddTo();

            this.ClearRedoStackIfAddedItemToUndo();

            // If a transaction is going on, add the operation as a entry to the transaction operation
            if (this.currentTransaction == null)
            {
                stack.Push(new UndoRedoRecord<T, K>(undoOperation, undoData, undoData1, description));
            }
            else
            {
                (stack[0] as UndoTransaction).AddUndoRedoOperation(new UndoRedoRecord<T, K>(undoOperation, undoData, undoData1, description));
            }

            this.TrimStackMaximumExceed(stack);

            // Fire event to inform consumers that the stack size has changed
            eventToFire();
        }

        /// <summary>
        /// Performs an undo operation
        /// </summary>
        public void Undo()
        {
            try
            {
                this.isUndoGoingOn = true;

                if (this.undoStack.Count == 0)
                {
                    throw new InvalidOperationException("Nothing in the undo stack");
                }
                object currentUndoData = this.undoStack.Pop();

                Type undoDataType = currentUndoData.GetType();

                // If the stored operation was a transaction, perform the undo as a transaction too.
                if (typeof(UndoTransaction).Equals(undoDataType))
                {
                    this.StartTransaction(currentUndoData as UndoTransaction);
                }

                undoDataType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, currentUndoData, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            finally
            {
                this.isUndoGoingOn = false;
                this.EndTransaction(this.currentTransaction);
                this.FireUndoStackStatusChanged();
            }
        }

        /// <summary>
        /// Performs a redo operation
        /// </summary>
        public void Redo()
        {
            try
            {
                this.isRedoGoingOn = true;
                if (this.redoStack.Count == 0)
                {
                    throw new InvalidOperationException("Nothing in the redo stack");
                }
                object currentUndoData = this.redoStack.Pop();

                Type undoDataType = currentUndoData.GetType();

                // If the stored operation was a transaction, perform the redo as a transaction too.
                if (typeof(UndoTransaction).Equals(undoDataType))
                {
                    this.StartTransaction(currentUndoData as UndoTransaction);
                }

                undoDataType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, currentUndoData, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            finally
            {
                this.isRedoGoingOn = false;
                this.EndTransaction(this.currentTransaction);
                this.FireRedoStackStatusChanged();
            }
        }

        /// <summary>
        /// Clears all undo/redo operations from the stack
        /// </summary>
        public void Clear()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
            this.FireUndoStackStatusChanged();
            this.FireRedoStackStatusChanged();
        }

        /// <summary>
        /// Returns a list containing description of all undo stack records
        /// </summary>
        /// <returns>collection of the undo stack information</returns>
        public IList<string> GetUndoStackInformation()
        {
            return this.undoStack.ConvertAll((input) => input.Name == null ? string.Empty : input.Name);
        }

        /// <summary>
        /// Returns a list containing description of all redo stack records
        /// </summary>
        /// <returns>collection of the redo stack information</returns>
        public IList<string> GetRedoStackInformation()
        {
            return this.redoStack.ConvertAll((input) => input.Name == null ? string.Empty : input.Name);
        }

        /// <summary>
        /// Trims the stack maximum exceed.
        /// </summary>
        /// <param name="stack">The stack.</param>
        private void TrimStackMaximumExceed(List<IUndoRedoRecord> stack)
        {
            // If the stack count exceeds maximum allowed items
            if (stack.Count > this.MaxItems)
            {
                object currentObject = stack[stack.Count - 1];
                Trace.TraceInformation("Removing item {0}", currentObject);
                stack.RemoveRange(this.MaxItems - 1, stack.Count - this.MaxItems);
            }
        }

        /// <summary>
        /// Gets the stack automatic add automatic.
        /// </summary>
        /// <returns>collection of undo redo records</returns>
        private List<IUndoRedoRecord> GetStackToAddTo()
        {
            List<IUndoRedoRecord> stack;

            // Determine the stack to which this operation will be added
            if (this.isUndoGoingOn)
            {
                stack = this.redoStack;
            }
            else
            {
                stack = this.undoStack;
            }

            return stack;
        }

        /// <summary>
        /// Gets the event automatic be fired.
        /// </summary>
        /// <returns>the event to be fired</returns>
        private Action GetEventToBeFired()
        {
            Action eventToFire;

            // Determine which stack event will be fired
            if (this.isUndoGoingOn)
            {
                eventToFire = new Action(this.FireRedoStackStatusChanged);
            }
            else
            {
                eventToFire = new Action(this.FireUndoStackStatusChanged);
            }

            return eventToFire;
        }

        /// <summary>
        /// Clears the redo stack difference added item automatic undo.
        /// </summary>
        private void ClearRedoStackIfAddedItemToUndo()
        {
            // if someone added an item to undo stack while there are items in redo stack.. clear the redo stack
            if (!this.isUndoGoingOn && !this.isRedoGoingOn)
            {
                this.redoStack.Clear();
                this.FireRedoStackStatusChanged();
            }
        }

        /// <summary>
        /// Fires the undo stack status changed.
        /// </summary>
        private void FireUndoStackStatusChanged()
        {
            if (null != this.UndoStackStatusChanged)
            {
                this.UndoStackStatusChanged(this.HasUndoOperations);
            }
        }

        /// <summary>
        /// Fires the redo stack status changed.
        /// </summary>
        private void FireRedoStackStatusChanged()
        {
            if (null != this.RedoStackStatusChanged)
            {
                this.RedoStackStatusChanged(this.HasRedoOperations);
            }
        }
    }
}