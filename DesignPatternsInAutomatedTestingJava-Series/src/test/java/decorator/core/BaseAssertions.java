package decorator.core;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
    protected ElementsT elements() {
        return NewInstanceFactory.createByTypeParameter(getClass(), 0);
    }
}
