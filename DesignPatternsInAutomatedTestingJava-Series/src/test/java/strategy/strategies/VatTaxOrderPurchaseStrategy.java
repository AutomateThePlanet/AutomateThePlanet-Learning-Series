package strategy.strategies;

import strategy.base.OrderPurchaseStrategy;
import strategy.data.PurchaseInfo;
import strategy.services.VatTaxCalculationService;
import strategy.core.Driver;
import strategy.enums.Country;
import strategy.pages.checkoutpage.CheckoutPage;

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
}
