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
package pageobjectfluent.pages.bingmainpage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import pageobjectfluent.core.BaseElements;

import java.util.List;

public class BingMainPageElements extends BaseElements {
    public WebElement searchBox() {
        return browser.findElement(By.id("sb_form_q"));
    }

    public WebElement goButton() {
        return browser.findElement(By.xpath("//label[@for='sb_form_go']"));
    }

    public WebElement resultsCountDiv() {
        return browser.findElement(By.id("b_tween"));
    }

    public WebElement imagesLink() {
        return browser.findElement(By.xpath("//nav//a[text() = 'Images']"));
    }

    public WebElement filterMenu() {
        return browser.findElement(By.xpath("//span[@id='fltIdt']"));
    }

    public WebElement sizes() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'Image size']"));
    }

    public List<WebElement> sizesOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'Image size']/ancestor::li/div/div//a"));
    }

    public WebElement color() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'Color']"));
    }

    public List<WebElement> colorOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'Color']/ancestor::li/div/div//a"));
    }

    public WebElement type() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'Type']"));
    }

    public List<WebElement> typeOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'Type']/ancestor::li/div/div//a"));
    }

    public WebElement layout() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'Layout']"));
    }

    public List<WebElement> layoutOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'Layout']/ancestor::li/div/div//a"));
    }

    public WebElement people() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'People']"));
    }

    public List<WebElement> peopleOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'People']/ancestor::li/div/div//a"));
    }

    public WebElement date() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'Date']"));
    }

    public List<WebElement> dateOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'Date']/ancestor::li/div/div//a"));
    }

    public WebElement license() {
        return browser.findElement(By.xpath("//div/ul/li/span/span[text() = 'License']"));
    }

    public List<WebElement> licenseOption() {
        return browser.findElements(By.xpath("//div/ul/li/span/span[text() = 'License']/ancestor::li/div/div//a"));
    }
}
