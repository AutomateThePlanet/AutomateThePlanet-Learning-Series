package decorator.services;

import decorator.enums.CouponCode;

import java.math.BigDecimal;
import java.math.RoundingMode;

public class CouponCodeCalculationService {
    BigDecimal discount;
    public BigDecimal calculate(BigDecimal price, String couponCode) {
        switch(CouponCode.valueOf(couponCode.toUpperCase())) {
            case HAPPYBIRTHDAY:
                discount = calculateVATInternal(price, 5);
                break;
            default:
                discount = BigDecimal.valueOf(0);
                break;
        }

        return discount;
    }

    private static BigDecimal calculateVATInternal(BigDecimal price, double percent) {
        var discount = price.multiply(BigDecimal.valueOf(percent).divide(BigDecimal.valueOf(100), RoundingMode.CEILING));
        return discount;
    }
}
