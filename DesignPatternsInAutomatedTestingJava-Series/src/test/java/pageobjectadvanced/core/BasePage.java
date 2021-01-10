package pageobjectadvanced.core;

import java.lang.reflect.ParameterizedType;

public class BasePage<ElementsT extends BaseElements, AssertionsT extends BaseAssertions<ElementsT>> {
    protected final String url;

    public BasePage(String url) {
        this.url = url;
    }

    @SuppressWarnings("unchecked")
    protected ElementsT elements() {
        try {
            var elementsClass = (Class<ElementsT>) ((ParameterizedType) getClass()
                    .getGenericSuperclass()).getActualTypeArguments()[0];
            return elementsClass.getDeclaredConstructor().newInstance();
        } catch (Exception e) {
            return null;
        }
    }

    public void navigate(String part) {
        Driver.getBrowser().navigate().to(url.concat(part));
    }

    public void navigate() {
        Driver.getBrowser().navigate().to(url);
    }

    @SuppressWarnings("unchecked")
    public AssertionsT assertions() {
        try {
            var assertionsClass = (Class<AssertionsT>) ((ParameterizedType) getClass()
                    .getGenericSuperclass()).getActualTypeArguments()[1];
            return assertionsClass.getDeclaredConstructor().newInstance();
        } catch (Exception e) {
            return null;
        }
    }
}
