// <copyright file="TestRunUnitTestDataDrivenResults.cs" company="Automate The Planet Ltd.">
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
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MSTest.Console.Extended.Data
{
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestDataDrivenResults
    {
        private TestRunUnitTestResultOutput _outputField;

        private string _executionIdField;

        private string _testIdField;

        private string _testNameField;

        private string _computerNameField;

        private TimeSpan _durationField;

        private DateTime _startTimeField;

        private DateTime _endTimeField;

        private string _testTypeField;

        private string _outcomeField;

        private string _testListIdField;

        private string _relativeResultsDirectoryField;
    
        public TestRunUnitTestResultOutput Output
        {
            get
            {
                return _outputField;
            }
            set
            {
                _outputField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string ExecutionId
        {
            get
            {
                return _executionIdField;
            }
            set
            {
                _executionIdField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string TestId
        {
            get
            {
                return _testIdField;
            }
            set
            {
                _testIdField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string TestName
        {
            get
            {
                return _testNameField;
            }
            set
            {
                _testNameField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string ComputerName
        {
            get
            {
                return _computerNameField;
            }
            set
            {
                _computerNameField = value;
            }
        }

        [XmlIgnore]
        public TimeSpan Duration
        {
            get
            {
                return _durationField;
            }
            set
            {
                _durationField = value;
            }
        }

        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlAttributeAttribute(DataType = "duration", AttributeName = "duration")]
        public string DurationString
        {
            get
            {
                return Duration.ToString();
            }
            set
            {
                Duration = string.IsNullOrEmpty(value) ? TimeSpan.Zero : TimeSpan.Parse(value);
            }
        }
    
        [XmlAttributeAttribute]
        public DateTime StartTime
        {
            get
            {
                return _startTimeField;
            }
            set
            {
                _startTimeField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public DateTime EndTime
        {
            get
            {
                return _endTimeField;
            }
            set
            {
                _endTimeField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string TestType
        {
            get
            {
                return _testTypeField;
            }
            set
            {
                _testTypeField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string Outcome
        {
            get
            {
                return _outcomeField;
            }
            set
            {
                _outcomeField = value;
            }
        }
    
        [XmlAttributeAttribute]
        public string TestListId
        {
            get
            {
                return _testListIdField;
            }
            set
            {
                _testListIdField = value;
            }
        }

        [XmlAttributeAttribute]
        public string RelativeResultsDirectory
        {
            get
            {
                return _relativeResultsDirectoryField;
            }
            set
            {
                _relativeResultsDirectoryField = value;
            }
        }
    }
}