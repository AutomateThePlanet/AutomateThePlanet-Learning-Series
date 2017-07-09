// <copyright file="mstestassert.cs" company="Automate The Planet Ltd.">
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

using System;
using HybridTestFramework.Core.Asserts;
using VSU = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HybridTestFramework.Core.MSTest.Asserts
{
    public class MsTestAssert : IAssert
    {
        public void AreEqual(object expected, object actual)
        {
            VSU.Assert.AreEqual(expected, actual);
        }

        public void AreEqual(object expected, object actual, string message)
        {
            VSU.Assert.AreEqual(expected, actual, message);
        }

        public void AreEqual<T>(T expected, T actual) where T : class
        {
            VSU.Assert.AreEqual(expected, actual);
        }

        public void AreEqual<T>(T expected, T actual, string message) where T : class
        {
            VSU.Assert.AreEqual(expected, actual, message);
        }

        public void AreNotEqual(object expected, object actual)
        {
            VSU.Assert.AreNotEqual(expected, actual);
        }

        public void AreNotEqual(object expected, object actual, string message)
        {
            VSU.Assert.AreNotEqual(expected, actual, message);
        }

        public void AreNotEqual<T>(T expected, T actual) where T : class
        {
            VSU.Assert.AreNotEqual(expected, actual);
        }

        public void AreNotEqual<T>(T expected, T actual, string message) where T : class
        {
            VSU.Assert.AreNotEqual(expected, actual, message);
        }

        public void Fail(string message)
        {
            VSU.Assert.Fail(message);
        }

        public void IsFalse(bool condition)
        {
            VSU.Assert.IsFalse(condition);
        }

        public void IsFalse(bool condition, string message)
        {
            VSU.Assert.IsFalse(condition, message);
        }

        public void IsInstanceOfType(object value, Type expectedType)
        {
            VSU.Assert.IsInstanceOfType(value, expectedType);
        }

        public void IsInstanceOfType(object value, Type expectedType, string message)
        {
            VSU.Assert.IsInstanceOfType(value, expectedType, message);
        }

        public void IsNotNull(object value)
        {
            VSU.Assert.IsNotNull(value);
        }

        public void IsNotNull(object value, string message)
        {
            VSU.Assert.IsNotNull(value, message);
        }

        public void IsNull(object value)
        {
            VSU.Assert.IsNull(value);
        }

        public void IsNull(object value, string message)
        {
            VSU.Assert.IsNull(value, message);
        }

        public void IsTrue(bool condition)
        {
            VSU.Assert.IsTrue(condition);
        }

        public void IsTrue(bool condition, string message)
        {
            VSU.Assert.IsTrue(condition, message);
        }
    }
}