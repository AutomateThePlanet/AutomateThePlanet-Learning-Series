using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriver.Tools
{
    public class Facades
    {
        public OrderConfirmation OrderConfirmation()
        {
            return new OrderConfirmation();
        }
    }
}
