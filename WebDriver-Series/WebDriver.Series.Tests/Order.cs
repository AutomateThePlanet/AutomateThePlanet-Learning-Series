using System;
namespace WebDriver.Series.Tests
{
    public class Order
    {
        public Order(string shipName) : this()
        {
            if (!string.IsNullOrEmpty(shipName))
            {
                this.ShipName = shipName;
            }            
        }

        public Order()
        {
            Random rand = new Random();
            this.OrderId = rand.Next();
            this.ShipName = Guid.NewGuid().ToString();
            this.Freight = rand.Next();
            this.OrderDate = DateTime.Now;
        }

        public int OrderId { get; set; }
        public string ShipName { get; set; }
        public double Freight { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
