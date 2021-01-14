package facade;

import facade.core.Driver;
import facade.data.PurchaseInfo;
import facade.pages.bellatrixdemoscheckoutpage.BellatrixDemosCheckoutPage;
import facade.pages.bellatrixdemositempage.BellatrixDemoItemPage;
import facade.pages.bellatrixdemosshoppingcartpage.BellatrixDemoShoppingCartPage;
import io.github.bonigarcia.wdm.WebDriverManager;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

public class StorePurchaseWithFacadeTests {
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
    public void purchaseFalcon9_UsingFacadePattern() {
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
        var itemPage = new BellatrixDemoItemPage();
        var shoppingCartPage = new BellatrixDemoShoppingCartPage();
        var checkoutPage = new BellatrixDemosCheckoutPage();

        new ShoppingCart(itemPage, shoppingCartPage, checkoutPage).purchaseItem(itemUrl, itemPrice, purchaseInfo);
    }

    @Test
    public void purchaseSaturnV_UsingFacadePattern() {
        var itemUrl = "saturn-v";
        var itemPrice = 120.00;
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
        var itemPage = new BellatrixDemoItemPage();
        var shoppingCartPage = new BellatrixDemoShoppingCartPage();
        var checkoutPage = new BellatrixDemosCheckoutPage();

        new ShoppingCart(itemPage, shoppingCartPage, checkoutPage).purchaseItem(itemUrl, itemPrice, purchaseInfo);
    }
}
