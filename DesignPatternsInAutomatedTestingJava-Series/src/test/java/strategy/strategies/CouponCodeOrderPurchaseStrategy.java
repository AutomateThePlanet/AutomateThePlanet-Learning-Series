package strategy.strategies;

import strategy.services.CouponCodeCalculationService;
import strategy.base.OrderPurchaseStrategy;
import strategy.core.Driver;
import strategy.data.PurchaseInfo;
import strategy.pages.checkoutpage.CheckoutPage;

public class CouponCodeOrderPurchaseStrategy implements OrderPurchaseStrategy {
    private final CouponCodeCalculationService couponCodeCalculationService;

    public CouponCodeOrderPurchaseStrategy() {
        couponCodeCalculationService = new CouponCodeCalculationService();
    }

    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var discount = couponCodeCalculationService.calculate(itemPrice, purchaseInfo.getCouponCode());

        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderDiscountPrice(discount);
    }
}
