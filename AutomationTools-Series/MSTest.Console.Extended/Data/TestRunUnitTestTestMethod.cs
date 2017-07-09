// <copyright file="TestRunUnitTestTestMethod.cs" company="Automate The Planet Ltd.">
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
    public partial class TestRunUnitTestTestMethod
    {
        private string _codeBaseField;

        private string _adapterTypeNameField;

        private string _classNameField;

        private string _nameField;

        /// <remarks/>
        [XmlAttributeAttribute]
        public string CodeBase
        {
            get
            {
                return _codeBaseField;
            }
            set
            {
                _codeBaseField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string AdapterTypeName
        {
            get
            {
                return _adapterTypeNameField;
            }
            set
            {
                _adapterTypeNameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string ClassName
        {
            get
            {
                return _classNameField;
            }
            set
            {
                _classNameField = value;
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
    }
}