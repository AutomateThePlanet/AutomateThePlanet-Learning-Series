package decorator.core;

import java.lang.reflect.ParameterizedType;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
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
}
