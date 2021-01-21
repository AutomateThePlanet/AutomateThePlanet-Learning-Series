// <copyright file="SignInPageLoginBehaviour.cs" company="Automate The Planet Ltd.">
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
using Microsoft.Practices.Unity;
using PerfectSystemTestsDesign.Pages.ShippingAddressPage;
using PerfectSystemTestsDesign.Pages.SignInPage;

namespace PerfectSystemTestsDesign.Behaviours
{
    public class SignInPageLoginBehaviour : PerfectSystemTestsDesign.Behaviours.Core.WaitableActionBehaviour
    {
        private readonly SignInPage signInPage;
        private readonly ShippingAddressPage shippingAddressPage;
        private readonly PerfectSystemTestsDesign.Data.ClientLoginInfo clientLoginInfo;

        public SignInPageLoginBehaviour(PerfectSystemTestsDesign.Data.ClientLoginInfo clientLoginInfo)
        {
            this.signInPage = PerfectSystemTestsDesign.Base.UnityContainerFactory.GetContainer().Resolve<SignInPage>();
            this.shippingAddressPage = PerfectSystemTestsDesign.Base.UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>(); 
            this.clientLoginInfo = clientLoginInfo;
        }

        protected override void PerformPostActWait()
        {
            this.shippingAddressPage.WaitForPageToLoad();
        }

        protected override void PerformAct()
        {
            this.signInPage.Login(this.clientLoginInfo.Email, this.clientLoginInfo.Password);
        }
    }
}