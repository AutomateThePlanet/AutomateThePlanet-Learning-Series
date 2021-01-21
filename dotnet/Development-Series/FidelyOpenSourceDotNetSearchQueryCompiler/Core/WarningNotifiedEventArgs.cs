/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Fidely.Framework
{
    /// <summary>
    /// Provides data for the WarningNotified event of the search query compiler.
    /// </summary>
    public class WarningNotifiedEventArgs : EventArgs
    {
        internal WarningNotifiedEventArgs(Type notifiedBy, string symbol, string message)
        {
            NotifiedBy = notifiedBy;
            Symbol = symbol;
            Message = message;
        }

        /// <summary>
        /// The type of the instance that notified warning.
        /// </summary>
        public Type NotifiedBy { get; private set; }

        /// <summary>
        /// The symbol that represents the instance that notified warning.
        /// </summary>
        public string Symbol { get; private set; }

        /// <summary>
        /// The warning message.
        /// </summary>
        public string Message { get; private set; }
    }
}