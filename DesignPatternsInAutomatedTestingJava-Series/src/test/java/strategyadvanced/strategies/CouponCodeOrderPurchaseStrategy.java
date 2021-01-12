package strategyadvanced.strategies;

import strategyadvanced.services.CouponCodeCalculationService;
import strategyadvanced.core.Driver;
import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.base.OrderPurchaseStrategy;

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

    @Override
    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        if (purchaseInfo.getCouponCode() == null) {
            throw new IllegalArgumentException("A coupon code should be set if the CouponCodeOrderPurchaseStrategy is executed");
        }
    }
}
