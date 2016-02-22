using System;
using System.ComponentModel;

namespace Fidely.Framework.Tests.Instrumentation
{
    public class ItemEntity
    {
        public int Id { get; set; }

        [Alias("n", Description="Item Name")]
        public string Name { get; set; }

        [Alias("p")]
        [Alias("price")]
        [Description("Unit Price")]
        public decimal UnitPrice { get; set; }

        [Alias("rate")]
        public double? DiscountRate { get; set; }
    }
}
