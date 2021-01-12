package decorator.strategies;

import decorator.data.PurchaseInfo;

public class OrderPurchaseStrategyDecorator extends OrderPurchaseStrategy {
    protected final OrderPurchaseStrategy orderPurchaseStrategy;
    protected final PurchaseInfo purchaseInfo;
    protected final double itemPrice;

    public OrderPurchaseStrategyDecorator(OrderPurchaseStrategy orderPurchaseStrategy, double itemPrice, PurchaseInfo purchaseInfo) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
        this.itemPrice = itemPrice;
        this.purchaseInfo = purchaseInfo;
    }

    @Override
    public double calculateTotalPrice() {
        validateOrderStrategy();
        return orderPurchaseStrategy.calculateTotalPrice();
    }

    @Override
    public void assertOrderSummary(double totalPrice) {
        validateOrderStrategy();
        orderPurchaseStrategy.assertOrderSummary(totalPrice);
    }

    private void validateOrderStrategy() {
        if (orderPurchaseStrategy == null) {
            throw new NullPointerException("The OrderPurchaseStrategy should be first initialized.");
        }
    }
}
