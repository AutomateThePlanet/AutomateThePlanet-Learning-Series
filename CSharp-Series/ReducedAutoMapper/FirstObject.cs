// <copyright file="FirstObject.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;

namespace ReducedAutoMapper
{
    public class FirstObject
    {
        public FirstObject(string firstName, string secondName, string poNumber, decimal price, DateTime skipDateTime, SecondObject secondObject)
        {
            FirstName = firstName;
            SecondName = secondName;
            PoNumber = poNumber;
            Price = price;
            SkipDateTime = skipDateTime;
            SecondObjectEntity = secondObject;
            SecondObjects = new List<SecondObject>();
        }

        public FirstObject()
        {
        }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string PoNumber { get; set; }

        public decimal Price { get; set; }

        public DateTime SkipDateTime { get; set; }

        public SecondObject SecondObjectEntity { get; set; }

        public List<SecondObject> SecondObjects { get; set; }

        public List<int> IntCollection { get; set; }

        public int[] IntArr { get; set; }

        public SecondObject[] SecondObjectArr { get; set; }
    }
}