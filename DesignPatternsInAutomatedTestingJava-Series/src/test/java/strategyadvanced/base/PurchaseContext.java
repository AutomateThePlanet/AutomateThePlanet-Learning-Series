package strategyadvanced.base;

import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.pages.itempage.ItemPage;
import strategyadvanced.pages.shoppingcartpage.ShoppingCartPage;

public class PurchaseContext {
    private final OrderPurchaseStrategy[] orderPurchaseStrategies;
    private final ItemPage itemPage;
    private final ShoppingCartPage shoppingCartPage;
    private final CheckoutPage checkoutPage;

    public PurchaseContext(OrderPurchaseStrategy... orderPurchaseStrategies) {
        this.orderPurchaseStrategies = orderPurchaseStrategies;
        itemPage = new ItemPage();
        shoppingCartPage = new ShoppingCartPage();
        checkoutPage = new CheckoutPage();
    }

    public void purchaseItem(String itemUrl, double itemPrice, PurchaseInfo purchaseInfo) {
        validatePurchaseInfo(purchaseInfo);
        itemPage.navigate(itemUrl);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        checkoutPage.fillBillingInfo(purchaseInfo);

        validateOrderSummary(itemPrice, purchaseInfo);
    }

    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        for (var currentStrategy : orderPurchaseStrategies) {
            currentStrategy.validatePurchaseInfo(purchaseInfo);
        }
    }

    public void validateOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        for (var currentStrategy : orderPurchaseStrategies) {
            currentStrategy.assertOrderSummary(itemPrice, purchaseInfo);
        }
    }
}
