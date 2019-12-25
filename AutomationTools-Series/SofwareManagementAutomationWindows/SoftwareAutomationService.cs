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
