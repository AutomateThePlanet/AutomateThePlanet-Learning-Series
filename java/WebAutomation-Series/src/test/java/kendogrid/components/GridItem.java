package kendogrid.components;

public class GridItem {
    private String contactName;
    private String contactTitle;
    private String companyName;
    private String country;

    public GridItem(String contactName, String contactTitle, String companyName, String country) {
        this.contactName = contactName;
        this.contactTitle = contactTitle;
        this.companyName = companyName;
        this.country = country;
    }

    public String getContactName() {
        return contactName;
    }

    public void setContactName(String contactName) {
        this.contactName = contactName;
    }

    public String getContactTitle() {
        return contactTitle;
    }

    public void setContactTitle(String contactTitle) {
        this.contactTitle = contactTitle;
    }

    public String getCompanyName() {
        return companyName;
    }

    public void setCompanyName(String companyName) {
        this.companyName = companyName;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }
}
