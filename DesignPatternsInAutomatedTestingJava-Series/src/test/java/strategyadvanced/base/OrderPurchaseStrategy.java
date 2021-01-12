package strategyadvanced.base;

import strategyadvanced.data.PurchaseInfo;

public interface OrderPurchaseStrategy {
    void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo);
    void validatePurchaseInfo(PurchaseInfo purchaseInfo);
}
