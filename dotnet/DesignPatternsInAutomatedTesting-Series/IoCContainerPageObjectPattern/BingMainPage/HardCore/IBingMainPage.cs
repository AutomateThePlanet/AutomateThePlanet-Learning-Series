﻿// <copyright file="IBingMainPage.cs" company="Automate The Planet Ltd.">
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

using IoCContainerPageObjectPattern.Base;
using IoCContainerPageObjectPattern.Enums;

namespace IoCContainerPageObjectPattern.BingMainPage.HardCore
{
    public interface IBingMainPage<TM, TV>
    : IPage<TM, TV>
        where TM : BasePageElementMap, new()
        where TV : BasePageValidator<TM>, new() 
    {
        void Search(string textToType);

        void ClickImages();

        void SetSize(Sizes size);

        void SetColor(Colors color);

        void SetTypes(Types type);

        void SetLayout(Layouts layout);

        void SetPeople(People people);

        void SetDate(Dates date);

        void SetLicense(Licenses license);
    }
}