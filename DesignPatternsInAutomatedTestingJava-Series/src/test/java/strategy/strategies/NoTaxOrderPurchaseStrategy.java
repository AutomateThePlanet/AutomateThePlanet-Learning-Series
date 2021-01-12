package strategy.strategies;

import strategy.base.OrderPurchaseStrategy;
import strategy.core.Driver;
import strategy.data.PurchaseInfo;
import strategy.pages.checkoutpage.CheckoutPage;

public class NoTaxOrderPurchaseStrategy implements OrderPurchaseStrategy {
    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderVatTaxPrice(0);
    }
}
