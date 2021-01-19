package torintegration;

import org.openqa.selenium.*;
import org.openqa.selenium.firefox.*;
import org.openqa.selenium.support.ui.*;
import org.testng.Assert;
import org.testng.annotations.*;

import java.io.*;
import java.net.*;
import java.util.concurrent.TimeUnit;

public class TorTests {
    private WebDriver driver;
    private WebDriverWait wait;
    private Process torProcess;

    @BeforeClass
    public void testSetup() throws IOException {
        System.setProperty("webdriver.gecko.driver", "resources\\geckodriver.exe");


        String torBinaryPath = "C:\\Users\\aangelov\\Desktop\\Tor Browser\\Browser\\firefox.exe";
        Runtime runTime = Runtime.getRuntime();
        torProcess = runTime.exec(torBinaryPath + " -n");

        FirefoxProfile profile = new FirefoxProfile();
        profile.setPreference("network.proxy.type", 1);
        profile.setPreference("network.proxy.socks", "127.0.0.1");
        profile.setPreference("network.proxy.socks_port", 9150);

        FirefoxOptions firefoxOptions = new FirefoxOptions();
        firefoxOptions.setProfile(profile);

        driver = new FirefoxDriver(firefoxOptions);
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        wait = new WebDriverWait(driver, 30);
    }

    @AfterClass
    public void afterClass() throws InterruptedException {
        driver.quit();
        torProcess.descendants().forEach(ph -> {
            ph.destroy();
        });
        torProcess.destroyForcibly();
    }

    @Test
    public void open_tor_browser() {
        refreshTorIdentity("johnsmith");

        driver.navigate().to("http://whatismyipaddress.com/");

        WebElement element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("//*[@id='section_left']/div[2]")));

        Assert.assertNotEquals("151.80.16.169", element.getText());
    }

    public void refreshTorIdentity(String userName) {
        try (Socket socket = new Socket("127.0.0.1", 9151)) {
            OutputStream output = socket.getOutputStream();
            String authenticationCommand = String.format("AUTHENTICATE \"%s\"\r\n", userName);
            output.write(authenticationCommand.getBytes());
            output.write("SIGNAL NEWNYM\r\n".getBytes());
            InputStream input = socket.getInputStream();
            BufferedReader reader = new BufferedReader(new InputStreamReader(input));
            String line = reader.readLine();

            if (!line.contains("250")) {
                System.out.println("Unable to signal new user to server.");
            }
        } catch (UnknownHostException ex) {
            System.out.println("Server not found: " + ex.getMessage());
        } catch (IOException ex) {
            System.out.println("I/O error: " + ex.getMessage());
        }
    }
}