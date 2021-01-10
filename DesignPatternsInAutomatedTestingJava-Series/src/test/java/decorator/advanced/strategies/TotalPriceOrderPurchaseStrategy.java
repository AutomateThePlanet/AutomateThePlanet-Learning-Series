package decorator.advanced.strategies;

import decorator.core.Driver;
import decorator.pages.checkoutpage.CheckoutPage;

import java.math.BigDecimal;

public class TotalPriceOrderPurchaseStrategy extends OrderPurchaseStrategy {
    private final BigDecimal itemsPrice;

    public TotalPriceOrderPurchaseStrategy(BigDecimal itemsPrice) {
        this.itemsPrice = itemsPrice;
    }

    @Override
    public BigDecimal calculateTotalPrice() {
        return itemsPrice;
    }

    @Override
    public void validateOrderSummary(BigDecimal totalPrice) {
        Driver.retry(10,500, ()->Driver.getPage(CheckoutPage.class).assertions()
                .assertOrderTotalPrice(totalPrice));
    }
}
