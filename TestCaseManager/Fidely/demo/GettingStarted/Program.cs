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

using Fidely.Framework;
using Fidely.Framework.Compilation.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Fidely.Demo.GettingStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Greet();

            SearchQueryCompiler<Product> compiler = SearchQueryCompilerBuilder.Instance.BuildUpDefaultCompilerForObject<Product>();
            IEnumerable<Product> products = GetProducts();

            while (true)
            {
                Console.Write("$ ");
                string query = Console.ReadLine();
                if (query.ToLower() == "exit")
                {
                    break;
                }

                Expression<Func<Product, bool>> filter = compiler.Compile(query);
                IEnumerable<Product> result = products.AsQueryable().Where(filter);
                PrintProducts(result);
            }
        }

        private static void Greet()
        {
            Console.WriteLine("*** Fidely Demo Application {0} ***", Constants.ProductVersion);
            Console.WriteLine();
            Console.WriteLine("This is a demo application of Fidely search query compilation framework.");
            Console.WriteLine("For more information about Fidely, please refer to http://fidely.codeplex.com.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Usage of this demo application");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            Console.WriteLine("You can search sample Product entities by entering your search query.");
            Console.WriteLine("For example,");
            Console.WriteLine();
            Console.WriteLine("    $ 200 <= ListPrice <= 400");
            Console.WriteLine();
            Console.WriteLine("First of all, press Enter key without entering any characters");
            Console.WriteLine("to display all sample data. After checking sample data, type your");
            Console.WriteLine("search query. You can use following operators:");
            Console.WriteLine();
            Console.WriteLine("    +, -, *, /, =, !=, <, <=, >, >=,");
            Console.WriteLine("    : (partial matching), =: (prefix search) and := (suffix search).");
            Console.WriteLine();
            Console.WriteLine("If you want to quit this application, type 'exit'.");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static IEnumerable<Product> GetProducts()
        {
            return Product.LoadFrom();
        }

        private static void PrintProducts(IEnumerable<Product> products)
        {
            Console.WriteLine("ProductID | Name         | ListPrice | SellStartDate");
            Console.WriteLine("----------+--------------+-----------+---------------");
            foreach (Product product in products)
            {
                Console.WriteLine("{0,9} | {1,-12} | {2,9:#,##0} | {3,-16:yyyy/MM/dd}", product.ProductID, product.Name, product.ListPrice, product.SellStartDate);
            }
            Console.WriteLine();
        }
    }
}
