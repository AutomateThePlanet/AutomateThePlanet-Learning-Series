// <copyright file="PropertiesAsserter.cs" company="Automate The Planet Ltd.">
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
using System.Linq.Expressions;
using System.Reflection;
using MSU = Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericPropertiesValidator
{
    public class PropertiesAsserter<K, T> where T : new() where K : new()
    {
        private static readonly string expressionCannotBeNullMessage = "The expression cannot be null.";
        private static readonly string invalidExpressionMessage = "Invalid expression.";
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
            var properties = realObject.GetType().GetProperties();
            foreach (var currentRealProperty in properties)
            {
                if (!propertiesNotToCompare.Contains(currentRealProperty.Name))
                {
                    var currentExpectedProperty = expectedObject.GetType().GetProperty(currentRealProperty.Name);
                    var exceptionMessage =
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
            var properties = realObject.GetType().GetProperties();
            var propertiesNotToCompare = GetMemberNames(propertiesNotToCompareExpressions);
            foreach (var currentRealProperty in properties)
            {
                if (!propertiesNotToCompare.Contains(currentRealProperty.Name))
                {
                    var currentExpectedProperty = expectedObject.GetType().GetProperty(currentRealProperty.Name);
                    var exceptionMessage =
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

        private static List<string> GetMemberNames<T>(params Expression<Func<T, object>>[] expressions)
        {
            var memberNames = new List<string>();
            foreach (var cExpression in expressions)
            {
                memberNames.Add(GetMemberName(cExpression.Body));
            }

            return memberNames;
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(expressionCannotBeNullMessage);
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException(invalidExpressionMessage);
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
    }
}