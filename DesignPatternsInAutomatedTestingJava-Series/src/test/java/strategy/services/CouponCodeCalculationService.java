package strategy.services;

import strategy.enums.CouponCode;

public class CouponCodeCalculationService {
    double discount;
    public double calculate(double price, String couponCode) {
        if (couponCode == null) {
            return 0;
        }
        switch(CouponCode.valueOf(couponCode.toUpperCase())) {
            case HAPPYBIRTHDAY:
                discount = 5;
                break;
            default:
                discount = 0;
                break;
        }
        return discount;
    }
}
