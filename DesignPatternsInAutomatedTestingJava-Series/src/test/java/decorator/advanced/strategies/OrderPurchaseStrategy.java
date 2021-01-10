package decorator.advanced.strategies;

import java.math.BigDecimal;

public abstract class OrderPurchaseStrategy {
    public abstract BigDecimal calculateTotalPrice();
    public abstract void validateOrderSummary(BigDecimal totalPrice);
}
