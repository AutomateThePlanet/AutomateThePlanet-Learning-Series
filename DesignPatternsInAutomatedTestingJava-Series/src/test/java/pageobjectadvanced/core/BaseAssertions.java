package pageobjectadvanced.core;

import java.lang.reflect.ParameterizedType;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
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
}
