// <copyright file="OrSpecification.cs" company="Automate The Planet Ltd.">
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

namespace AdvancedSpecificationDesignPattern.Specifications.Core
{
    public class OrSpecification<TEntity> : Specification<TEntity>
    {
        private readonly ISpecification<TEntity> _leftSpecification;
        private readonly ISpecification<TEntity> _rightSpecification;

        public OrSpecification(ISpecification<TEntity> leftSpecification, ISpecification<TEntity> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return _leftSpecification.IsSatisfiedBy(entity) || _rightSpecification.IsSatisfiedBy(entity);
        }
    }
}