// <copyright file="TestRunResultSummary.cs" company="Automate The Planet Ltd.">
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
    public partial class TestRunResultSummary
    {
        private TestRunResultSummaryCounters _countersField;

        private TestRunResultSummaryOutput _outputField;

        private TestRunResultSummaryRunInfos _runInfosField;

        private string _outcomeField;

        /// <remarks/>
        public TestRunResultSummaryCounters Counters
        {
            get
            {
                return _countersField;
            }
            set
            {
                _countersField = value;
            }
        }

        /// <remarks/>
        public TestRunResultSummaryOutput Output
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

        /// <remarks/>
        public TestRunResultSummaryRunInfos RunInfos
        {
            get
            {
                return _runInfosField;
            }
            set
            {
                _runInfosField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
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
    }
}