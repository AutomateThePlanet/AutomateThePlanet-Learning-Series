package decorator.advanced.strategies;

import decorator.data.PurchaseInfo;
import decorator.services.CouponCodeCalculationService;

import java.math.BigDecimal;

public class CouponCodeOrderPurchaseStrategy extends OrderPurchaseStrategyDecorator {
    private final CouponCodeCalculationService couponCodeCalculationService;
    private BigDecimal discount;

    public CouponCodeOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, BigDecimal itemPrice, PurchaseInfo purchaseInfo) {
        super(orderPurchaseStrategy, itemPrice, purchaseInfo);
        couponCodeCalculationService = new CouponCodeCalculationService();
    }

    @Override
    public BigDecimal calculateTotalPrice() {
        discount = couponCodeCalculationService.calculate(itemPrice, purchaseInfo.getCouponCode());
        return orderPurchaseStrategy.calculateTotalPrice().subtract(discount);
    }
}
