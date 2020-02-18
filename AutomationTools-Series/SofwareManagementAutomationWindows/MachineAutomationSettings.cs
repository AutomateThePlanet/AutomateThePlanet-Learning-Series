using System.Collections.Generic;

namespace SoftwareManagementAutomationWindows
{
    public class MachineAutomationSettings
    {
        public bool IsEnabled { get; set; }
        public List<string> PackagesToBeInstalled { get; set; }
    }
}