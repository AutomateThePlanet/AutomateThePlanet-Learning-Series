package tipstricks;

import org.apache.tools.ant.util.FileUtils;
import org.openqa.selenium.*;
import org.openqa.selenium.chrome.*;
import org.openqa.selenium.firefox.FirefoxProfile;
import org.openqa.selenium.firefox.ProfilesIni;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Set;
import java.util.concurrent.TimeUnit;

import static java.lang.System.getProperty;

public class TipsTricksTests {
    private WebDriver driver;
    private WebDriverWait wait;

    @BeforeClass
    public void testSetup() throws IOException {
        System.setProperty("webdriver.chrome.driver", "resources\\chromedriver.exe");

        // 5. Execute tests in headless Chrome
          ChromeOptions chromeOptions = new ChromeOptions();
//        chromeOptions.addArguments("--headless", "--disable-gpu", "--window-size=1920,1200","--ignore-certificate-errors");
//        driver = new ChromeDriver(chromeOptions);
        // 22. Set HTTP Proxy ChromeDriver
        // var proxy = new Proxy();
        // proxy.setProxyType(Proxy.ProxyType.MANUAL);
        // proxy.setAutodetect(false);
        // proxy.setSslProxy("127.0.0.1:3239");
        // chromeOptions.setProxy(proxy);

        // 23. Set HTTP Proxy with Authentication ChromeDriver
        // chromeOptions.addArguments("--proxy-server=http://user:password@127.0.0.1:3239");

        // 24. tart ChromeDriver with an Packed Extension
        // chromeOptions.addArguments("load-extension=/pathTo/extension");

        // 25. Start ChromeDriver with an Unpacked Extension
        // chromeOptions.addExtensions(new File("local/path/to/extension.crx"));

        // 29. Verify File Downloaded ChromeDriver
        // String downloadFilepath = "c:\\temp";
//        HashMap<String, Object> chromePrefs = new HashMap<>();
//        chromePrefs.put("profile.default_content_settings.popups", 0);
//        chromePrefs.put("download.default_directory", downloadFilepath);
//        chromeOptions.setExperimentalOption("prefs", chromePrefs);
//        chromeOptions.addArguments("--test-type");
//        chromeOptions.addArguments("start-maximized", "disable-popup-blocking");

        // 18.2. Handle SSL Certificate Error ChromeDrive
        // chromeOptions.addArguments("--ignore-certificate-errors");

        // 7. Use Specific Profile in Chrome
        // option.addArguments("user-data-dri=C:\\Users\\Your path to user\\Roaming\\Google\\Chrome\\User Data");
        driver = new ChromeDriver();
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);

        // 10. Maximize Window
        driver.manage().window().maximize();

        // 4. Set Page Load Timeout
        // driver.manage().timeouts().pageLoadTimeout(10, TimeUnit.SECONDS);
        wait = new WebDriverWait(driver, 30);

        // 7. Use Specific Profile in Firefox
        ProfilesIni profile = new ProfilesIni();
        FirefoxProfile firefoxProfile = profile.getProfile("xyzProfile");
        // FirefoxOptions firefoxOptions = new FirefoxOptions();

        // 8. Turn Off JavaScript
        // firefoxProfile.setPreference("javascript.enabled", false);

        // 16. Change User Agent
        // firefoxProfile.setPreference("general.useragent.override", "Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+");

        // 17. Set HTTP Proxy for Browser
        // firefoxProfile.setPreference("network.proxy.type", 1);
        // firefoxProfile.setPreference("network.proxy.http", "myproxy.com");
        // firefoxProfile.setPreference("network.proxy.http_port", 3239);

        // 18. Handle SSL Certificate Error FirefoxDriver

        // firefoxProfile.setAcceptUntrustedCertificates(true);
        // firefoxProfile.setAssumeUntrustedCertificateIssuer(false);

        // 21. Start FirefoxDriver with Plugins
        // firefoxProfile.addExtension(new File("C:\\extensionsLocation\\extension.xpi"));

        // 30. Verify File Downloaded FirefoxDriver
//        String downloadFilepath = "c:\\temp";
//        firefoxProfile.setPreference("browser.download.folderList", 2);
//        firefoxProfile.setPreference("browser.download.dir", downloadFilepath);
//        firefoxProfile.setPreference("browser.download.manager.alertOnEXEOpen", false);
//        firefoxProfile.setPreference("browser.helperApps.neverAsk.saveToDisk", "application/msword, application/binary, application/ris, text/csv, image/png, application/pdf, text/html, text/plain, application/zip, application/x-zip, application/x-zip-compressed, application/download, application/octet-stream"));

