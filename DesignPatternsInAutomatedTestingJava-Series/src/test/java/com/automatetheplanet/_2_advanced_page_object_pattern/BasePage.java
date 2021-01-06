package com.automatetheplanet._2_advanced_page_object_pattern;

public abstract class BasePage<TM extends BasePageElements, TV extends BasePageAsserts> {
    protected final String url;

    public BasePage(String url) {
        this.url = url;
    }

    public BasePage() {
        this.url = null;
    }

    protected abstract TM elements();

    public void navigate(String part) {
        Driver.getBrowser().navigate().to(url.concat(part));
    }

    public void navigate() {
        Driver.getBrowser().navigate().to(url);
    }

    protected abstract TV validate();

}
