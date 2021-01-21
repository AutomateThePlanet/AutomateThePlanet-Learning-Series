// <copyright file="AssemblyLoader.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace GetAssemblyPropertiesWithoutLockingFile
{
    public class AssemblyLoader : MarshalByRefObject
    {
        private Assembly assembly;
        private static readonly HashAlgorithm cryptoServiceProvider = new SHA1CryptoServiceProvider();

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void LoadAssembly(string path)
        {
            assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
        }

        public List<Test> GetMethodsWithTestMethodAttribute()
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types;
            }
            types = types.Where(t => t != null).ToArray();
            var methods = types.SelectMany(t => t.GetMethods().Where(y =>
            {
                var attributes = y.GetCustomAttributes(true).ToArray();
                if (attributes.Length == 0)
                {
                    return false;
                }
                else
                {
                    var result = false;
                    foreach (var cAttribute in attributes)
                    {
                        result = cAttribute.GetType().FullName.Equals(typeof(TestMethodAttribute).ToString());
                        if (result)
                        {
                            break;
                        }
                    }

                    return result;
                }
            })).ToArray();

            var tests = GetTestsByMethodInfos(methods);

            return tests;
        }

        private static List<Test> GetTestsByMethodInfos(MethodInfo[] methodInfos)
        {
            var tests = new List<Test>();
            foreach (var methodInfo in methodInfos)
            {
                tests.Add(new Test(string.Format("{0}.{1}",
                    methodInfo.DeclaringType.FullName, methodInfo.Name),
                    methodInfo.DeclaringType.Name, 
                    GenerateTestMethodId(methodInfo)));
            }
            return tests;
        }

        private static Guid GenerateTestMethodId(MethodInfo methodInfo)
        {
            var currentNameSpace = methodInfo.DeclaringType.FullName;
            var currentTestMethodShortName = methodInfo.Name;
            var currentTestMethodFullName = string.Concat(currentNameSpace, ".", currentTestMethodShortName);
            var testId = GuidFromString(currentTestMethodFullName);

            return testId;
        }

        private static Guid GuidFromString(string data)
        {
            var hash = cryptoServiceProvider.ComputeHash(Encoding.Unicode.GetBytes(data));

            var toGuid = new byte[16];
            Array.Copy(hash, toGuid, 16);

            return new Guid(toGuid);
        }
    }
}
