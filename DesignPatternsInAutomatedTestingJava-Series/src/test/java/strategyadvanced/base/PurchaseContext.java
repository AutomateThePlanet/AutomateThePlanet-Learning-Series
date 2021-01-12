package strategyadvanced.base;

import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.pages.itempage.ItemPage;
import strategyadvanced.pages.shoppingcartpage.ShoppingCartPage;

public class PurchaseContext {
    private final OrderPurchaseStrategy[] orderPurchaseStrategies;

    public PurchaseContext(OrderPurchaseStrategy... orderPurchaseStrategies) {
        this.orderPurchaseStrategies = orderPurchaseStrategies;
    }

    public void PurchaseItem(String itemUrl, double itemPrice, PurchaseInfo purchaseInfo) {
        validatePurchaseInfo(purchaseInfo);

        var itemPage = new ItemPage();
        var shoppingCartPage = new ShoppingCartPage();
        var checkoutPage = new CheckoutPage();
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
