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
package bellatrixdemos.pages.bellatrixdemositempage;

import bellatrixdemos.core.BasePage;
import bellatrixdemos.pages.interfaces.ItemPage;

public class BellatrixDemoItemPage extends BasePage<BellatrixDemoItemElements, BellatrixDemoItemAssertions> implements ItemPage {
    @Override
    protected String getUrl() {
        return "http://demos.bellatrix.solutions/product/";
    }

    @Override
    public void clickBuyNowButton() {
        elements().addToCartButton().click();
    }

    @Override
    public void clickViewShoppingCartButton() {
        elements().viewShoppingCartButton().click();
    }

    @Override
    public void assertPrice(double itemPrice) {
        assertions().assertProductPrice(itemPrice);
    }
}
