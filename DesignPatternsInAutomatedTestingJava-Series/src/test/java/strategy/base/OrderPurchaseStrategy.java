package strategy.base;

import strategy.data.PurchaseInfo;

public interface OrderPurchaseStrategy {
    void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo);
}
