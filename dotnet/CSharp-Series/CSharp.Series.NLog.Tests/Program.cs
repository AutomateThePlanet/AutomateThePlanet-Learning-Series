// <copyright file="Program.cs" company="Automate The Planet Ltd.">
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
using CSharp.Series.NLog.Tests.Loggers;
using Microsoft.Practices.Unity;
using NLog;
using Loggers= CSharp.Series.NLog.Tests.Loggers;

namespace CSharp.Series.NLog.Tests
{
    public class Program
    {
        ////private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            // Register a type to have a singleton lifetime without mapping the type
            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<Loggers.ILogger, EventLogger>(new ContainerControlledLifetimeManager());
            var eventLogger = unityContainer.Resolve<Loggers.ILogger>(); 
            eventLogger.LogInfo("EventLogger log message to Kaspersky event log.");
        }
    }
}