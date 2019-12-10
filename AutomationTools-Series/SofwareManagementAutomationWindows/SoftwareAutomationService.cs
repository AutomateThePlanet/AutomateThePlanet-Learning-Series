// <copyright file="SoftwareAutomationService.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>

using System;
using System.Linq;
using System.Management.Automation;

namespace SoftwareManagementAutomationWindows
{
    public static class SoftwareAutomationService
    {
        public static void InstallRequiredSoftware()
        {
            var machineAutomationSettings = ConfigurationService.Instance.GetMachineAutomationSettings();
            if (machineAutomationSettings.IsEnabled && machineAutomationSettings.PackagesToBeInstalled.Any())
            {
                using (var powerShellInstance = PowerShell.Create())
                {
                    // install Chocolatey
                    powerShellInstance.AddScript("Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))");

                    // Remove agree prompt.
                    powerShellInstance.AddScript("choco feature enable -n=allowGlobalConfirmation");
                    foreach (var packageToBeInstalled in machineAutomationSettings.PackagesToBeInstalled)
                    {
                        Console.WriteLine($"INSTALL {packageToBeInstalled}");
                        string[] packageParts = packageToBeInstalled.Split(' ');
                        powerShellInstance.AddScript($"choco install {packageParts.First()}");
                        powerShellInstance.AddParameter("--allow-downgrade");
                        powerShellInstance.AddParameter("--force");
                        if (packageParts.Length >= 2)
                        {
                            string version = packageParts[1].Split('=').Last();
                            powerShellInstance.AddParameter("version", version);
                        }
                    }

                    try
                    {
                        var psOutput = powerShellInstance.Invoke();

                        if (powerShellInstance.Streams.Error.Count > 0)
                        {
                            foreach (var outputItem in psOutput)
                            {
                                // if null object was dumped to the pipeline during the script then a null
                                // object may be present here. check for null to prevent potential NRE.
                                if (outputItem != null)
                                {
                                    Console.WriteLine(outputItem.BaseObject.ToString());
                                }
                            }
                        }
                    }
                    catch (Exception e) when (e.Message.Contains("Installation of Chocolatey to default folder requires Administrative permissions"))
                    {
                        throw new InvalidOperationException("To use BELLATRIX.MachineAutomation please start Visual Studio in Administrative Mode.", e);
                    }
                }
            }
        }
    }
}
