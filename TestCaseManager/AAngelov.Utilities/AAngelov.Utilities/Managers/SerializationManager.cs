// <copyright file="SerializationManager.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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