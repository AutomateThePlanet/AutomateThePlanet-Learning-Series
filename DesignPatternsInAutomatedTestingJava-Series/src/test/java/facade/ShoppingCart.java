package facade;

import facade.data.PurchaseInfo;
import facade.pages.interfaces.CheckoutPage;
import facade.pages.interfaces.ItemPage;
import facade.pages.interfaces.ShoppingCartPage;

public class ShoppingCart {
    private final ItemPage itemPage;
    private final ShoppingCartPage shoppingCartPage;
    private final CheckoutPage checkoutPage;

    public ShoppingCart(ItemPage itempage, ShoppingCartPage shoppingCartPage, CheckoutPage checkoutPage) {
        this.itemPage = itempage;
        this.shoppingCartPage = shoppingCartPage;
        this.checkoutPage = checkoutPage;
    }

    public void purchaseItem(String itemUrl, double itemPrice, PurchaseInfo purchaseInfo) {
        itemPage.navigate(itemUrl);
        itemPage.assertPrice(itemPrice);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        shoppingCartPage.assertSubtotalAmount(itemPrice);
        checkoutPage.fillBillingInfo(purchaseInfo);
        checkoutPage.assertSubtotal(itemPrice);
    }
}
