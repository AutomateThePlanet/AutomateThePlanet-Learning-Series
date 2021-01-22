/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Teodor Nikolov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package facade.pages.bellatrixdemoscheckoutpage;

import facade.core.BasePage;
import facade.core.Driver;
import facade.data.PurchaseInfo;
import facade.pages.interfaces.CheckoutPage;
import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.ExpectedConditions;

public class BellatrixDemoCheckoutPage extends BasePage<BellatrixDemoCheckoutElements, BellatrixDemoCheckoutAssertions> implements CheckoutPage {
    @Override
    protected String getUrl() {
        return "http://demos.bellatrix.solutions/checkout/";
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
        Driver.getBrowserWait().until(ExpectedConditions.invisibilityOfElementLocated(By.xpath("//div[@class='blockUI blockOverlay']")));
        elements().placeOrderButton().click();
    }

    @Override
    public void assertSubtotal(double itemPrice) {
        assertions().assertOrderSubtotalPrice(itemPrice);
    }
}
