// <copyright file="TestRunResultSummaryCounters.cs" company="Automate The Planet Ltd.">
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
using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryCounters
    {
        private int _totalField;

        private int _executedField;

        private int _passedField;

        private int _errorField;

        private int _failedField;

        private int _timeoutField;

        private int _abortedField;

        private int _inconclusiveField;

        private int _passedButRunAbortedField;

        private int _notRunnableField;

        private int _notExecutedField;

        private int _disconnectedField;

        private int _warningField;

        private int _completedField;

        private int _inProgressField;

        private int _pendingField;

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Total
        {
            get
            {
                return _totalField;
            }
            set
            {
                _totalField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Executed
        {
            get
            {
                return _executedField;
            }
            set
            {
                _executedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Passed
        {
            get
            {
                return _passedField;
            }
            set
            {
                _passedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Error
        {
            get
            {
                return _errorField;
            }
            set
            {
                _errorField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Failed
        {
            get
            {
                return _failedField;
            }
            set
            {
                _failedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Timeout
        {
            get
            {
                return _timeoutField;
            }
            set
            {
                _timeoutField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Aborted
        {
            get
            {
                return _abortedField;
            }
            set
            {
                _abortedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Inconclusive
        {
            get
            {
                return _inconclusiveField;
            }
            set
            {
                _inconclusiveField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int PassedButRunAborted
        {
            get
            {
                return _passedButRunAbortedField;
            }
            set
            {
                _passedButRunAbortedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int NotRunnable
        {
            get
            {
                return _notRunnableField;
            }
            set
            {
                _notRunnableField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int NotExecuted
        {
            get
            {
                return _notExecutedField;
            }
            set
            {
                _notExecutedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Disconnected
        {
            get
            {
                return _disconnectedField;
            }
            set
            {
                _disconnectedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Warning
        {
            get
            {
                return _warningField;
            }
            set
            {
                _warningField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Completed
        {
            get
            {
                return _completedField;
            }
            set
            {
                _completedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int InProgress
        {
            get
            {
                return _inProgressField;
            }
            set
            {
                _inProgressField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int Pending
        {
            get
            {
                return _pendingField;
            }
            set
            {
                _pendingField = value;
            }
        }
    }
}