// <copyright file="PropertiesAsserter.cs" company="Automate The Planet Ltd.">
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
using System.Linq.Expressions;
using CSharp.Series.Tests.PropertiesAsLambdas;
using MSU = Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace CSharp.Series.Tests.PropertiesAsserter
{
    public class PropertiesAsserter<K, T> where T : new() where K : new()
    {
        private static K instance;

        public static K Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new K();
                }
                return instance;
            }
        }

        public void Assert(T expectedObject, T realObject, params string[] propertiesNotToCompare)
        {
            PropertyInfo[] properties = realObject.GetType().GetProperties();
            foreach (PropertyInfo currentRealProperty in properties)
            {
                if (!propertiesNotToCompare.Contains(currentRealProperty.Name))
                {
                    PropertyInfo currentExpectedProperty = expectedObject.GetType().GetProperty(currentRealProperty.Name);
                    string exceptionMessage =
                        string.Format("The property {0} of class {1} was not as expected.", currentRealProperty.Name, currentRealProperty.DeclaringType.Name);

                    if (currentRealProperty.PropertyType != typeof(DateTime) && currentRealProperty.PropertyType != typeof(DateTime?))
                    {
                        MSU.Assert.AreEqual(currentExpectedProperty.GetValue(expectedObject, null), currentRealProperty.GetValue(realObject, null), exceptionMessage);
                    }
                    else
                    {
                        DateTimeAssert.AreEqual(
                            currentExpectedProperty.GetValue(expectedObject, null) as DateTime?,
                            currentRealProperty.GetValue(realObject, null) as DateTime?,
                            DateTimeDeltaType.Minutes,
                            5);
                    }
                }
            }
        }

        public void Assert<T>(T expectedObject, T realObject, params Expression<Func<T, object>>[] propertiesNotToCompareExpressions)
        {
            PropertyInfo[] properties = realObject.GetType().GetProperties();
            List<string> propertiesNotToCompare = expectedObject.GetMemberNames(propertiesNotToCompareExpressions);
            foreach (PropertyInfo currentRealProperty in properties)
            {
                if (!propertiesNotToCompare.Contains(currentRealProperty.Name))
                {
                    PropertyInfo currentExpectedProperty = expectedObject.GetType().GetProperty(currentRealProperty.Name);
                    string exceptionMessage =
                        string.Format("The property {0} of class {1} was not as expected.", currentRealProperty.Name, currentRealProperty.DeclaringType.Name);

                    if (currentRealProperty.PropertyType != typeof(DateTime) && currentRealProperty.PropertyType != typeof(DateTime?))
                    {
                        MSU.Assert.AreEqual(currentExpectedProperty.GetValue(expectedObject, null), currentRealProperty.GetValue(realObject, null), exceptionMessage);
                    }
                    else
                    {
                        DateTimeAssert.AreEqual(
                            currentExpectedProperty.GetValue(expectedObject, null) as DateTime?,
                            currentRealProperty.GetValue(realObject, null) as DateTime?,
                            DateTimeDeltaType.Minutes,
                            5);
                    }
                }
            }
        }
    }
}