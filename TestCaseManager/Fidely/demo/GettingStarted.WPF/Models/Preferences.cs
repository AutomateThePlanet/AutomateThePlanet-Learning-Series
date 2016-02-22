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

using Fidely.Framework.Integration;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Fidely.Demo.GettingStarted.WPF.Models
{
    public class Preferences
    {
        public MatchingMode MatchingMode { get; set; }


        public void CopyTo(Preferences dest)
        {
            dest.MatchingMode = MatchingMode;
        }

        public void SaveTo(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer serializer = new XmlSerializer(GetType());
                serializer.Serialize(stream, this);
            }
        }

        public static Preferences LoadFrom(string path)
        {
            if (!File.Exists(path))
            {
                return new Preferences();
            }

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Preferences));
                return (Preferences)serializer.Deserialize(stream);
            }
        }
    }
}
