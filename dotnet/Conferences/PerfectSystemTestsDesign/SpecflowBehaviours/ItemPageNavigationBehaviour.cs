﻿// <copyright file="ItemPageNavigationBehaviour.cs" company="Automate The Planet Ltd.">
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

using Microsoft.Practices.Unity;
using PerfectSystemTestsDesign.SpecflowBehaviours.Core;
using PerfectSystemTestsDesign.Pages.ItemPage;
using TechTalk.SpecFlow;

namespace PerfectSystemTestsDesign.SpecflowBehaviours
{
    [Binding]
    public class ItemPageNavigationBehaviour : ActionBehaviour
    {
        // The variables are no more readonly. 
        // The base behavior classes no more implement the IBehavior interface.
        // There is no more BehaviourExecutor.
        // The parameters are no more passed through the constructors.
        private readonly ItemPage itemPage;
        private string itemUrl;

        public ItemPageNavigationBehaviour()
        {
            this.itemPage = PerfectSystemTestsDesign.Base.UnityContainerFactory.GetContainer().Resolve<ItemPage>();
         
        }

        [When(@"I navigate to ""([^""]*)""")]
        public void NavigateToItemUrl(string itemUrl)
        {
            this.itemUrl = itemUrl;
            base.Execute();
        }

        
        protected override void PerformAct()
        {
            this.itemPage.Navigate(this.itemUrl);
        }
    }
}