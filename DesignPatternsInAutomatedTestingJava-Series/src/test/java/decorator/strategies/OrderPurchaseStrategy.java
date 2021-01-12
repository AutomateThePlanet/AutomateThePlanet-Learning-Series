package decorator.strategies;

public abstract class OrderPurchaseStrategy {
    public abstract double calculateTotalPrice();
    public abstract void assertOrderSummary(double totalPrice);
}
