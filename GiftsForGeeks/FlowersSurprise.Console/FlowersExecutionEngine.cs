// <copyright file="FlowerExecutionEngine.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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

using FlowersSurprise.Console.Flowers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowersSurprise.Console
{
    public static class FlowersExecutionEngine
    {
        public static void Execute(List<Flower> flowers)
        {
            var key = new ConsoleKeyInfo();

            while (!System.Console.KeyAvailable && key.Key != ConsoleKey.Escape)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine();
                System.Console.Write(value: "Enter a letter part of your name (Qwert) (quit = ESC): ");
                key = System.Console.ReadKey(intercept: true);
                Flower flowerToBePrinted = flowers.FirstOrDefault(f => f.Letter == key.Key);
                flowerToBePrinted?.Print();
            }
        }
    }
}
