package decorator.core;

import java.util.HashMap;
import java.util.Map;

// Based on http://neutrofoton.github.io/blog/2013/08/29/generic-singleton-pattern-in-java/
// Can be used inside App design pattern.
public class SingletonFactory {
    private static final SingletonFactory instance = new SingletonFactory();

    private final Map<String,Object> mapHolder = new HashMap<>();

    private SingletonFactory() {}

    @SuppressWarnings("unchecked")
    public static <T> T getInstance(Class<T> classOf) {
        try {
            if(!instance.mapHolder.containsKey(classOf.getName())) {
                T obj = (T) classOf.getConstructors()[0].newInstance();

                instance.mapHolder.put(classOf.getName(), obj);
            }
            return (T) instance.mapHolder.get(classOf.getName());
        } catch (Exception e) {
            // not the best practice to return null. But probably we will never end here so it is OK.
            return null;
        }
    }
}