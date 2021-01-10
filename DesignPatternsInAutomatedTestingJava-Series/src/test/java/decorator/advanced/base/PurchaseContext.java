package decorator.advanced.base;

import decorator.advanced.strategies.OrderPurchaseStrategy;
import decorator.core.Driver;
import decorator.data.PurchaseInfo;
import decorator.pages.checkoutpage.CheckoutPage;
import decorator.pages.itempage.ItemPage;
import decorator.pages.shoppingcartpage.ShoppingCartPage;

import java.math.BigDecimal;

public class PurchaseContext {
    private final OrderPurchaseStrategy orderPurchaseStrategy;

    public PurchaseContext(OrderPurchaseStrategy orderPurchaseStrategy) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
    }

    public void PurchaseItem(String itemUrl, BigDecimal itemPrice, PurchaseInfo clientPurchaseInfo) {
        Driver.getPage(ItemPage.class).open(itemUrl);
        Driver.getPage(ItemPage.class).clickBuyNowButton();
        Driver.getPage(ItemPage.class).clickViewShoppingCartButton();
        Driver.getPage(ShoppingCartPage.class).clickProceedToCheckoutButton();
        Driver.getPage(CheckoutPage.class).fillBillingInfo(clientPurchaseInfo);
        var expectedTotalPrice = orderPurchaseStrategy.calculateTotalPrice();
        orderPurchaseStrategy.validateOrderSummary(expectedTotalPrice);
    }
}
