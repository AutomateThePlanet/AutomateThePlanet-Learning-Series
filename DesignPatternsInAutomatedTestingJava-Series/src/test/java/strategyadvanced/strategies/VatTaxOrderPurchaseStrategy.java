package strategyadvanced.strategies;

import strategyadvanced.base.OrderPurchaseStrategy;
import strategyadvanced.core.Driver;
import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.enums.Country;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.services.VatTaxCalculationService;

import java.util.Arrays;

public class VatTaxOrderPurchaseStrategy implements OrderPurchaseStrategy {
    private final VatTaxCalculationService vatTaxCalculationService;

    public VatTaxOrderPurchaseStrategy() {
        vatTaxCalculationService = new VatTaxCalculationService();
    }

    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var currentCountry = Arrays.stream(Country.values())
                .filter(country -> country.toString().equals(purchaseInfo.getCountry()))
                .toArray(Country[]::new)[0];
        var vatTax = vatTaxCalculationService.calculate(itemPrice, currentCountry, purchaseInfo);

        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderVatTaxPrice(vatTax);
    }

    @Override
    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        // Throw a new IllegalArgumentException if the country is not part of the EU Union.
    }
}
