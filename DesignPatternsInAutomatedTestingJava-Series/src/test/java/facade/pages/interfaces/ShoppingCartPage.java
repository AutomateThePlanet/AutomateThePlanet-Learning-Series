package facade.pages.interfaces;

public interface ShoppingCartPage {
    void clickProceedToCheckoutButton();
    void assertSubtotalAmount(double itemPrice);
}
