package pageobjectadvanced.core;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
    @SuppressWarnings("unchecked")
    protected ElementsT elements() {
        return NewInstanceFactory.createByTypeParameter(getClass(), 0);
    }
}
