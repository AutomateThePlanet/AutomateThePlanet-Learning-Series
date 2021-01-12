package decorator.strategies;

import decorator.data.PurchaseInfo;
import decorator.services.CouponCodeCalculationService;

public class CouponCodeOrderPurchaseStrategy extends OrderPurchaseStrategyDecorator {
    private final CouponCodeCalculationService couponCodeCalculationService;
    private double discount;

    public CouponCodeOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, double itemPrice, PurchaseInfo purchaseInfo) {
        super(orderPurchaseStrategy, itemPrice, purchaseInfo);
        couponCodeCalculationService = new CouponCodeCalculationService();
    }

    @Override
    public double calculateTotalPrice() {
        discount = couponCodeCalculationService.calculate(itemPrice, purchaseInfo.getCouponCode());
        return orderPurchaseStrategy.calculateTotalPrice() - discount;
    }
}
