// <copyright file="TestRunResultSummaryRunInfosRunInfo.cs" company="Automate The Planet Ltd.">
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
namespace MSTest.Console.Extended.Data
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryRunInfosRunInfo
    {
        private string _textField;

        private string _computerNameField;

        private string _outcomeField;

        private System.DateTime _timestampField;

        /// <remarks/>
        public string Text
        {
            get
            {
                return _textField;
            }
            set
            {
                _textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime Timestamp
        {
            get
            {
                return _timestampField;
            }
            set
            {
                _timestampField = value;
            }
        }
    }
}