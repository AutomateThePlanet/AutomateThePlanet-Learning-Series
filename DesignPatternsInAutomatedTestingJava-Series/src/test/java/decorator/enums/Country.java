package decorator.enums;

import org.apache.commons.lang3.StringUtils;

import java.util.regex.Pattern;

public enum Country {
    BULGARIA ("Bulgaria"),
    UNITED_KINGDOM ("United Kingdom"),
    GERMANY ("Germany"),
    AUSTRIA ("Austria"),
    FRANCE ("France"),
    CHINA ("China");

    Country(String name) {
        this.name = name;
    }

    String name;

    @Override
    public String toString() {
        return name;
    }
}
