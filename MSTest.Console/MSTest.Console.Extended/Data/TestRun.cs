// <copyright file="TestRun.cs" company="Automate The Planet Ltd.">
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
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
    public partial class TestRun
    {
        private TestRunTestSettings testSettingsField;

        private TestRunTimes timesField;

        private TestRunResultSummary resultSummaryField;

        private TestRunUnitTest[] testDefinitionsField;

        private TestRunTestList[] testListsField;

        private TestRunTestEntry[] testEntriesField;

        private TestRunUnitTestResult[] resultsField;

        private string idField;

        private string nameField;

        private string runUserField;

        /// <remarks/>
        public TestRunTestSettings TestSettings
        {
            get
            {
                return this.testSettingsField;
            }
            set
            {
                this.testSettingsField = value;
            }
        }

        /// <remarks/>
        public TestRunTimes Times
        {
            get
            {
                return this.timesField;
            }
            set
            {
                this.timesField = value;
            }
        }

        /// <remarks/>
        public TestRunResultSummary ResultSummary
        {
            get
            {
                return this.resultSummaryField;
            }
            set
            {
                this.resultSummaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("UnitTest", IsNullable = false)]
        public TestRunUnitTest[] TestDefinitions
        {
            get
            {
                return this.testDefinitionsField;
            }
            set
            {
                this.testDefinitionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TestList", IsNullable = false)]
        public TestRunTestList[] TestLists
        {
            get
            {
                return this.testListsField;
            }
            set
            {
                this.testListsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TestEntry", IsNullable = false)]
        public TestRunTestEntry[] TestEntries
        {
            get
            {
                return this.testEntriesField;
            }
            set
            {
                this.testEntriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("UnitTestResult", IsNullable = false)]
        public TestRunUnitTestResult[] Results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string runUser
        {
            get
            {
                return this.runUserField;
            }
            set
            {
                this.runUserField = value;
            }
        }
    }
}