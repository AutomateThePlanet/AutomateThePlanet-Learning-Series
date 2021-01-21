// <copyright file="BaseRegistryManager.cs" company="Automate The Planet Ltd.">
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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsRegistryReadWrite
{
    public class BaseRegistryManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected string MainRegistrySubKey;

        protected void Write(string subKeys, Object value)
        {
            var subKeyNames = subKeys.Split('/');
            var registryKeys = new List<RegistryKey>();
            for (var i = 0; i < subKeyNames.Length; i++)
            {
                var currentRegistryKey = default(RegistryKey);
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

            CloseAllRegistryKeys(registryKeys);
        }

        protected Object Read(string subKeys)
        {
            var result = default(Object);

            try
            {
                var subKeyNames = subKeys.Split('/');
                var registryKeys = new List<RegistryKey>();
                for (var i = 0; i < subKeyNames.Length - 1; i++)
                {
                    var currentRegistryKey = default(RegistryKey);
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

        protected int ReadInt(string subKeys)
        {
            var result = (int)Read(subKeys);

            return result;
        }

        protected double? ReadDouble(string subKeys)
        {
            var obj = Read(subKeys);
            double? result = null;
            if (obj != null)
            {
                result = double.Parse(obj.ToString());
            }

            return result;
        }

        protected bool ReadBool(string subKeys)
        {
            var result = default(bool);
            var resultStr = (string)Read(subKeys);
            if (!string.IsNullOrEmpty(resultStr))
            {
                result = bool.Parse(resultStr);
            }

            return result;
        }

        protected string ReadStr(string subKeys)
        {
            var result = string.Empty;
            var resultStr = (string)Read(subKeys);
            if (!string.IsNullOrEmpty(resultStr))
            {
                result = resultStr;
            }

            return result;
        }

        protected string GenerateMergedKey(params string[] keys)
        {
            var result = MainRegistrySubKey;
            foreach (var currentKey in keys)
            {
                result = string.Join("/", result, currentKey);
            }

            return result;
        }

        private void CloseAllRegistryKeys(List<RegistryKey> registryKeys)
        {
            for (var i = 0; i < registryKeys.Count - 1; i++)
            {
                registryKeys[i].Close();
            }
        }
    }
}
