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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Fidely.Framework.Compilation.Entities.Evaluators
{
    public class PropertyEvaluator<T>
    {
        private static readonly Type[] supportedTypes = new Type[]
        {
            typeof(byte),
            typeof(sbyte),
            typeof(bool),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(Guid),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
        };


        //public PropertyEvaluator()
        //{
        //    var attr = Attribute.GetCustomAttribute(typeof(T), typeof(MetadataTypeAttribute)) as MetadataTypeAttribute;
        //    var type = (attr != null) ? attr.MetadataClassType : typeof(T);

        //    foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        //    {
        //        if (property.CanRead && IsSupportedType(property.PropertyType) && Attribute.GetCustomAttribute(property, typeof(NotEvaluateAttribute)) == null)
        //        {
        //            Register(typeof(T).GetProperty(property.Name, BindingFlags.Instance | BindingFlags.Public));
        //        }
        //    }
        //}


        private bool IsSupportedType(Type type)
        {
            return supportedTypes.Contains(type);
        }
    }
}
