// <copyright file="MapSecondObject.cs" company="Automate The Planet Ltd.">
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
using System.Runtime.Serialization;

namespace ReducedAutoMapper
{
    [DataContract]
    public class MapSecondObject
    {
        public MapSecondObject()
        {
        }

        [DataMember]
        public string FirstNameS { get; set; }

        [DataMember]
        public string SecondNameS { get; set; }

        [DataMember]
        public string PoNumberS { get; set; }

        [DataMember]
        public decimal PriceS { get; set; }

        [DataMember]
        public ThirdObject ThirdObject1 { get; set; }

        [DataMember]
        public ThirdObject ThirdObject2 { get; set; }

        [DataMember]
        public ThirdObject ThirdObject3 { get; set; }

        [DataMember]
        public ThirdObject ThirdObject4 { get; set; }

        [DataMember]
        public ThirdObject ThirdObject5 { get; set; }

        [DataMember]
        public ThirdObject ThirdObject6 { get; set; }
    }
}