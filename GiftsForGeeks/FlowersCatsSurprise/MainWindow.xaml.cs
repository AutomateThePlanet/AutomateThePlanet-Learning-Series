// <copyright file="MainWindow.xaml.cs" company="Automate The Planet Ltd.">
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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Input;


namespace FlowersCatsSurprise
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bitmap> flowers = new List<Bitmap>();
        List<Bitmap> cats = new List<Bitmap>();
        Random randCat = new Random();
        Random randFlower = new Random();
        bool canBeClickedAgain = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (canBeClickedAgain)
            {
                canBeClickedAgain = false;
                int randCatIndex = randCat.Next(0, cats.Count - 1);
                WallpaperChangingService.Set(cats[randCatIndex], WallpaperStyle.Centered);
                canBeClickedAgain = true;
            }
        }

        private void Flower_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (canBeClickedAgain)
            {
                canBeClickedAgain = false;
                int randFlowerIndex = randFlower.Next(0, flowers.Count - 1);
                WallpaperChangingService.Set(flowers[randFlowerIndex], WallpaperStyle.Centered);
                canBeClickedAgain = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {

                string resourceKey = entry.Key.ToString();
                object resource = entry.Value;
                if (resourceKey.Contains("cat"))
                {
                    cats.Add(resource as Bitmap);
                }
                else if (resourceKey.Contains("flowers"))
                {
                    flowers.Add(resource as Bitmap);
                }
            }
        }
    }
}
