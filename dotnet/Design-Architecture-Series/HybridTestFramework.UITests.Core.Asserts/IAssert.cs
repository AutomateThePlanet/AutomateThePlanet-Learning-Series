﻿// <copyright file="IAssert.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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

namespace HybridTestFramework.Core.Asserts
{
    public interface IAssert
    {
        void AreEqual(object expected, object actual);

        void AreEqual(object expected, object actual, string message);

        void AreEqual<T>(T expected, T actual) where T : class;

        void AreEqual<T>(T expected, T actual, string message) where T : class;

        void AreNotEqual(object expected, object actual);

        void AreNotEqual(object expected, object actual, string message);

        void AreNotEqual<T>(T expected, T actual) where T : class;

        void AreNotEqual<T>(T expected, T actual, string message) where T : class;

        void IsFalse(bool condition);

        void IsFalse(bool condition, string message);

        void IsTrue(bool condition);

        void IsTrue(bool condition, string message);
        
        void IsNull(object value);

        void IsNull(object value, string message);

        void IsNotNull(object value);

        void IsNotNull(object value, string message);

        void Fail(string message);

        void IsInstanceOfType(object value, Type expectedType);

        void IsInstanceOfType(object value, Type expectedType, string message);
    }
}