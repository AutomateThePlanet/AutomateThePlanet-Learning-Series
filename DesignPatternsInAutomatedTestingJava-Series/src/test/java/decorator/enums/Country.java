package decorator.enums;

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
