// <copyright file="ClipBoardManager.cs" company="CodePlex">
// https://aangelov.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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