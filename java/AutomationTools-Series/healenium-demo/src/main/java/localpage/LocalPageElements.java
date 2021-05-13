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

package localpage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.Select;

public class LocalPageElements {
    private final WebDriver driver;

    public LocalPageElements(WebDriver driver) {
        this.driver = driver;
    }

    public WebElement firstName() {
        return driver.findElement(By.id("firstName"));
    }

    public WebElement lastName() {
        return driver.findElement(By.id("lastName"));
    }

    public WebElement username() {
        return driver.findElement(By.id("username"));
    }

    public WebElement email() {
        return driver.findElement(By.id("email"));
    }

    public WebElement address1() {
        return driver.findElement(By.id("address"));
    }

    public WebElement address2() {
        return driver.findElement(By.id("address2"));
    }

    public Select country() {
        return new Select(driver.findElement(By.id("country")));
    }

    public Select state() {
        return new Select(driver.findElement(By.id("state")));
    }

    public WebElement zip() {
        return driver.findElement(By.id("zip"));
    }

    public WebElement cardName() {
        return driver.findElement(By.id("cc-name"));
    }

    public WebElement cardNumber() {
        return driver.findElement(By.id("cc-number"));
    }

    public WebElement cardExpiration() {
        return driver.findElement(By.id("cc-expiration"));
    }

    public WebElement cardCVV() {
        return driver.findElement(By.id("cc-cvv"));
    }

    public WebElement submitButton() {
        return driver.findElement(By.xpath("//button[text()='Continue to checkout']"));
    }
}
