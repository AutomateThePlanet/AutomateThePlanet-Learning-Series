// <copyright file="BehaviorEngine.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

using AdvancedBehavioursDesignPatternPartTwo.Base;
using Unity;
using Unity.Resolution;

namespace AdvancedBehavioursDesignPatternPartTwo.Behaviours.Core;

public static class BehaviorEngine
{
    public static void Execute(params BehaviorDefinition[] behaviorDefinitions)
    {
        foreach (var definition in behaviorDefinitions)
        {
            var behavior =
                UnityContainerFactory.GetContainer().Resolve(
                    definition.BehaviorType,
                    new ResolverOverride[]
                    {
                        new ParameterOverride("definition", definition)
                    }) as Behavior;
            behavior.Execute();
        }
    }
}