        // firefoxOptions.setProfile(firefoxProfile);
        // WebDriver firefoxDriver = new FirefoxDriver(firefoxOptions);
    }

    @AfterClass
    public void afterClass() throws InterruptedException {
        driver.quit();
    }

    @Test
    public void takeFullScreenshot_test() throws Exception {
        driver.navigate().to("http://automatetheplanet.com");
        takeFullScreenshot("testImage");
    }

    @Test
    public void takeElementScreenshot_test() throws Exception {
        driver.navigate().to("http://automatetheplanet.com");
        var element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("/html/body/div[1]/header/div/div[2]/div/div[2]/nav")));
        takeScreenshotOfElement(element, "testElementImage");
    }

    // 2. Get HTML Source of WebElement
    @Test
    public void getHtmlSourceOfWebElement() {
        driver.navigate().to("http://automatetheplanet.com");
        var element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("/html/body/div[1]/header/div/div[2]/div/div[2]/nav")));

        String sourceHtml = element.getAttribute("innerHTML");
        System.out.println(sourceHtml);
    }

    // 3. Execute JavaScript
    @Test
    public void executeJavaScript() {
        driver.navigate().to("http://automatetheplanet.com");

        JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
        String title = (String) javascriptExecutor.executeScript("return document.title");

        // 4. Visibility of all elements wait
        // wait.until(ExpectedConditions.visibilityOfAllElements(driver.findElements(By.xpath("//*[@id='tve_editor']/div[2]/div[2]/div/div"))));

        System.out.println(title);
    }

    // 6. Check If an Element Is Visible
    @Test
    public void checkIfElementIsVisible() {
        driver.navigate().to("http://automatetheplanet.com");
        var element = driver.findElement(By.xpath("/html/body/div[1]/header/div/div[2]/div/div[2]/nav"));
        Assert.assertTrue(element.isDisplayed());
    }

    @Test
    public void manageCookies() {
        driver.navigate().to("http://automatetheplanet.com");

        // get all cookies
        var cookies = driver.manage().getCookies();
        for (Cookie cookie:cookies) {
            System.out.println(cookie.getName());
        }

        // get a cookie by name
        var fbPixelCookie = driver.manage().getCookieNamed("_fbp");

        // create a new cookie by name
        Cookie newCookie = new Cookie("customName", "customValue");
        driver.manage().addCookie(newCookie);

        // delete a cookie
        driver.manage().deleteCookie(fbPixelCookie);

        // delete a cookie by name
        driver.manage().deleteCookieNamed("customName");

        // delete all cookies
        driver.manage().deleteAllCookies();
    }

    // 11. Drag and Drop
    @Test
    public void dragAndDrop() {
        driver.navigate().to("http://loopj.com/jquery-simple-slider/");
        var element = driver.findElement(By.xpath("//*[@id='project']/p[1]/div/div[2]"));
        Actions action = new Actions(driver);
        action.dragAndDropBy(element, 30, 0).build().perform();
    }

    // 12. Upload a File
    @Test
    public void fileUpload() throws IOException {
        driver.navigate().to("https://demos.telerik.com/aspnet-ajax/ajaxpanel/application-scenarios/file-upload/defaultcs.aspx");
        var element = driver.findElement(By.id("ctl00_ContentPlaceholder1_RadUpload1file0"));

        String filePath = Paths.get(getProperty("java.io.tmpdir"), "debugWebDriver.xml").toString();
        File destFile = new File(filePath);
        destFile.createNewFile();

        element.sendKeys(filePath);
    }

    // 13. Handle JavaScript Pop-ups
    @Test
    public void handleJavaScripPopUps() {
        driver.navigate().to("http://www.w3schools.com/js/tryit.asp?filename=tryjs_confirm");
        driver.switchTo().frame("iframeResult");
        var button = driver.findElement(By.xpath("/html/body/button"));
        button.click();
        Alert alert = driver.switchTo().alert();

        if (alert.getText().equals("Press a button!")) {
            alert.accept();
        }
        else {
            alert.dismiss();
        }
    }

    // 14. Switch Between Browser Windows or Tabs
    @Test
    public void movingBetweenTabs() {
        driver.navigate().to("https://www.automatetheplanet.com/");
        var firstLink = driver.findElement(By.xpath("//*[@id='menu-item-11362']/a"));
        var secondLink = driver.findElement(By.xpath("//*[@id='menu-item-6']/a"));
        String selectLinkOpenninNewTab = Keys.chord(Keys.CONTROL,Keys.RETURN);
        firstLink.sendKeys(selectLinkOpenninNewTab);
        secondLink.sendKeys(selectLinkOpenninNewTab);
        Set<String> windows = driver.getWindowHandles();
        String firstTab = (String)windows.toArray()[1];
        String lastTab = (String)windows.toArray()[2];
        driver.switchTo().window(lastTab);

        Assert.assertEquals("Resources - Automate The Planet", driver.getTitle());

        driver.switchTo().window(firstTab);

        Assert.assertEquals("Blog - Automate The Planet", driver.getTitle());
    }

    // 15. Navigation History
    @Test
    public void navigationHistory() {
        driver.navigate().to("https://www.codeproject.com/Articles/1078541/Advanced-WebDriver-Tips-and-Tricks-Part");
        driver.navigate().to("http://www.codeproject.com/Articles/1017816/Speed-up-Selenium-Tests-through-RAM-Facts-and-Myth");

        driver.navigate().back();

        Assert.assertEquals("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.getTitle());

        driver.navigate().refresh();

        Assert.assertEquals("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.getTitle());

        driver.navigate().forward();

        Assert.assertEquals("Speed up Selenium Tests through RAM Facts and Myths - CodeProject", driver.getTitle());
    }

    // 19. Scroll Focus to Control
    @Test
    public void scrollFocusToControl() {
        driver.navigate().to("http://automatetheplanet.com/");

        var ourMissionLink = driver.findElement(By.xpath("//*[@id=\"panel-6435-0-0-4\"]/div"));
        String jsToBeExecuted = String.format("window.scroll(0, {0});", ourMissionLink.getLocation().getY());
        JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
        javascriptExecutor.executeScript(jsToBeExecuted);
    }

    // 20. Focus on a Control
    @Test
    public void focusOnControl() {
        driver.navigate().to("http://automatetheplanet.com/");

        waitUntilLoaded();
        var ourMissionLink = driver.findElement(By.xpath("//*[@id=\"panel-6435-0-0-4\"]/div"));

        Actions action = new Actions(driver);
        action.moveToElement(ourMissionLink).build().perform();
    }

    // 26. Assert a Button Enabled or Disabled
    @Test
    public void assertButtonEnabledDisabled() {
        driver.navigate().to("http://www.w3schools.com/tags/tryit.asp?filename=tryhtml_button_disabled");
        driver.switchTo().frame("iframeResult");
        var button  = driver.findElement(By.xpath("/html/body/button"));

        Assert.assertFalse(button.isEnabled());
    }

    // 27. Set and Assert the Value of a Hidden Field
    @Test
    public void setHiddenField() {
        //<input type="hidden" name="country" value="Bulgaria"/>

        var theHiddenElem   = driver.findElement(By.name("country"));
        JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
        javascriptExecutor.executeScript("arguments[0].value='Germany';", theHiddenElem);
        String hiddenFieldValue = theHiddenElem.getAttribute("value");

        Assert.assertEquals("Germany", hiddenFieldValue);
    }

    // 29. Verify File Downloaded ChromeDriver
    @Test
    public void VerifyFileDownloadChrome() throws IOException {
        var expectedFilePath = Paths.get("c:\\temp\\Testing_Framework_2015_3_1314_2_Free.exe");
        try
        {
            driver.navigate().to("https://www.telerik.com/download-trial-file/v2/telerik-testing-framework");
            wait.until(x -> Files.exists(expectedFilePath));
            long bytes = Files.size(expectedFilePath);

            Assert.assertEquals(4326192, bytes);
        }
        finally
        {
            if (Files.exists(expectedFilePath))
            {
                Files.delete(expectedFilePath);
            }
        }
    }

    // 1. Taking a Screenshot
    public void takeFullScreenshot(String fileName) throws Exception {
        File srcFile = ((TakesScreenshot) driver).getScreenshotAs(OutputType.FILE);

        String tempDir = getProperty("java.io.tmpdir");
        File destFile = new File(Paths.get(tempDir, fileName + ".png").toString());
        FileUtils.getFileUtils().copyFile(srcFile, destFile);
    }

    public void takeScreenshotOfElement(WebElement element, String fileName) throws Exception {
        File screenshotFile = ((TakesScreenshot) driver).getScreenshotAs(OutputType.FILE);
        BufferedImage fullImg = ImageIO.read(screenshotFile);
        Point point = element.getLocation();
        int elementWidth = element.getSize().getWidth();
        int elementHeight = element.getSize().getHeight();

        BufferedImage eleScreenshot = fullImg.getSubimage(point.getX(), point.getY(), elementWidth, elementHeight);
        ImageIO.write(eleScreenshot, "png", screenshotFile);

        String tempDir = getProperty("java.io.tmpdir");
        File destFile = new File(Paths.get(tempDir, fileName + ".png").toString());
        FileUtils.getFileUtils().copyFile(screenshotFile, destFile);
    }

    // 4. Set Page Load Timeout
    private void waitUntilLoaded() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            String isReady = (String) javascriptExecutor.executeScript("return document.readyState");
            return isReady.equals("complete");
        });
    }

    // 28. Wait AJAX Call to Complete Using JQuery
    private void waitForAjaxComplete() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            Boolean isAjaxCallComplete  = (Boolean) javascriptExecutor.executeScript("return window.jQuery != undefined && jQuery.active == 0");
            return isAjaxCallComplete ;
        });
    }
}