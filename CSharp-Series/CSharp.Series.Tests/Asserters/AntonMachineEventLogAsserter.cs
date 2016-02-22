using System.Collections.Generic;

namespace CSharp.Series.Tests.Asserters
{
    public class AntonMachineEventLogAsserter : EventLogAsserter
    {
        public AntonMachineEventLogAsserter()
            : base("Application", new List<string>() { "DESKTOP-FL88834" })
        {
        }
    }
}
