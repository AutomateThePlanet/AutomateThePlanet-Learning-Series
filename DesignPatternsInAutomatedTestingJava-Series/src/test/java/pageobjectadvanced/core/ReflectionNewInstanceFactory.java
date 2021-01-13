package pageobjectadvanced.core;

import java.lang.reflect.ParameterizedType;

public class ReflectionNewInstanceFactory {
    @SuppressWarnings("unchecked, rawtypes")
    public static Object getTypeParameter(int index, Class getClass) {
        try {
            var elementsClass = (Class)((ParameterizedType) getClass
                    .getGenericSuperclass()).getActualTypeArguments()[index];
            return elementsClass.getDeclaredConstructor().newInstance();
        } catch (Exception e) {
            return null;
        }
    }
}
