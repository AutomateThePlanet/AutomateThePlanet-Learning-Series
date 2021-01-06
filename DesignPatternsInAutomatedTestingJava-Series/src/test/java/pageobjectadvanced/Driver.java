package pageobjectadvanced;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class Driver {
    private static WebDriverWait browserWait;
    private static WebDriver browser;

    public static WebDriver getBrowser() {
        if (browser == null) {
            throw new NullPointerException("The WebDriver browser instance was not initialized. You should first call the start() method.");
        }
        return browser;
    }

    public static void setBrowser(WebDriver browser) {
        Driver.browser = browser;
    }

    public static WebDriverWait getBrowserWait() {
        if (browserWait == null || browser == null) {
            throw new NullPointerException("The WebDriver browser wait instance was not initialized. You should first call the start() method.");
        }
        return browserWait;
    }

    public static void setBrowserWait(WebDriverWait browserWait) {
        Driver.browserWait = browserWait;
    }

    public static void startBrowser(BrowserType browserType, int defaultTimeout) {

        switch (browserType) {
            case FIREFOX:
                WebDriverManager.firefoxdriver().setup();
                setBrowser(new FirefoxDriver());
                break;
            case CHROME:
                //setBrowser(new ChromeDriver());
                break;
            case EDGE:
                //setBrowser(new EdgeDriver());
                break;
            default:
                break;
        }

        setBrowserWait(new WebDriverWait(getBrowser(), defaultTimeout));
    }

    public static void startBrowser(BrowserType browserType) {
        startBrowser(browserType, 30);
    }

    public static void startBrowser() {
        startBrowser(BrowserType.FIREFOX);
    }

    public static void stopBrowser() {
        getBrowser().quit();
        setBrowser(null);
        setBrowserWait(null);
    }
}
