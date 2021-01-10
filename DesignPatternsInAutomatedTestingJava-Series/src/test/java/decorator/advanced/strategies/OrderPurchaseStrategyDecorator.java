package decorator.advanced.strategies;

import decorator.data.PurchaseInfo;

import java.math.BigDecimal;

public class OrderPurchaseStrategyDecorator extends OrderPurchaseStrategy {
    protected final OrderPurchaseStrategy orderPurchaseStrategy;
    protected final PurchaseInfo purchaseInfo;
    protected final BigDecimal itemPrice;

    public OrderPurchaseStrategyDecorator(OrderPurchaseStrategy orderPurchaseStrategy, BigDecimal itemPrice, PurchaseInfo purchaseInfo) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
        this.itemPrice = itemPrice;
        this.purchaseInfo = purchaseInfo;
    }

    @Override
    public BigDecimal calculateTotalPrice() {
        validateOrderStrategy();
        return orderPurchaseStrategy.calculateTotalPrice();
    }

    @Override
    public void validateOrderSummary(BigDecimal totalPrice) {
        validateOrderStrategy();
        orderPurchaseStrategy.validateOrderSummary(totalPrice);
    }

    private void validateOrderStrategy() {
        if (orderPurchaseStrategy == null) {
            throw new NullPointerException("The OrderPurchaseStrategy should be first initialized.");
        }
    }
}
