// <copyright file="BaseTestExecutionEngine.cs" company="Automate The Planet Ltd.">
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

using HybridTestFramework.UITests.Core;
using Unity;
using System;
using System.Reflection;

namespace HybridTestFramework.Core.Behaviours.Contracts
{
    public abstract class BaseTestExecutionEngine<TExecutionEngineAttribute> : ITestExecutionEngine
        where TExecutionEngineAttribute : ExecutionEngineAttribute
    {
        protected IDriver Driver;      
        protected readonly IUnityContainer Container;
        private bool _isDisposed;

        public BaseTestExecutionEngine(IUnityContainer container)
        {
            this.Container = container;
        }

        public void Dispose()
        {
            if (!_isDisposed && Driver != null)
            {
                Driver.Quit();
                _isDisposed = true;
            }
        }

        public bool IsSelected(MemberInfo memberInfo)
        {
            var isSelectedOnMethodLevel = GetExecutionEngineTypeByMethodInfo(memberInfo);
            var isSelectedOnClassLevel = GetExecutionEngineType(memberInfo.DeclaringType);

            return isSelectedOnMethodLevel || isSelectedOnClassLevel;
        }

        public abstract void RegisterDependencies(Browsers executionBrowserType);

        private bool GetExecutionEngineTypeByMethodInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("The test method's info cannot be null.");
            }
            var executionEngineTypeMethodAttribute =
                memberInfo.GetCustomAttribute<ExecutionEngineAttribute>();
            if (executionEngineTypeMethodAttribute != null)
            {
                return true;
            }
            return false;
        }

        private bool GetExecutionEngineType(Type currentType)
        {
            if (currentType == null)
            {
                throw new ArgumentNullException("The test method's type cannot be null.");
            }

            var executionEngineClassAttribute = 
                currentType.GetCustomAttribute<ExecutionEngineAttribute>(true);
            if (executionEngineClassAttribute != null)
            {
                return true;
            }
            return false;
        }
    }
}