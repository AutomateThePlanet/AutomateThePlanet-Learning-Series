package strategyadvanced.strategies;

import strategyadvanced.core.Driver;
import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.base.OrderPurchaseStrategy;

public class NoTaxOrderPurchaseStrategy implements OrderPurchaseStrategy {
    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderVatTaxPrice(0);
    }

    @Override
    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        // Throw a new IllegalArgumentException if the country is part of the EU Union.
    }
}
