// <copyright file="ClipBoardManager.cs" company="Automate The Planet Ltd.">
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
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Provides add and retrieve commands from clipboard
    /// </summary>
    /// <typeparam name="T">the object which will be retrieved from clipboard</typeparam>
    public class ClipBoardManager<T> where T : class
    {
        /// <summary>
        /// Gets object from clipboard.
        /// </summary>
        /// <returns>the retrieved object</returns>
        public static T GetFromClipboard()
        {
            T retrievedObj = null;
            IDataObject dataObj = Clipboard.GetDataObject();            
            string format = typeof(T).FullName;
            if (dataObj.GetDataPresent(format))
            {
                retrievedObj = dataObj.GetData(format) as T;                
            }
            return retrievedObj;
        }

        /// <summary>
        /// Copies the suite object to clipboard.
        /// </summary>
        public static void CopyToClipboard(T objectToCopy)
        {
            // register my custom data format with Windows or get it if it's already registered
            DataFormats.Format format = DataFormats.GetFormat(typeof(T).FullName);

            // now copy to clipboard
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, objectToCopy);
            Clipboard.SetDataObject(dataObj, false);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public static void Clear()
        {
            Clipboard.Clear();
        }
    }
}