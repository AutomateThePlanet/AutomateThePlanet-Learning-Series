package strategyadvanced.core;

import pageobjectadvanced.core.ReflectionNewInstanceFactory;

public abstract class BasePage<ElementsT extends BaseElements, AssertionsT extends BaseAssertions<ElementsT>> {
    protected final String url;

    public BasePage(String url) {
        this.url = url;
    }

    @SuppressWarnings("unchecked")
    protected ElementsT elements() {
        return (ElementsT) ReflectionNewInstanceFactory.getTypeParameter(0, getClass());
    }

    public void navigate(String part) {
        Driver.getBrowser().navigate().to(url.concat(part));
    }

    public void navigate() {
        Driver.getBrowser().navigate().to(url);
    }

    @SuppressWarnings("unchecked")
    public AssertionsT assertions() {
        return (AssertionsT) ReflectionNewInstanceFactory.getTypeParameter(1, getClass());
    }
}
