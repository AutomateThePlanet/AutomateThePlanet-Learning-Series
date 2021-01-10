package decorator.pages.checkoutpage;

import decorator.core.BasePage;
import decorator.core.Driver;
import decorator.data.PurchaseInfo;
import org.openqa.selenium.*;

public class CheckoutPage extends BasePage<CheckoutElements, CheckoutAssertions> {
    public CheckoutPage() {
        super("http://demos.bellatrix.solutions/checkout/");
    }

    private void clickPlaceOrderButton() throws InterruptedException {
        for (int i = 0; i < 10; i++) {
            try {
                elements().placeOrderButton().click();
            } catch (ElementClickInterceptedException e) {
                Thread.sleep(500);
            }
        }
    }

    public void fillBillingInfo(PurchaseInfo purchaseInfo) {
        if (purchaseInfo.getCouponCode() != null) {
            elements().couponCodeShowInputButton().click();
            Driver.retry(2,500,()->elements().couponCodeInput().sendKeys(purchaseInfo.getCouponCode()));
            elements().couponCodeApplyButton().click();
        }
        elements().billingFirstName().sendKeys(purchaseInfo.getFirstName());
        elements().billingLastName().sendKeys(purchaseInfo.getLastName());
        elements().billingCompany().sendKeys(purchaseInfo.getCompany());
        elements().billingCountryWrapper().click();
        elements().billingCountryFilter().sendKeys(purchaseInfo.getCountry());
        elements().getCountryOptionByName(purchaseInfo.getCountry()).click();
        elements().billingAddress1().sendKeys(purchaseInfo.getAddress1());
        elements().billingAddress2().sendKeys(purchaseInfo.getAddress2());
        elements().billingCity().sendKeys(purchaseInfo.getCity());
        elements().billingZip().sendKeys(purchaseInfo.getZip());
        elements().billingPhone().sendKeys(purchaseInfo.getPhone());
        elements().billingEmail().sendKeys(purchaseInfo.getEmail());
        if (purchaseInfo.getShouldCreateAccount()) {
            elements().createAccountCheckBox().click();
        }

        if (purchaseInfo.getShouldCheckPayment()) {
            elements().checkPaymentsRadioButton().click();
        }

        Driver.retry(15,1000,()->elements().placeOrderButton().click());
    }
}
