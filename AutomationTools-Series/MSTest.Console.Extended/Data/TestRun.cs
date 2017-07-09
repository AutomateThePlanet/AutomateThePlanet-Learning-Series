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
    [XmlRootAttribute(Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
    public partial class TestRun
    {
        private TestRunTestSettings _testSettingsField;

        private TestRunTimes _timesField;

        private TestRunResultSummary _resultSummaryField;

        private TestRunUnitTest[] _testDefinitionsField;

        private TestRunTestList[] _testListsField;

        private TestRunTestEntry[] _testEntriesField;

        private TestRunUnitTestResult[] _resultsField;

        private string _idField;

        private string _nameField;

        private string _runUserField;

        /// <remarks/>
        public TestRunTestSettings TestSettings
        {
            get
            {
                return _testSettingsField;
            }
            set
            {
                _testSettingsField = value;
            }
        }

        /// <remarks/>
        public TestRunTimes Times
        {
            get
            {
                return _timesField;
            }
            set
            {
                _timesField = value;
            }
        }

        /// <remarks/>
        public TestRunResultSummary ResultSummary
        {
            get
            {
                return _resultSummaryField;
            }
            set
            {
                _resultSummaryField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("UnitTest", IsNullable = false)]
        public TestRunUnitTest[] TestDefinitions
        {
            get
            {
                return _testDefinitionsField;
            }
            set
            {
                _testDefinitionsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("TestList", IsNullable = false)]
        public TestRunTestList[] TestLists
        {
            get
            {
                return _testListsField;
            }
            set
            {
                _testListsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("TestEntry", IsNullable = false)]
        public TestRunTestEntry[] TestEntries
        {
            get
            {
                return _testEntriesField;
            }
            set
            {
                _testEntriesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("UnitTestResult", IsNullable = false)]
        public TestRunUnitTestResult[] Results
        {
            get
            {
                return _resultsField;
            }
            set
            {
                _resultsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string Id
        {
            get
            {
                return _idField;
            }
            set
            {
                _idField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string Name
        {
            get
            {
                return _nameField;
            }
            set
            {
                _nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string RunUser
        {
            get
            {
                return _runUserField;
            }
            set
            {
                _runUserField = value;
            }
        }
    }
}