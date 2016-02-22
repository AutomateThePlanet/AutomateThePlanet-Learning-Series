using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Fidely.Demo.Windows.Model
{
    public class InMemoryTraceListener : TraceListener
    {
        public static event EventHandler<FlashedEventArgs> Flashed;


        private readonly StringBuilder messages;


        public InMemoryTraceListener()
        {
            messages = new StringBuilder();
        }


        public override void Flush()
        {
            base.Flush();

            if (Flashed != null)
            {
                Flashed(this, new FlashedEventArgs(messages.ToString().Split(new string[]{ Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList()));
            }

            messages.Clear();
        }

        public override void Write(string message)
        {
            messages.Append(message);
        }

        public override void WriteLine(string message)
        {
            messages.AppendLine(message);
        }
    }
}
