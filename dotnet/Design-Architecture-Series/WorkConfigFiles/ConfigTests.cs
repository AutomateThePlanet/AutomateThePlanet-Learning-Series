// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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

using HybridTestFramework.UITests.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WorkConfigFiles
{
    [TestClass]
    public class ConfigTests 
    {
        [TestMethod]
        public void GetConfigValues_From_TimeoutSettings()
        {
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.WaitForAjaxTimeout),
                TimeoutSettingsProvider.WaitForAjaxTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.SleepInterval),
                TimeoutSettingsProvider.SleepInterval);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.ElementToBeVisibleTimeout),
                TimeoutSettingsProvider.ElementToBeVisibleTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.ЕlementNotToBeVisibleTimeout),
                TimeoutSettingsProvider.ЕlementNotToBeVisibleTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.ЕlementToBeClickableTimeout),
                TimeoutSettingsProvider.ЕlementToBeClickableTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.ЕlementToExistTimeout),
                TimeoutSettingsProvider.ЕlementToExistTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.ЕlementToHaveContentTimeout),
                TimeoutSettingsProvider.ЕlementToHaveContentTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsProvider),
                nameof(TimeoutSettingsProvider.ЕlementToNotExistTimeout),
                TimeoutSettingsProvider.ЕlementToNotExistTimeout);
        }

        [TestMethod]
        public void GetConfigValues_From_TimeoutSettingsAsAttributes()
        {
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.WaitForAjaxTimeout),
                TimeoutSettingsAsAttributesProvider.WaitForAjaxTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.SleepInterval),
                TimeoutSettingsAsAttributesProvider.SleepInterval);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.ElementToBeVisibleTimeout),
                TimeoutSettingsAsAttributesProvider.ElementToBeVisibleTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.ЕlementNotToBeVisibleTimeout),
                TimeoutSettingsAsAttributesProvider.ЕlementNotToBeVisibleTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.ЕlementToBeClickableTimeout),
                TimeoutSettingsAsAttributesProvider.ЕlementToBeClickableTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.ЕlementToExistTimeout),
                TimeoutSettingsAsAttributesProvider.ЕlementToExistTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.ЕlementToHaveContentTimeout),
                TimeoutSettingsAsAttributesProvider.ЕlementToHaveContentTimeout);
            Console.WriteLine("From {0}- {1} = {2}",
                nameof(TimeoutSettingsAsAttributesProvider),
                nameof(TimeoutSettingsAsAttributesProvider.ЕlementToNotExistTimeout),
                TimeoutSettingsAsAttributesProvider.ЕlementToNotExistTimeout);
        }
    }
}