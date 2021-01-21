// <copyright file="ReducedAutoMapper.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Reflection;

namespace ReducedAutoMapper
{
    public class ReducedAutoMapper
    {
        private static ReducedAutoMapper instance;

        private Dictionary<object, object> mappingTypes;

        public ReducedAutoMapper()
        {
            MappingTypes = new Dictionary<object, object>();
        }

        public static ReducedAutoMapper Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReducedAutoMapper();
                }
                return instance;
            }
        }

        public Dictionary<object, object> MappingTypes
        {
            get
            {
                return mappingTypes;
            }
            set
            {
                mappingTypes = value;
            }
        }

        public void CreateMap<TSource, TDestination>()
            where TSource : new()
            where TDestination : new()
        {
            if (!MappingTypes.ContainsKey(typeof(TSource)))
            {
                MappingTypes.Add(typeof(TSource), typeof(TDestination));
            }
        }

        public TDestination Map<TSource, TDestination>(
            TSource realObject,
            TDestination dtoObject = default (TDestination),
            Dictionary<object, object> alreadyInitializedObjects = null,
            bool shouldMapInnerEntities = true)
            where TSource : class, new()
            where TDestination : class, new()
        {
            if (realObject == null)
            {
                return null;
            }
            if (alreadyInitializedObjects == null)
            {
                alreadyInitializedObjects = new Dictionary<object, object>();
            }
            if (dtoObject == null)
            {
                dtoObject = new TDestination();
            }

            var realObjectType = realObject.GetType();
            var properties = realObjectType.GetProperties();
            foreach (var currentRealProperty in properties)
            {
                var currentDtoProperty = dtoObject.GetType().GetProperty(currentRealProperty.Name);
                if (currentDtoProperty == null)
                {
                    ////Debug.WriteLine("The property {0} was not found in the DTO object in order to be mapped. Because of that we skip to map it.", currentRealProperty.Name);
                }
                else
                {
                    if (MappingTypes.ContainsKey(currentRealProperty.PropertyType) && shouldMapInnerEntities)
                    {
                        var mapToObject = mappingTypes[currentRealProperty.PropertyType];
                        var types = new Type[] { currentRealProperty.PropertyType, (Type)mapToObject };
                        var method = GetType().GetMethod("Map").MakeGenericMethod(types);
                        var realObjectPropertyValue = currentRealProperty.GetValue(realObject, null);
                        var objects = new object[]
                        {
                            realObjectPropertyValue,
                            null,
                            alreadyInitializedObjects,
                            shouldMapInnerEntities
                        };
                        if (objects != null && realObjectPropertyValue != null)
                        {
                            if (alreadyInitializedObjects.ContainsKey(realObjectPropertyValue) && currentDtoProperty.CanWrite)
                            {
                                // Set the cached version of the same object (optimization)
                                currentDtoProperty.SetValue(dtoObject, alreadyInitializedObjects[realObjectPropertyValue]);
                            }
                            else
                            {
                                // Add the object to cached objects collection.
                                alreadyInitializedObjects.Add(realObjectPropertyValue, null);
                                // Recursively call Map method again to get the new proxy object.
                                var newProxyProperty = method.Invoke(this, objects);
                                if (currentDtoProperty.CanWrite)
                                {
                                    currentDtoProperty.SetValue(dtoObject, newProxyProperty);
                                }

                                if (alreadyInitializedObjects.ContainsKey(realObjectPropertyValue) && alreadyInitializedObjects[realObjectPropertyValue] == null)
                                {
                                    alreadyInitializedObjects[realObjectPropertyValue] = newProxyProperty;
                                }
                            }
                        }
                        else if (realObjectPropertyValue == null && currentDtoProperty.CanWrite)
                        {
                            // If the original value of the object was null set null to the destination property.
                            currentDtoProperty.SetValue(dtoObject, null);
                        }
                    }
                    else if (!MappingTypes.ContainsKey(currentRealProperty.PropertyType))
                    {
                        // If the property is not custom type just set normally the value.
                        if (currentDtoProperty.CanWrite)
                        {
                            currentDtoProperty.SetValue(dtoObject, currentRealProperty.GetValue(realObject, null));
                        }
                    }
                }
            }

            return dtoObject;
        }
    }
}