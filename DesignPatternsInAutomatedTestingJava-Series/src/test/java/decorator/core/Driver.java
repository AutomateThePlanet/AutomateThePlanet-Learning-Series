package decorator.core;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebDriverException;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

import javax.swing.*;
import java.time.Duration;

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

    @SuppressWarnings("rawtypes")
    public static <PageT extends BasePage> PageT getPage(Class<PageT> pageClass) {
        return SingletonFactory.getInstance(pageClass);
    }

    public static <ElementsT extends BaseElements> ElementsT getElements(Class<ElementsT> elementsClass) {
        return SingletonFactory.getInstance(elementsClass);
    }

    @SuppressWarnings("rawtypes")
    public static <AssertionsT extends BaseAssertions> AssertionsT getAssertions(Class<AssertionsT> assertionsClass) {
        return SingletonFactory.getInstance(assertionsClass);
    }

    public static void retry(long forSeconds, long everyMilliseconds, Runnable runnable) {
        for (int i = 0; i < forSeconds*1000/everyMilliseconds; i++) {
            try {
                runnable.run();
                return;
            } catch (WebDriverException ignored) {
                try {
                    Thread.sleep(everyMilliseconds);
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    throw new RuntimeException(e);
                }
            }
        }
        runnable.run();
    }
}
