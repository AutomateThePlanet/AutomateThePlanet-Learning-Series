using System;

namespace CSharp.Series.Tests
{
public static class ObsoleteExample
{
    // Mark OrderDetailTotal As Obsolete.
    [ObsoleteAttribute("This property (DepricatedOrderDetailTotal) is obsolete. Use InvoiceTotal instead.", false)]
    public static decimal OrderDetailTotal
    {
        get
        {
            return 12m;
        }
    }

    public static decimal InvoiceTotal
    {
        get
        {
            return 25m;
        }
    }

    // Mark CalculateOrderDetailTotal As Obsolete.
    [ObsoleteAttribute("This method is obsolete. Call CalculateInvoiceTotal instead.", true)]
    public static decimal CalculateOrderDetailTotal()
    {
        return 0m;
    }

    public static decimal CalculateInvoiceTotal()
    {
        return 1m;
    }
}
}