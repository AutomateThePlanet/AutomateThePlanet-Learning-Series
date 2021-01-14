package facade.pages.interfaces;

import facade.data.PurchaseInfo;

public interface CheckoutPage {
    void navigate(String url);
    void navigate();
    void fillBillingInfo(PurchaseInfo purchaseInfo);
    void assertSubtotal(double itemPrice);
}
