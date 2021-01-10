package decorator.core;

import java.lang.reflect.ParameterizedType;

public abstract class BasePage<ElementsT extends BaseElements, AssertionsT extends BaseAssertions<ElementsT>> {
    protected final String url;

    public BasePage(String url) {
        this.url = url;
    }

    @SuppressWarnings("unchecked")
    protected ElementsT elements() {
        try {
            var elementsClass = (Class<ElementsT>) ((ParameterizedType) getClass()
                    .getGenericSuperclass()).getActualTypeArguments()[0];
            return Driver.getElements(elementsClass);
        } catch (Exception e) {
            return null;
        }
    }

    public void open(String part) {
        Driver.getBrowser().navigate().to(url.concat(part));
    }

    public void open() {
        Driver.getBrowser().navigate().to(url);
    }

    @SuppressWarnings("unchecked")
    public AssertionsT assertions() {
        try {
            var assertionsClass = (Class<AssertionsT>) ((ParameterizedType) getClass()
                    .getGenericSuperclass()).getActualTypeArguments()[1];
            return Driver.getAssertions(assertionsClass);
        } catch (Exception e) {
            return null;
        }
    }
}
