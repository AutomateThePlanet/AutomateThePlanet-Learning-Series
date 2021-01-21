﻿// <copyright file="SpecificationsExtensionMethods.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace AdvancedSpecificationDesignPattern.Specifications.Core
{
    public static class SpecificationsExtensionMethods
    {
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> leftSpecification, ISpecification<TEntity> rightSpecification)
        {
            return new AndSpecification<TEntity>(leftSpecification, rightSpecification);
        }

        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> leftSpecification, ISpecification<TEntity> rightSpecification)
        {
            return new OrSpecification<TEntity>(leftSpecification, rightSpecification);
        }

        public static ISpecification<TEntity> Not<TEntity>(this ISpecification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(specification);
        }
    }
}