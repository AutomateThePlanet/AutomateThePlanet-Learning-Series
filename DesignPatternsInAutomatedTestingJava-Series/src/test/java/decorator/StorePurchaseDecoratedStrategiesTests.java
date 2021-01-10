package decorator;

import decorator.advanced.base.PurchaseContext;
import decorator.advanced.strategies.CouponCodeOrderPurchaseStrategy;
import decorator.advanced.strategies.OrderPurchaseStrategy;
import decorator.advanced.strategies.TotalPriceOrderPurchaseStrategy;
import decorator.advanced.strategies.VatTaxOrderPurchaseStrategy;
import decorator.core.Driver;
import decorator.data.PurchaseInfo;
import decorator.enums.Country;
import decorator.pages.checkoutpage.CheckoutPage;
import io.github.bonigarcia.wdm.WebDriverManager;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.math.BigDecimal;
import java.util.Arrays;

public class StorePurchaseDecoratedStrategiesTests {
    @BeforeClass
    public static void classInit() {
        WebDriverManager.firefoxdriver().setup();
    }

    @BeforeMethod
    public void testInit() {
        Driver.startBrowser();
    }

    @AfterMethod
    public void testCleanup() {
        Driver.stopBrowser();
    }

    @Test
    public void Purchase_Falcon9_DecoratedStrategies() {
        var itemUrl = "falcon-9";
        var itemPrice = BigDecimal.valueOf(50.00);
        var purchaseInfo = new PurchaseInfo();
        purchaseInfo.setEmail("info@berlinspaceflowers.com");
        purchaseInfo.setFirstName("Anton");
        purchaseInfo.setLastName("Angelov");
        purchaseInfo.setCompany("Space Flowers");
        purchaseInfo.setCountry("Germany");
        purchaseInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        purchaseInfo.setAddress2("Lьtzowplatz 17");
        purchaseInfo.setCity("Berlin");
        purchaseInfo.setZip("10115");
        purchaseInfo.setPhone("+491888999281");
        purchaseInfo.setCouponCode("happybirthday");

        OrderPurchaseStrategy orderPurchaseStrategy = new TotalPriceOrderPurchaseStrategy(itemPrice);
        orderPurchaseStrategy = new CouponCodeOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, purchaseInfo);
        orderPurchaseStrategy = new VatTaxOrderPurchaseStrategy(orderPurchaseStrategy, orderPurchaseStrategy.calculateTotalPrice(), purchaseInfo);

        new PurchaseContext(orderPurchaseStrategy).PurchaseItem(itemUrl, itemPrice, purchaseInfo);
    }
}
