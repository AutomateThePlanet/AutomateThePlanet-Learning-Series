package facade.pages.interfaces;

public interface ItemPage {
    void navigate(String url);
    void clickBuyNowButton();
    void clickViewShoppingCartButton();
    void assertPrice(double itemPrice);
}
