/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Teodor Nikolov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package pageobjectfluent.core;

public abstract class BasePage<ElementsT extends BaseElements, AssertionsT extends BaseAssertions<ElementsT>> {
    protected abstract String getUrl();

    protected ElementsT elements() {
        return NewInstanceFactory.createByTypeParameter(getClass(), 0);
    }

    public AssertionsT assertions() {
        return NewInstanceFactory.createByTypeParameter(getClass(), 1);
    }

    public BasePage<ElementsT, AssertionsT> navigate(String part) {
        Driver.getBrowser().navigate().to(getUrl().concat(part));
        return this;
    }

    public BasePage<ElementsT, AssertionsT> navigate() {
        Driver.getBrowser().navigate().to(getUrl());
        return this;
    }
}
