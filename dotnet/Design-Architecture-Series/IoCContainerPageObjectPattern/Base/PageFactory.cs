// <copyright file="PageFactory.cs" company="Automate The Planet Ltd.">
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

using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace IoCContainerPageObjectPattern.Base
{
    public static class PageFactory
    {
        private static readonly IUnityContainer Container; 

        static PageFactory()
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = "unity.config" };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            Container = new UnityContainer(); 
            Container.LoadConfiguration(unitySection);
        }

        public static T Get<T>()
        {
            return Container.Resolve<T>();
        }
    }
}