// <copyright file="SerializationManager.cs" company="Automate The Planet Ltd.">
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
namespace AAngelov.Utilities.Managers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    /// <summary>
    /// Contains help methods to determine if specific obj is serializable
    /// </summary>
    public static class SerializationManager
    {
        /// <summary>
        /// Determines whether the specified object is serializable.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>is the object serializable</returns>
        public static bool IsSerializable(object obj)
        {
            MemoryStream mem = new MemoryStream();
            BinaryFormatter bin = new BinaryFormatter();
            try
            {
                bin.Serialize(mem, obj);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your object cannot be serialized." +
                                " The reason is: " + ex.ToString());
                return false;
            }
        }
    }
}