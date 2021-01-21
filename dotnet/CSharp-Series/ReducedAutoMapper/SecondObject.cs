// <copyright file="SecondObject.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReducedAutoMapper
{
    public class SecondObject
    {
        public SecondObject(string firstNameS, string secondNameS, string poNumberS, decimal priceS)
        {
            FirstNameS = firstNameS;
            SecondNameS = secondNameS;
            PoNumberS = poNumberS;
            PriceS = priceS;
            ThirdObject1 = new ThirdObject();
            ThirdObject2 = new ThirdObject();
            ThirdObject3 = new ThirdObject();
            ThirdObject4 = new ThirdObject();
            ThirdObject5 = new ThirdObject();
            ThirdObject6 = new ThirdObject();
        }

        public SecondObject()
        {
        }

        public string FirstNameS { get; set; }
        public string SecondNameS { get; set; }
        public string PoNumberS { get; set; }
        public decimal PriceS { get; set; }
        public ThirdObject ThirdObject1 { get; set; }
        public ThirdObject ThirdObject2 { get; set; }
        public ThirdObject ThirdObject3 { get; set; }
        public ThirdObject ThirdObject4 { get; set; }
        public ThirdObject ThirdObject5 { get; set; }
        public ThirdObject ThirdObject6 { get; set; }
    }
}
