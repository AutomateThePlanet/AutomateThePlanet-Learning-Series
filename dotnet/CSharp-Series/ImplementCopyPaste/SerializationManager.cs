// <copyright file="SerializationManager.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImplementCopyPaste
{
    public static class SerializationManager
    {
        public static bool IsSerializable(object obj)
        {
            var mem = new MemoryStream();
            var bin = new BinaryFormatter();
            try
            {
                bin.Serialize(mem, obj);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("Your object cannot be serialized.", " The reason is: ", ex.ToString()));
                return false;
            }
        }
    }
}
