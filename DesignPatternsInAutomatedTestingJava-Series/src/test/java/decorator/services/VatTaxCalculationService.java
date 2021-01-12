package decorator.services;

import decorator.enums.Country;

public class VatTaxCalculationService {
    double taxValue;
    public double calculate(double price, Country country) {
        switch(country) {
            case BULGARIA:
            case UNITED_KINGDOM:
            case GERMANY:
            case AUSTRIA:
            case FRANCE:
                taxValue = calculateVATInternal(price, 20);
                break;
            default:
                taxValue = 0;
                break;
        }

        return taxValue;
    }

    private static double calculateVATInternal(double price, double percent) {
        var taxValue = price*percent/100;
        return taxValue;
    }
}
