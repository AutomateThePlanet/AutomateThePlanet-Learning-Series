package strategyadvanced.data;

public class PurchaseInfo {
    private String firstName;
    private String lastName;
    private String company;
    private String country;
    private String address1;
    private String address2;
    private String city;
    private String zip;
    private String phone;
    private String email;
    private Boolean shouldCreateAccount = false;
    private Boolean shouldCheckPayment = false;
    private String couponCode = null;

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getCompany() {
        return company;
    }

    public void setCompany(String company) {
        this.company = company;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public String getAddress1() {
        return address1;
    }

    public void setAddress1(String address1) {
        this.address1 = address1;
    }

    public String getAddress2() {
        return address2;
    }

    public void setAddress2(String address2) {
        this.address2 = address2;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getZip() {
        return zip;
    }

    public void setZip(String zip) {
        this.zip = zip;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Boolean getShouldCreateAccount() {
        return shouldCreateAccount;
    }

    public void setShouldCreateAccount(Boolean shouldCreateAccount) {
        this.shouldCreateAccount = shouldCreateAccount;
    }

    public Boolean getShouldCheckPayment() {
        return shouldCheckPayment;
    }

    public void setShouldCheckPayment(Boolean shouldCheckPayment) {
        this.shouldCheckPayment = shouldCheckPayment;
    }

    public String getCouponCode() {
        return couponCode;
    }

    public void setCouponCode(String couponCode) {
        this.couponCode = couponCode;
    }
}
