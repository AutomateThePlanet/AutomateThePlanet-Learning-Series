﻿// <copyright file="Order.cs" company="Automate The Planet Ltd.">
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
namespace WebDriver.Series.Tests
{
    public class Order
    {
        public Order(string shipName) : this()
        {
            if (!string.IsNullOrEmpty(shipName))
            {
                this.ShipName = shipName;
            }            
        }

        public Order()
        {
            Random rand = new Random();
            this.OrderId = rand.Next();
            this.ShipName = Guid.NewGuid().ToString();
            this.Freight = rand.Next();
            this.OrderDate = DateTime.Now;
        }

        public int OrderId { get; set; }
        public string ShipName { get; set; }
        public double Freight { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
