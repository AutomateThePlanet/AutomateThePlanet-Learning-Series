package decorator.core;

import java.text.DecimalFormat;

public abstract class BaseAssertions<ElementsT extends BaseElements> {
    protected ElementsT elements() {
        return NewInstanceFactory.createByTypeParameter(getClass(), 0);
    }

    protected String formatCurrency(double number) {
        DecimalFormat currencyFormat = new DecimalFormat("#,##0.00\u20ac");
        currencyFormat.setMaximumFractionDigits(2);
        currencyFormat.setMinimumFractionDigits(2);
        return currencyFormat.format(number);
    }
}
