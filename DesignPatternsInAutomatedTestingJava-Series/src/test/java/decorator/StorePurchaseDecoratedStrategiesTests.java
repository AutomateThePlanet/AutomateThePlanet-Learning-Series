package decorator;

import decorator.base.PurchaseContext;
import decorator.core.Driver;
import decorator.data.PurchaseInfo;
import decorator.strategies.CouponCodeOrderPurchaseStrategy;
import decorator.strategies.OrderPurchaseStrategy;
import decorator.strategies.TotalPriceOrderPurchaseStrategy;
import decorator.strategies.VatTaxOrderPurchaseStrategy;
import io.github.bonigarcia.wdm.WebDriverManager;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

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
    public void purchaseFalcon9_UsingDecoratedStrategiesPattern() {
        var itemUrl = "falcon-9";
        var itemPrice = 50.00;
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
        orderPurchaseStrategy = new VatTaxOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, purchaseInfo);
        orderPurchaseStrategy = new CouponCodeOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, purchaseInfo);

        new PurchaseContext(orderPurchaseStrategy).purchaseItem(itemUrl, purchaseInfo);
    }
}
