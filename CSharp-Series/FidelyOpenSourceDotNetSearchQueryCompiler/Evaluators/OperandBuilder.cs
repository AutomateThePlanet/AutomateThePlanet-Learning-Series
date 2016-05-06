/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Fidely.Framework.Compilation.Evaluators;

namespace Fidely.Framework.Compilation.Objects.Evaluators
{
    internal class OperandBuilder : IOperandBuilder
    {
        private static readonly Type[] NumberTypes = new Type[]
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
        };

        internal Operand BuildUp(Expression instance, PropertyInfo property)
        {
            Logger.Info("Generating an operand with property '{0} : {1}'.", property.Name, property.PropertyType.FullName);

            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type type = property.PropertyType.GetGenericArguments()[0];
                MemberExpression value = Expression.Property(instance, property);
                if (this.IsNumber(type))
                {
                    Logger.Verbose("Generating an operand (type = '{0}').", typeof(decimal).FullName);
                    ConditionalExpression expression = Expression.Condition(Expression.Property(value, property.PropertyType.GetProperty("HasValue")), Expression.Property(value, property.PropertyType.GetProperty("Value")), Expression.Default(type));
                    return new Operand(Expression.Convert(expression, typeof(decimal)), typeof(decimal));
                }
                else if (type == typeof(Guid) || type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan) || type == typeof(string))
                {
                    Logger.Verbose("Generating an operand (type = '{0}').", type.FullName);
                    ConditionalExpression expression = Expression.Condition(Expression.Property(value, property.PropertyType.GetProperty("HasValue")), Expression.Property(value, property.PropertyType.GetProperty("Value")), Expression.Default(type));
                    return new Operand(expression, type);
                }
                else
                {
                    Logger.Verbose("Generating an operand (type = '{0}').", typeof(string).FullName);
                    ConditionalExpression expression = Expression.Condition(Expression.Property(value, property.PropertyType.GetProperty("HasValue")), Expression.Call(Expression.Property(value, property.PropertyType.GetProperty("Value")), typeof(object).GetMethod("ToString")), Expression.Constant(""));
                    return new Operand(expression, typeof(string));
                }
            }
            else
            {
                Type type = property.PropertyType;
                if (this.IsNumber(type))
                {
                    Logger.Verbose("Generating an operand (type = '{0}').", typeof(decimal).FullName);
                    return new Operand(Expression.Convert(Expression.Property(instance, property), typeof(decimal)), typeof(decimal));
                }
                else if (type == typeof(Guid) || type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan))
                {
                    Logger.Verbose("Generating an operand (type = '{0}').", type.FullName);
                    return new Operand(Expression.Property(instance, property), type);
                }
                else
                {
                    Logger.Verbose("Generating an operand (type = '{0}').", typeof(string).FullName);
                    return new Operand(Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) }), Expression.Convert(Expression.Property(instance, property), typeof(object))), typeof(string));
                }
            }
        }

        internal Operand BuildUp(object value)
        {
            Logger.Info("Generating an operand with value '{0}'.", value ?? "null");

            if (value == null)
            {
                return new BlankOperand();
            }

            Type type = value.GetType();
            if (this.IsNumber(type))
            {
                Logger.Verbose("Generating an operand (type = '{0}').", typeof(decimal).FullName);
                return new Operand(Expression.Convert(Expression.Constant(value), typeof(decimal)), typeof(decimal));
            }
            else if (type == typeof(Guid) || type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan) || type == typeof(string))
            {
                Logger.Verbose("Generating an operand (type = '{0}').", type.FullName);
                return new Operand(Expression.Constant(value), type);
            }
            else
            {
                Logger.Verbose("Generating an operand (type = '{0}').", typeof(string).FullName);
                return new Operand(Expression.Call(Expression.Constant(value), typeof(object).GetMethod("ToString")), typeof(string));
            }
        }

        internal bool IsNumber(Type type)
        {
            return NumberTypes.Contains(type);
        }

        Operand IOperandBuilder.BuildUp(object value)
        {
            return this.BuildUp(value);
        }
    }
}