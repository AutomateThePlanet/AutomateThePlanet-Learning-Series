// <copyright file="TestRunTimes.cs" company="Automate The Planet Ltd.">
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
    public partial class TestRunTimes
    {
        private System.DateTime _creationField;

        private System.DateTime _queuingField;

        private System.DateTime _startField;

        private System.DateTime _finishField;

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime Creation
        {
            get
            {
                return _creationField;
            }
            set
            {
                _creationField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime Queuing
        {
            get
            {
                return _queuingField;
            }
            set
            {
                _queuingField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime Start
        {
            get
            {
                return _startField;
            }
            set
            {
                _startField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public System.DateTime Finish
        {
            get
            {
                return _finishField;
            }
            set
            {
                _finishField = value;
            }
        }
    }
}