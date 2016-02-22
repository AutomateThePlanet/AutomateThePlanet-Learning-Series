/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fidely.Framework;
using System.ComponentModel;
using System.IO;

namespace Fidely.Demo.GettingStarted
{
    public class Product
    {
        [Alias("id")]
        [Description("Product ID")]
        public int ProductID { get; set; }

        [Description("Name of the product")]
        public string Name { get; set; }

        [Alias("price")]
        [Description("Selling price")]
        public decimal ListPrice { get; set; }

        [Alias("start")]
        [Description("Date the product was available for sale")]
        public DateTime SellStartDate { get; set; }


        public static IEnumerable<Product> LoadFrom()
        {
            List<Product> products = new List<Product>();

            using (StreamReader reader = new StreamReader(typeof(Product).Assembly.GetManifestResourceStream("Fidely.Demo.GettingStarted.Products.csv")))
            {
                string[] lines = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    string csv = (line.Contains('#')) ? line.Substring(0, line.IndexOf('#')) : line;
                    if (String.IsNullOrWhiteSpace(csv))
                    {
                        continue;
                    }

                    string[] values = csv.Split(',');
                    if (values.Length != 4)
                    {
                        continue;
                    }

                    Product product = new Product();
                    product.ProductID = Int32.Parse(values[0]);
                    product.Name = String.IsNullOrWhiteSpace(values[1]) ? null : values[1].Trim();
                    product.ListPrice = Decimal.Parse(values[2]);
                    product.SellStartDate = DateTime.Parse(values[3]);

                    products.Add(product);
                }
            }

            return products;
        }
    }
}
