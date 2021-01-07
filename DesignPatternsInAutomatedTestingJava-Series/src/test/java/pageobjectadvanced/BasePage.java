package pageobjectadvanced;

public abstract class BasePage<ElementsТ extends BasePageElements, AssertsТ extends BasePageAsserts> {
    protected final String url;

    public BasePage(String url) {
        this.url = url;
    }

    public BasePage() {
        this.url = null;
    }

    protected abstract ElementsТ elements();

    public void navigate(String part) {
        Driver.getBrowser().navigate().to(url.concat(part));
    }

    public void navigate() {
        Driver.getBrowser().navigate().to(url);
    }

    protected abstract AssertsТ asserts();
}
