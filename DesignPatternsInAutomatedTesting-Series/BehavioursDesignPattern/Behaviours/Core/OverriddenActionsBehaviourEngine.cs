// <copyright file="OverridenActionsBehaviourEngine.cs" company="Automate The Planet Ltd.">
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

namespace BehavioursDesignPattern.Behaviours.Core
{
    public class OverriddenActionsBehaviourEngine
    {
        private readonly Dictionary<Type, Dictionary<BehaviourActions, Action>> overridenBehavioursActions;

        public OverriddenActionsBehaviourEngine()
        {
            this.overridenBehavioursActions = new Dictionary<Type, Dictionary<BehaviourActions, Action>>();
        }

        public void Execute(params Type[] pageBehaviours)
        {
            foreach (Type pageBehaviour in pageBehaviours)
            {
                var currentbehaviour = Activator.CreateInstance(pageBehaviour) as Behaviour;
                this.ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.PreActAsserts, () => currentbehaviour.PerformPreActAsserts());
                this.ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.Act, () => currentbehaviour.PerformAct());
                this.ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.PostActAsserts, () => currentbehaviour.PerformPostActAsserts());
                this.ExecuteBehaviourOperation(pageBehaviour, BehaviourActions.PostAct, () => currentbehaviour.PerformPostAct());
            }
        }

        public void ConfugureCustomBehaviour<ТBehavior>(BehaviourActions behaviourAction, Action action)
            where ТBehavior : IBehaviour
        {
            if (!this.overridenBehavioursActions.ContainsKey(typeof(ТBehavior)))
            {
                this.overridenBehavioursActions.Add(typeof(ТBehavior), new Dictionary<BehaviourActions, Action>());
            }
            if (!this.overridenBehavioursActions[typeof(ТBehavior)].ContainsKey(behaviourAction))
            {
                this.overridenBehavioursActions[typeof(ТBehavior)].Add(behaviourAction, action);
            }
            else
            {
                this.overridenBehavioursActions[typeof(ТBehavior)][behaviourAction] = action;
            }
        }

        private void ExecuteBehaviourOperation(Type pageBehaviour, BehaviourActions behaviourAction, Action defaultBehaviourOperation)
        {
            if (this.overridenBehavioursActions.ContainsKey(pageBehaviour.GetType()) &&
                this.overridenBehavioursActions[pageBehaviour.GetType()].ContainsKey(behaviourAction))
            {
                this.overridenBehavioursActions[pageBehaviour.GetType()][behaviourAction].Invoke();
            }
            else
            {
                defaultBehaviourOperation.Invoke();
            }
        }
    }
}