// <copyright file="TestRunUnitTest.cs" company="Automate The Planet Ltd.">
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
    public partial class TestRunUnitTest
    {
        private TestRunUnitTestExecution _executionField;

        private TestRunUnitTestTestMethod _testMethodField;

        private TestRunUnitTestOwners _ownersField;

        private TestRunUnitTestTestCategory _testCategoryField;

        private string _nameField;

        private string _storageField;

        private string _idField;

        /// <remarks/>
        public TestRunUnitTestOwners Owners
        {
            get
            {
                return _ownersField;
            }
            set
            {
                _ownersField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestTestCategory TestCategory
        {
            get
            {
                return _testCategoryField;
            }
            set
            {
                _testCategoryField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestExecution Execution
        {
            get
            {
                return _executionField;
            }
            set
            {
                _executionField = value;
            }
        }

        /// <remarks/>
        public TestRunUnitTestTestMethod TestMethod
        {
            get
            {
                return _testMethodField;
            }
            set
            {
                _testMethodField = value;
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
        public string Storage
        {
            get
            {
                return _storageField;
            }
            set
            {
                _storageField = value;
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
    }
}