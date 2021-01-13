package decorator.pages.checkoutpage;

import decorator.core.BaseAssertions;
import decorator.core.Driver;
import decorator.enums.PaymentMethod;
import org.testng.Assert;

import java.text.DecimalFormat;

public class CheckoutAssertions extends BaseAssertions<CheckoutElements> {

    private String formatCurrency(Number number) {
        DecimalFormat currencyFormat = new DecimalFormat("#,##0.00\u20ac");
        currencyFormat.setMaximumFractionDigits(2);
        currencyFormat.setMinimumFractionDigits(2);
        return currencyFormat.format(number);
    }

    public void assertOrderReceived() {
        Assert.assertEquals(elements().receivedMessage().getText(), "Order received");
    }

    public void assertOrderSubtotalPrice(Number subtotalPrice) {
        Assert.assertEquals(elements().orderDetailsSubtotal().getText(), formatCurrency(subtotalPrice));
    }

    public void assertOrderDiscountPrice(Number discountPrice) {
        Assert.assertEquals(elements().orderDetailsDiscount().getText(), formatCurrency(discountPrice));
    }

    public void assertOrderVatTaxPrice(Number vatTaxPrice) {
        Assert.assertEquals(elements().orderDetailsVatTax().getText(), formatCurrency(vatTaxPrice));
    }

    public void assertOrderPaymentMethod(PaymentMethod paymentMethod) {
        Assert.assertEquals(elements().orderDetailsPaymentMethod().getText(), paymentMethod.toString());
    }

    public void assertOrderTotalPrice(Number totalPrice) {
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        Assert.assertEquals(elements().orderDetailsTotal().getText(), formatCurrency(totalPrice));
    }
}
