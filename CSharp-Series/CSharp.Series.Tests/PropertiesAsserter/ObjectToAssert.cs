using System;

namespace CSharp.Series.Tests.PropertiesAsserter
{
    public class ObjectToAssert
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PoNumber { get; set; }

        public decimal Price { get; set; }

        public DateTime SkipDateTime { get; set; }
    }
}