package decorator.strategies;

import decorator.core.Driver;
import decorator.pages.checkoutpage.CheckoutPage;

public class TotalPriceOrderPurchaseStrategy extends OrderPurchaseStrategy {
    private final double itemsPrice;

    public TotalPriceOrderPurchaseStrategy(double itemsPrice) {
        this.itemsPrice = itemsPrice;
    }

    @Override
    public double calculateTotalPrice() {
        return itemsPrice;
    }

    @Override
    public void assertOrderSummary(double totalPrice) {
        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderTotalPrice(totalPrice);
    }
}
