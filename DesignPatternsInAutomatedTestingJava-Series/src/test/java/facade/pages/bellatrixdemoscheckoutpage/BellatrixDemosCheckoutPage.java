package facade.pages.bellatrixdemoscheckoutpage;

import facade.core.BasePage;
import facade.core.Driver;
import facade.data.PurchaseInfo;
import facade.pages.interfaces.CheckoutPage;
import org.openqa.selenium.support.ui.ExpectedConditions;

public class BellatrixDemosCheckoutPage extends BasePage<BellatrixDemoCheckoutElements, BellatrixDemoCheckoutAssertions> implements CheckoutPage {
    public BellatrixDemosCheckoutPage() {
        super("http://demos.bellatrix.solutions/checkout/");
    }

    @Override
    public void fillBillingInfo(PurchaseInfo purchaseInfo) {
        if (purchaseInfo.getCouponCode() != null) {
            elements().couponCodeShowInputButton().click();
            Driver.getBrowserWait().until(ExpectedConditions.elementToBeClickable(elements().couponCodeInput()));
            elements().couponCodeInput().sendKeys(purchaseInfo.getCouponCode());
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

        Driver.waitForAjax();
        Driver.getBrowserWait().until(ExpectedConditions.elementToBeClickable(elements().placeOrderButton()));
        elements().placeOrderButton().click();
    }

    @Override
    public void assertSubtotal(double itemPrice) {
        assertions().assertOrderSubtotalPrice(itemPrice);
    }
}
