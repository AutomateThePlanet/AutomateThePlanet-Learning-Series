package strategy.pages.checkoutpage;

import org.testng.Assert;
import strategy.core.BaseAssertions;
import strategy.enums.PaymentMethod;

import java.text.DecimalFormat;

public class CheckoutAssertions extends BaseAssertions<CheckoutElements> {

    private String formatCurrency(double number) {
        DecimalFormat currencyFormat = new DecimalFormat("#,##0.00\u20ac");
        currencyFormat.setMaximumFractionDigits(2);
        currencyFormat.setMinimumFractionDigits(2);
        return currencyFormat.format(number);
    }

    public void assertOrderReceived() {
        Assert.assertEquals(elements().receivedMessage().getText(), "Order received");
    }

    public void assertOrderSubtotalPrice(double subtotalPrice) {
        Assert.assertEquals(elements().orderDetailsSubtotal().getText(), formatCurrency(subtotalPrice));
    }

    public void assertOrderDiscountPrice(double discountPrice) {
        Assert.assertEquals(elements().orderDetailsDiscount().getText(), formatCurrency(discountPrice));
    }

    public void assertOrderVatTaxPrice(double vatTaxPrice) {
        Assert.assertEquals(elements().orderDetailsVatTax().getText(), formatCurrency(vatTaxPrice));
    }

    public void assertOrderPaymentMethod(PaymentMethod paymentMethod) {
        Assert.assertEquals(elements().orderDetailsPaymentMethod().getText(), paymentMethod.toString());
    }

    public void assertOrderTotalPrice(double totalPrice) {
        Assert.assertEquals(elements().orderDetailsTotal().getText(), formatCurrency(totalPrice));
    }
}
