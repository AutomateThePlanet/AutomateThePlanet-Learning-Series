/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Anton Angelov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package bellatrixdemos.pages.bellatrixdemoscheckoutpage;

import bellatrixdemos.core.BaseAssertions;
import org.testng.Assert;

public class BellatrixDemoCheckoutAssertions extends BaseAssertions<BellatrixDemoCheckoutElements> {

    public void assertOrderReceived() {
        Assert.assertEquals(elements().receivedMessage().getText(), "Order received");
    }

    public void assertOrderSubtotalPrice(double subtotalPrice) {
        Assert.assertEquals(elements().orderDetailsSubtotal().getText(), formatCurrency(subtotalPrice));
    }

    public void assertOrderDiscountPrice(double discountPrice) {
        Assert.assertEquals(elements().orderDetailsDiscount().getText(), formatCurrency(discountPrice));
    }

    public void assertOrderVatTaxPrice(double vatTaxPrice) {
        Assert.assertEquals(elements().orderDetailsVatTax().getText(), formatCurrency(vatTaxPrice));
    }

    public void assertOrderTotalPrice(double totalPrice) {
        Assert.assertEquals(elements().orderDetailsTotal().getText(), formatCurrency(totalPrice));
    }
}
