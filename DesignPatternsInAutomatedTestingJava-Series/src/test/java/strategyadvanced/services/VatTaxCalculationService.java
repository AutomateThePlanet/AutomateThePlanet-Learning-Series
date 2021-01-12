package strategyadvanced.services;

import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.enums.Country;

public class VatTaxCalculationService {
    private double taxValue;

    public double calculate(double price, Country country, PurchaseInfo purchaseInfo) {
        switch(country) {
            case BULGARIA:
            case UNITED_KINGDOM:
            case GERMANY:
            case AUSTRIA:
            case FRANCE:
                taxValue = calculateVATInternal(price, 20, purchaseInfo);
                break;
            default:
                taxValue = 0;
                break;
        }

        return taxValue;
    }

    private static double calculateVATInternal(double price, double percent, PurchaseInfo purchaseInfo) {
        var couponCodeCalculationService = new CouponCodeCalculationService();
        var taxValue = (price - couponCodeCalculationService.calculate(price, purchaseInfo.getCouponCode())) * percent / 100;
        return taxValue;
    }
}
