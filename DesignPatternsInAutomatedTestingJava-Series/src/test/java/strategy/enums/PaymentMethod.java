package strategy.enums;

import org.apache.commons.lang3.StringUtils;

public enum PaymentMethod {
    DIRECT_BANK_TRANSFER,
    CHECK_PAYMENTS;

    @Override
    public String toString() {
        return StringUtils.capitalize(name().replace('_', ' ').toLowerCase());
    }
}
