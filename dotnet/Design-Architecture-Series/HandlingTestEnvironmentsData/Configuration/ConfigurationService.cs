// <copyright file="ConfigurationService.cs" company="Automate The Planet Ltd.">
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

using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HandlingTestEnvironmentsData
{
    public sealed class ConfigurationService
    {
        private static ConfigurationService _instance;

        public ConfigurationService() => Root = InitializeConfiguration();

        public static ConfigurationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationService();
                }

                return _instance;
            }
        }

        public IConfigurationRoot Root { get; }

        public BillingInfoDefaultValues GetBillingInfoDefaultValues()
        {
            var result = ConfigurationService.Instance.Root.GetSection("billingInfoDefaultValues").Get<BillingInfoDefaultValues>();

            if (result == null)
            {
                throw new ConfigurationNotFoundException(typeof(BillingInfoDefaultValues).ToString());
            }

            return result;
        }

        public UrlSettings GetUrlSettings()
        {
            var result = ConfigurationService.Instance.Root.GetSection("urlSettings").Get<UrlSettings>();

            if (result == null)
            {
                throw new ConfigurationNotFoundException(typeof(UrlSettings).ToString());
            }

            return result;
        }

        public WebSettings GetWebSettings()
         => ConfigurationService.Instance.Root.GetSection("webSettings").Get<WebSettings>();

        private IConfigurationRoot InitializeConfiguration()
        {
            var filesInExecutionDir = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var settingsFile =
                filesInExecutionDir.FirstOrDefault(x => x.Contains("testFrameworkSettings") && x.EndsWith(".json"));
            var builder = new ConfigurationBuilder();
            if (settingsFile != null)
            {
                builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
            }

            return builder.Build();
        }
    }
}
