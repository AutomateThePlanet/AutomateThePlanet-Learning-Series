// <copyright file="NunitAssert.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.Core.Asserts;
using System;
using NUF = NUnit.Framework;

namespace HybridTestFramework.Core.NUnit.Asserts
{
    public class NunitAssert : IAssert
    {
        public void AreEqual(object expected, object actual)
        {
            NUF.Assert.That(actual, NUF.Is.EqualTo(expected));
        }
        
        public void AreEqual(object expected, object actual, string message)
        {
            NUF.Assert.That(actual, NUF.Is.EqualTo(expected), message);
        }

        public void AreEqual<T>(T expected, T actual) where T : class
        {
            NUF.Assert.That<T>(actual, NUF.Is.EqualTo(expected));
        }

        public void AreEqual<T>(T expected, T actual, string message) where T : class
        {
            NUF.Assert.That<T>(actual, NUF.Is.EqualTo(expected), message);
        }

        public void AreNotEqual(object expected, object actual)
        {
            NUF.Assert.That(actual, NUF.Is.Not.EqualTo(expected));
        }

        public void AreNotEqual(object expected, object actual, string message)
        {
            NUF.Assert.That(actual, NUF.Is.Not.EqualTo(expected), message);
        }

        public void AreNotEqual<T>(T expected, T actual) where T : class
        {
            NUF.Assert.That<T>(actual, NUF.Is.Not.EqualTo(expected));
        }

        public void AreNotEqual<T>(T expected, T actual, string message) where T : class
        {
            NUF.Assert.That<T>(actual, NUF.Is.Not.EqualTo(expected), message);
        }

        public void Fail(string message)
        {
            NUF.Assert.Fail(message);
        }

        public void IsFalse(bool condition)
        {
            NUF.Assert.That(condition, NUF.Is.False);
        }

        public void IsFalse(bool condition, string message)
        {
            NUF.Assert.That(condition, NUF.Is.False, message);
        }

        public void IsInstanceOfType(object value, Type expectedType)
        {
            NUF.Assert.That(value, NUF.Is.TypeOf(expectedType));
        }

        public void IsInstanceOfType(object value, Type expectedType, string message)
        {
            NUF.Assert.That(value, NUF.Is.TypeOf(expectedType), message);
        }

        public void IsNotNull(object value)
        {
            NUF.Assert.That(value, NUF.Is.Not.Null);
        }

        public void IsNotNull(object value, string message)
        {
            NUF.Assert.That(value, NUF.Is.Not.Null, message);
        }

        public void IsNull(object value)
        {
            NUF.Assert.That(value, NUF.Is.Null);
        }

        public void IsNull(object value, string message)
        {
            NUF.Assert.That(value, NUF.Is.Null, message);
        }

        public void IsTrue(bool condition)
        {
            NUF.Assert.That(condition, NUF.Is.True);
        }

        public void IsTrue(bool condition, string message)
        {
            NUF.Assert.That(condition, NUF.Is.True, message);
        }
    }
}