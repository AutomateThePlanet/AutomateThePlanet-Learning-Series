package strategyadvanced.core;

import java.lang.reflect.ParameterizedType;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
    @SuppressWarnings("unchecked")
    protected ElementsT elements() {
        return (ElementsT)ReflectionNewInstanceFactory.getTypeParameter(0, getClass());
    }
}
