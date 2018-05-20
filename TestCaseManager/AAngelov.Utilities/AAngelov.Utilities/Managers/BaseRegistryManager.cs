// <copyright file="BaseRegistryManager.cs" company="Automate The Planet Ltd.">
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
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Win32;

    /// <summary>
    /// Contains helper methods for saving and reading specific app related information from Windows Registry
    /// </summary>
    public class BaseRegistryManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main registry sub key name
        /// </summary>
        protected string MainRegistrySubKey;

        /// <summary>
        /// Writes the specified sub key name.
        /// </summary>
        /// <param name="subKeys">Name of the sub key.</param>
        /// <param name="value">The value.</param>
        protected void Write(string subKeys, object value)
        {
            string[] subKeyNames = subKeys.Split('/');
            List<RegistryKey> registryKeys = new List<RegistryKey>();
            for (int i = 0; i < subKeyNames.Length; i++)
            {
                RegistryKey currentRegistryKey = default(RegistryKey);
                if (i == 0)
                {
                    currentRegistryKey = Registry.CurrentUser.CreateSubKey(subKeyNames[i]);
                }
                else if (i < subKeyNames.Length - 1)
                {
                    currentRegistryKey = registryKeys[i - 1].CreateSubKey(subKeyNames[i]);
                }
                else
                {
                    registryKeys.Last().SetValue(subKeyNames.Last(), value);
                }
                registryKeys.Add(currentRegistryKey);
            }

            this.CloseAllRegistryKeys(registryKeys);
        }

        /// <summary>
        /// Reads the specified sub key.
        /// </summary>
        /// <param name="subKeys">The sub key.</param>
        /// <returns></returns>
        protected object Read(string subKeys)
        {
            var result = default(object);

            try
            {
                string[] subKeyNames = subKeys.Split('/');
                List<RegistryKey> registryKeys = new List<RegistryKey>();
                for (int i = 0; i < subKeyNames.Length - 1; i++)
                {
                    RegistryKey currentRegistryKey = default(RegistryKey);
                    if (i == 0)
                    {
                        currentRegistryKey = Registry.CurrentUser.OpenSubKey(subKeyNames[i]);
                    }
                    else
                    {
                        currentRegistryKey = registryKeys[i - 1].OpenSubKey(subKeyNames[i]);
                    }
                    registryKeys.Add(currentRegistryKey);
                    if (registryKeys.Last() != null && subKeyNames.Last() != null)
                    {
                        result = registryKeys.Last().GetValue(subKeyNames.Last());
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return result;
        }

        /// <summary>
        /// Reads the int.
        /// </summary>
        /// <param name="subKeys">The sub keys.</param>
        /// <returns>the integer read from registry</returns>
        protected int ReadInt(string subKeys)
        {
            int result = (int)this.Read(subKeys);

            return result;
        }

        /// <summary>
        /// Reads the double.
        /// </summary>
        /// <param name="subKeys">The sub keys.</param>
        /// <returns>the double value</returns>
        protected double? ReadDouble(string subKeys)
        {
            object obj = this.Read(subKeys);
            double? result = null;
            if(obj != null)
            {
                result = double.Parse(obj.ToString());
            }           

            return result;
        }

        /// <summary>
        /// Reads the bool.
        /// </summary>
        /// <param name="subKeys">The sub keys.</param>
        /// <returns>the boolean read from registry</returns>
        protected bool ReadBool(string subKeys)
        {
            bool result = default(bool);
            string resultStr = (string)this.Read(subKeys);
            if (!string.IsNullOrEmpty(resultStr))
            {
                result = bool.Parse(resultStr);
            }

            return result;
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="subKeys">The sub keys.</param>
        /// <returns>the string read from registry</returns>
        protected string ReadStr(string subKeys)
        {
            string result = string.Empty;
            string resultStr = (string)this.Read(subKeys);
            if (!string.IsNullOrEmpty(resultStr))
            {
                result = resultStr;
            }

            return result;
        }

        /// <summary>
        /// Gets the merged keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>return merged sub key</returns>
        protected string GenerateMergedKey(params string[] keys)
        {
            string result = this.MainRegistrySubKey;
            foreach (var currentKey in keys)
            {
                result = string.Join("/", result, currentKey);                
            }

            return result;
        }

        /// <summary>
        /// Closes all registry keys.
        /// </summary>
        /// <param name="registryKeys">The registry keys.</param>
        private void CloseAllRegistryKeys(List<RegistryKey> registryKeys)
        {
            for (int i = 0; i < registryKeys.Count - 1; i++)
            {
                registryKeys[i].Close();
            }
        }
    }
}