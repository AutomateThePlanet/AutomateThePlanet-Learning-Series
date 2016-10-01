// <copyright file="Assert.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core.Utilities;
using Microsoft.Practices.Unity;
using System;

namespace HybridTestFramework.Core.Asserts
{
    public static class Assert
    {
        private static readonly IAssert assert;

        static Assert()
        {
            assert = UnityContainerFactory.GetContainer().Resolve<IAssert>();
        }

        public static void AreEqual(object expected, object actual)
        {
            assert.AreEqual(expected, actual);
        }

        public static void AreEqual(object expected, object actual, string message)
        {
            assert.AreEqual(expected, actual, message);
        }

        public static void AreEqual<T>(T expected, T actual) where T : class
        {
            assert.AreEqual<T>(expected, actual);
        }

        public static void AreEqual<T>(T expected, T actual, string message) where T : class
        {
            assert.AreEqual<T>(expected, actual, message);
        }

        public static void AreNotEqual(object expected, object actual)
        {
            assert.AreNotEqual(expected, actual);
        }

        public static void AreNotEqual(object expected, object actual, string message)
        {
            assert.AreNotEqual(expected, actual, message);
        }

        public static void AreNotEqual<T>(T expected, T actual) where T : class
        {
            assert.AreNotEqual<T>(expected, actual);
        }

        public static void AreNotEqual<T>(T expected, T actual, string message) where T : class
        {
            assert.AreNotEqual<T>(expected, actual, message);
        }

        public static void IsFalse(bool condition)
        {
            assert.IsFalse(condition);
        }

        public static void IsFalse(bool condition, string message)
        {
            assert.IsFalse(condition, message);
        }

        public static void IsTrue(bool condition)
        {
            assert.IsTrue(condition);
        }

        public static void IsTrue(bool condition, string message)
        {
            assert.IsTrue(condition, message);
        }

        public static void IsNull(object value)
        {
            assert.IsNull(value);
        }

        public static void IsNull(object value, string message)
        {
            assert.IsNull(value, message);
        }

        public static void IsNotNull(object value)
        {
            assert.IsNotNull(value);
        }

        public static void IsNotNull(object value, string message)
        {
            assert.IsNotNull(value, message);
        }

        public static void Fail(string message)
        {
            assert.Fail(message);
        }

        public static void IsInstanceOfType(object value, Type expectedType)
        {
            assert.IsInstanceOfType(value, expectedType);
        }

        public static void IsInstanceOfType(object value, Type expectedType, string message)
        {
            assert.IsInstanceOfType(value, expectedType, message);
        }
    }
}