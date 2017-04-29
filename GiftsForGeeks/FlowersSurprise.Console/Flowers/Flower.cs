// <copyright file="Flower.cs" company="Automate The Planet Ltd.">
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
using System;

namespace FlowersSurprise.Console.Flowers
{
    public abstract class Flower
    {
        public Flower(ConsoleColor color, ConsoleKey letter)
        {
            this.Color = color;
            this.Letter = letter;
        }

        public abstract string FlowerPicture { get; }
        public ConsoleColor Color { get; set; }
        public ConsoleKey Letter { get; set; }

        public void Print()
        {
            System.Console.ForegroundColor = this.Color;
            System.Console.WriteLine(this.FlowerPicture);
        }
    }
}