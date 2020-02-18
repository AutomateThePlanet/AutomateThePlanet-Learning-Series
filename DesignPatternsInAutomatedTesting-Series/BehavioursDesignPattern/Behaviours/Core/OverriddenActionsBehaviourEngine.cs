// <copyright file="OverridenActionsBehaviourEngine.cs" company="Automate The Planet Ltd.">
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

using System;
using System.Collections.Generic;

namespace BehavioursDesignPattern.Behaviours.Core
{
    public class OverriddenActionsBehaviourEngine
    {
        private readonly Dictionary<Type, Dictionary<BehaviourActions, Action>> _overridenBehavioursActions;

        public OverriddenActionsBehaviourEngine()
        {
            _overridenBehavioursActions = new Dictionary<Type, Dictionary<BehaviourActions, Action>>();
        }

        public void Execute(params Type[] pageBehaviours)
        {
            foreach (var pageBehaviour in pageBehaviours)
            {
                var currentbehaviour = Activator.CreateInstance(pageBehaviour) as Behaviour;
                ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.PreActAsserts, () => currentbehaviour.PerformPreActAsserts());
                ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.Act, () => currentbehaviour.PerformAct());
                ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.PostActAsserts, () => currentbehaviour.PerformPostActAsserts());
                ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.PostAct, () => currentbehaviour.PerformPostAct());
            }
        }

        public void ConfugureCustomBehaviour<TТBehavior>(BehaviourActions behaviourAction, Action action)
            where TТBehavior : IBehaviour
        {
            if (!_overridenBehavioursActions.ContainsKey(typeof(TТBehavior)))
            {
                _overridenBehavioursActions.Add(typeof(TТBehavior), new Dictionary<BehaviourActions, Action>());
            }
            if (!_overridenBehavioursActions[typeof(TТBehavior)].ContainsKey(behaviourAction))
            {
                _overridenBehavioursActions[typeof(TТBehavior)].Add(behaviourAction, action);
            }
            else
            {
                _overridenBehavioursActions[typeof(TТBehavior)][behaviourAction] = action;
            }
        }

        private void ExecuteBehaviourOperation(Type pageBehaviour, BehaviourActions behaviourAction, Action defaultBehaviourOperation)
        {
            if (_overridenBehavioursActions.ContainsKey(pageBehaviour.GetType()) &&
                _overridenBehavioursActions[pageBehaviour.GetType()].ContainsKey(behaviourAction))
            {
                _overridenBehavioursActions[pageBehaviour.GetType()][behaviourAction].Invoke();
            }
            else
            {
                defaultBehaviourOperation.Invoke();
            }
        }
    }
}