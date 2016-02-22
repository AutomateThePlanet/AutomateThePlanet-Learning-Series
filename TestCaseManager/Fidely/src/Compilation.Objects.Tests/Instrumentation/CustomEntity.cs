using System;

namespace Fidely.Framework.Compilation.Objects.Tests.Instrumentation
{
    public class CustomEntity
    {
        public decimal Decimal { get; set; }
        
        public Guid Guid { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public DateTimeOffset DateTimeOffset { get; set; }
        
        public TimeSpan TimeSpan { get; set; }

        public string String { get; set; }

        public object Null { get; set; }
    }
}
