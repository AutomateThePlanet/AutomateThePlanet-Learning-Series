// <copyright file="ThreadSafeNestedContructorsBaseSingleton.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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
using System.Reflection;

namespace SingletonDesignPattern.Base
{
    public abstract class ThreadSafeNestedContructorsBaseSingleton<T>
    {
        public static T Instance
        {
            get
            {
                return SingletonFactory.Instance;
            }
        }

        internal static class SingletonFactory
        {
            internal static T Instance;

            static SingletonFactory()
            {
                CreateInstance(typeof(T));
            }

            public static T CreateInstance(Type type)
            {
                var ctorsPublic = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

                if (ctorsPublic.Length > 0)
                {
                    throw new Exception(string.Concat(type.FullName, " has one or more public constructors so the property cannot be enforced."));
                }

                var nonPublicConstructor =
                    type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], new ParameterModifier[0]);

                if (nonPublicConstructor == null)
                {
                    throw new Exception(string.Concat(type.FullName, " does not have a private/protected constructor so the property cannot be enforced."));
                }

                try
                {
                    return Instance = (T)nonPublicConstructor.Invoke(new object[0]);
                }
                catch (Exception e)
                {
                    throw new Exception(
                        string.Concat("The Singleton could not be constructed. Check if ", type.FullName, " has a default constructor."), e);
                }
            }
        }
    }
}