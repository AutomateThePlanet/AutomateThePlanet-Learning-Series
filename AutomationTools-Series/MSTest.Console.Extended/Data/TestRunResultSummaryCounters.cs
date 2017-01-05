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
        private int totalField;

        private int executedField;

        private int passedField;

        private int errorField;

        private int failedField;

        private int timeoutField;

        private int abortedField;

        private int inconclusiveField;

        private int passedButRunAbortedField;

        private int notRunnableField;

        private int notExecutedField;

        private int disconnectedField;

        private int warningField;

        private int completedField;

        private int inProgressField;

        private int pendingField;

        /// <remarks/>
        [XmlAttributeAttribute]
        public int total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int executed
        {
            get
            {
                return this.executedField;
            }
            set
            {
                this.executedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int passed
        {
            get
            {
                return this.passedField;
            }
            set
            {
                this.passedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int failed
        {
            get
            {
                return this.failedField;
            }
            set
            {
                this.failedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int timeout
        {
            get
            {
                return this.timeoutField;
            }
            set
            {
                this.timeoutField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int aborted
        {
            get
            {
                return this.abortedField;
            }
            set
            {
                this.abortedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int inconclusive
        {
            get
            {
                return this.inconclusiveField;
            }
            set
            {
                this.inconclusiveField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int passedButRunAborted
        {
            get
            {
                return this.passedButRunAbortedField;
            }
            set
            {
                this.passedButRunAbortedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int notRunnable
        {
            get
            {
                return this.notRunnableField;
            }
            set
            {
                this.notRunnableField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int notExecuted
        {
            get
            {
                return this.notExecutedField;
            }
            set
            {
                this.notExecutedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int disconnected
        {
            get
            {
                return this.disconnectedField;
            }
            set
            {
                this.disconnectedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int warning
        {
            get
            {
                return this.warningField;
            }
            set
            {
                this.warningField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int completed
        {
            get
            {
                return this.completedField;
            }
            set
            {
                this.completedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int inProgress
        {
            get
            {
                return this.inProgressField;
            }
            set
            {
                this.inProgressField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public int pending
        {
            get
            {
                return this.pendingField;
            }
            set
            {
                this.pendingField = value;
            }
        }
    }
}