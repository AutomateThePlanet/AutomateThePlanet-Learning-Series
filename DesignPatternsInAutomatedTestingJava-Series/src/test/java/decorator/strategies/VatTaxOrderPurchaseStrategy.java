package decorator.strategies;

import decorator.data.PurchaseInfo;
import decorator.enums.Country;
import decorator.services.CouponCodeCalculationService;
import decorator.services.VatTaxCalculationService;

import java.util.Arrays;

public class VatTaxOrderPurchaseStrategy extends OrderPurchaseStrategyDecorator {
    private final VatTaxCalculationService vatTaxCalculationService;
    private final CouponCodeCalculationService couponCodeCalculationService;
    private double vatTax;

    public VatTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, double itemPrice, PurchaseInfo purchaseInfo) {
        super(orderPurchaseStrategy, itemPrice, purchaseInfo);
        vatTaxCalculationService = new VatTaxCalculationService();
        couponCodeCalculationService = new CouponCodeCalculationService();
    }

    @Override
    public double calculateTotalPrice() {
        var currentCountry = Arrays.stream(Country.values())
                .filter(country -> country.toString().equals(purchaseInfo.getCountry()))
                .toArray(Country[]::new)[0];
        vatTax = vatTaxCalculationService.calculate((itemPrice - couponCodeCalculationService.calculate(itemPrice, purchaseInfo.getCouponCode())), currentCountry);
        return orderPurchaseStrategy.calculateTotalPrice() + vatTax;
    }
}
