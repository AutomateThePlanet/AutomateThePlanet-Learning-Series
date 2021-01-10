package decorator.advanced.strategies;

import decorator.data.PurchaseInfo;
import decorator.enums.Country;
import decorator.services.VatTaxCalculationService;

import java.math.BigDecimal;
import java.util.Arrays;

public class VatTaxOrderPurchaseStrategy extends OrderPurchaseStrategyDecorator {
    private final VatTaxCalculationService vatTaxCalculationService;
    private BigDecimal vatTax;

    public VatTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, BigDecimal itemPrice, PurchaseInfo purchaseInfo) {
        super(orderPurchaseStrategy, itemPrice, purchaseInfo);
        vatTaxCalculationService = new VatTaxCalculationService();
    }

    @Override
    public BigDecimal calculateTotalPrice() {
        var currentCountry = Arrays.stream(Country.values())
                .filter(country -> country.toString().equals(purchaseInfo.getCountry()))
                .toArray(Country[]::new)[0];
        vatTax = vatTaxCalculationService.calculate(itemPrice, currentCountry);
        return orderPurchaseStrategy.calculateTotalPrice().add(vatTax);
    }
}
