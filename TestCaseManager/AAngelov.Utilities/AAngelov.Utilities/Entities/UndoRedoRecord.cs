// <copyright file="UndoRedoRecord.cs" company="Automate The Planet Ltd.">
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
    using System.Diagnostics;
    using AAngelov.Utilities.Contracts;

    /// <summary>
    /// undo operation that will be pushed to stack with one parameter
    /// </summary>
    /// <typeparam name="T">first parameter of the method that will be pushed to the stacks</typeparam>
    /// <param name="undoData">The undo data.</param>
    public delegate void UndoRedoOperation<T>(T undoData);

    /// <summary>
    /// undo operation that will be pushed to stack with two parameters
    /// </summary>
    /// <typeparam name="T">first parameter of the method that will be pushed to the stacks</typeparam>
    /// <typeparam name="K">second parameter of the method that will be pushed to the stacks</typeparam>
    /// <param name="undoData">The undo data.</param>
    /// <param name="undoData1">The undo data1.</param>
    public delegate void UndoRedoOperation<T, K>(T undoData, K undoData1);

    /// <summary>
    /// Contains information about an undo or redo record
    /// </summary>
    /// <typeparam name="T">first parameter of the method that will be pushed to the stacks</typeparam>
    public class UndoRedoRecord<T> : IUndoRedoRecord
    {
        /// <summary>
        /// The _operation
        /// </summary>
        private UndoRedoOperation<T> operation;

        /// <summary>
        /// The _undo data
        /// </summary>
        private T undoData;

        /// <summary>
        /// The _description
        /// </summary>
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoRecord{T}"/> class.
        /// </summary>
        public UndoRedoRecord()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoRecord{T}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="undoData">The undo data.</param>
        /// <param name="description">The description.</param>
        public UndoRedoRecord(UndoRedoOperation<T> operation, T undoData, string description = "")
        {
            this.SetInfo(operation, undoData, description);
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
                return this.description;
            }
        }

        /// <summary>
        /// Sets the information.
        /// </summary>
        /// <param name="operationToSet">The operation.</param>
        /// <param name="undoDataToSet">The undo data.</param>
        /// <param name="descriptionToSet">The description.</param>
        public void SetInfo(UndoRedoOperation<T> operationToSet, T undoDataToSet, string descriptionToSet = "")
        {
            this.operation = operationToSet;
            this.undoData = undoDataToSet;
            this.description = descriptionToSet;
        }

        /// <summary>
        /// Executes the operation saved in the stack.
        /// </summary>
        public void Execute()
        {
            Trace.TraceInformation("Undo/redo operation {0} with data {1} - {2}", this.operation, this.undoData, this.description);
            this.operation(this.undoData);
        }
    }

    /// <summary>
    /// Contains information about an undo or redo record
    /// </summary>
    /// <typeparam name="T">first parameter of the method that will be pushed to the stacks</typeparam>
    /// <typeparam name="K">second parameter of the method that will be pushed to the stacks</typeparam>
    public class UndoRedoRecord<T, K> : IUndoRedoRecord
    {
        /// <summary>
        /// The _operation
        /// </summary>
        private UndoRedoOperation<T, K> operation;

        /// <summary>
        /// The _undo data
        /// </summary>
        private T undoData;

        /// <summary>
        /// The _undo data1
        /// </summary>
        private K undoData1;

        /// <summary>
        /// The _description
        /// </summary>
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoRecord{T, K}"/> class.
        /// </summary>
        public UndoRedoRecord()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoRecord{T, K}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="undoData">The undo data.</param>
        /// <param name="undoData1">The undo data1.</param>
        /// <param name="description">The description.</param>
        public UndoRedoRecord(UndoRedoOperation<T, K> operation, T undoData, K undoData1, string description = "")
        {
            this.SetInfo(operation, undoData, undoData1, description);
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
                return this.description;
            }
        }

        /// <summary>
        /// Sets the information.
        /// </summary>
        /// <param name="operationToSet">The operation.</param>
        /// <param name="undoDataToSet">The undo data.</param>
        /// <param name="undoData1ToSet">The undo data1.</param>
        /// <param name="descriptionToSet">The description.</param>
        public void SetInfo(UndoRedoOperation<T, K> operationToSet, T undoDataToSet, K undoData1ToSet, string descriptionToSet = "")
        {
            this.operation = operationToSet;
            this.undoData = undoDataToSet;
            this.undoData1 = undoData1ToSet;
            this.description = descriptionToSet;
        }

        /// <summary>
        /// Executes the operation saved in the stack.
        /// </summary>
        public void Execute()
        {
            Trace.TraceInformation("Undo/redo operation {0} with data {1} - {2}", this.operation, this.undoData, this.undoData1, this.description);
            this.operation(this.undoData, this.undoData1);
        }
    }
}