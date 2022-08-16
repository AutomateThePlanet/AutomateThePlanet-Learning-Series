// <copyright file="SignInPageLoginBehaviour.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using AdvancedBehavioursDesignPattern.Base;
using AdvancedBehavioursDesignPattern.Behaviours.Core;
using AdvancedBehavioursDesignPattern.Pages.ShippingAddressPage;
using AdvancedBehavioursDesignPattern.Pages.SignInPage;
using Unity;
using AdvancedBehavioursDesignPattern.Data;

namespace AdvancedBehavioursDesignPattern.Behaviours;

public class SignInPageLoginBehaviour : WaitableActionBehaviour
{
    private readonly SignInPage _signInPage;
    private readonly ShippingAddressPage _shippingAddressPage;
    private readonly ClientLoginInfo _clientLoginInfo;

    public SignInPageLoginBehaviour(ClientLoginInfo clientLoginInfo)
    {
        _signInPage = UnityContainerFactory.GetContainer().Resolve<SignInPage>();
        _shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
        _clientLoginInfo = clientLoginInfo;
    }

    protected override void PerformPostActWait()
    {
        _shippingAddressPage.WaitForPageToLoad();
    }

    protected override void PerformAct()
    {
        _signInPage.Login(_clientLoginInfo.Email, _clientLoginInfo.Password);
    }
}