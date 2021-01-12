package pageobjectadvanced.core;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
    @SuppressWarnings("unchecked")
    protected ElementsT elements() {
        return (ElementsT) ReflectionNewInstanceFactory.getTypeParameter(0, getClass());
    }
}
