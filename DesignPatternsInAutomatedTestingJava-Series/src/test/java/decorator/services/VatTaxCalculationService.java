package decorator.services;

import decorator.enums.Country;

import java.math.BigDecimal;
import java.math.RoundingMode;

public class VatTaxCalculationService {
    BigDecimal taxValue;
    public BigDecimal calculate(BigDecimal price, Country country) {
        switch(country) {
            case BULGARIA:
            case UNITED_KINGDOM:
            case GERMANY:
            case AUSTRIA:
            case FRANCE:
                taxValue = calculateVATInternal(price, 20);
                break;
            default:
                taxValue = BigDecimal.valueOf(0);
                break;
        }

        return taxValue;
    }

    private static BigDecimal calculateVATInternal(BigDecimal price, double percent) {
        var taxValue = price.multiply(BigDecimal.valueOf(percent).divide(BigDecimal.valueOf(100), RoundingMode.CEILING));
        return taxValue;
    }
}
