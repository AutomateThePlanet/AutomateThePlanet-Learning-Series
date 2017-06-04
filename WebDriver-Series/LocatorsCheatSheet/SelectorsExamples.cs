// <copyright file="SelectorsExamples.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace LocatorsCheatSheet
{
    [TestFixture]
    public class SelectorsExamples
    {
        private IWebDriver _driver;

        // Default FindsBy Locators
        //[FindsBy(How = How.Id, Using = "userName")]
        //[FindsBy(How = How.ClassName, Using = "panel other")]
        //[FindsBy(How = How.CssSelector, Using = "#userName")]
        //[FindsBy(How = How.LinkText, Using = "Automate The Planet")]
        //[FindsBy(How = How.Name, Using = "webDriverCheatSheet")]
        //[FindsBy(How = How.PartialLinkText, Using = "Automate")]
        //[FindsBy(How = How.TagName, Using = "a")]
        //[FindsBy(How = How.XPath, Using = "//*[@id='panel']/div/h1")]
        //private IWebElement _myElement;

        [SetUp]
        public void TestInit()
        {
            _driver = new FirefoxDriver();
            ////PageFactory.InitElements(_driver, this);
        }

        [Test]
        public void DefaultWebDriverSelectorsMethods()
        {
            _driver.FindElement(By.Id("userName"));
            _driver.FindElement(By.ClassName("panel other"));
            _driver.FindElement(By.CssSelector("#userName"));
            _driver.FindElement(By.LinkText("Automate The Planet"));
            _driver.FindElement(By.Name("webDriverCheatSheet"));
            _driver.FindElement(By.PartialLinkText("Automate"));
            _driver.FindElement(By.TagName("a"));
            _driver.FindElement(By.XPath("//*[@id='panel']/div/h1"));
        }

        [Test]
        public void XPathSelectors()
        {
            ////_driver.FindElement(By.XPath("//*[@id='panel']/div/h1"));
            // //img //image element
            // //img[@id='myId'] //image element with ID = 'myId'
            // //img[@id!='myId'] //image elements with ID not equal to 'myId'
            // //img[@name] //image elements that have name attribute
            // //*[contains(@id, 'Id')] //element with id containing
            // //*[starts-with(@id, 'Id')] //element with id starting with
            // //*[ends-with(@id, 'Id')] //element with id ending with
            // //*[matches(@id, 'r')] //element with id  matching regex ‘r’
            // //*[@name='myName'] //image element with name = 'myName'
            // //*[@id='X' or @name='X'] //element with id X or, failing that, a name X
            // //*[@name="N"][@value="v"] // element with name N & specified value ‘v’
            // //*[@name="N" and @value="v"] // element with name N & specified value ‘v’
            // //*[@name="N" and not(@value="v")] // element with name N & not specified value ‘v’
            // //input[@type="submit"] //selects input of type sumit
            // //section[//h1[@id='hi']] //returns <section> if it has an <h1> descendant with id='hi'
            // //table[count(tr) > 1] //return table with more than 1 row

            // //*[.="t"] //element containing text 't' exactly
            // //a[contains(text(), "Log Out")] //Matching Inner Text
            // //a[not(contains(text(), "Log Out"))] //'a' not containing 'Log Out' inner text
            // //a[@href="url"] // anchor with target link 'url'

            // Parent & Childs
            // //img/*[1] //first child of element img
            // //ul/child::li  //first child 'li' of 'ul'
            // //img[1] //first img child
            // //img/*[last()] //last child of element img
            // //img[last()] //last img child
            // //img[last()-1] //second last img child
            // //input/following-sibling::a //'a' following some sibling 'input'
            // //a/following-sibling::* //sibling element immediately following 'a'
            // //input/preceding-sibling::a  // 'a' preceding some sibling 'input'
            // //input/preceding-sibling::*[1] // sibling element immediately preceding 'input'
            // //img[@id='MyId']::parent/* //Selects the parent of image with id

            // //*[@id="TestTable"]//tr[3]//td[2] // cell by row and column
            // //td[preceding-sibling::td="t"] // cell immediately following cell containing 't' exactly
            // //td[preceding-sibling::td[contains(.,"t")]] //cell immediately following cell containing 't'

            // //input[@checked] // checkbox (or radio button) that is checked
            // //a[@disabled] // all 'a' elements that are disabled
            // //a[not(@disabled)] // all 'a' elements that are not disabled

            // //a[@price > 2.50]  //'a' with price > 2.5
            // //ul[*] // 'ul' that has children
        }

        [Test]
        public void CssSelectors()
        {
            ////_driver.FindElement(By.CssSelector("panel other"));

            //// Element By Id
            //ul#myUniqueId 
            //#myUniqueId // do not specify element type

            ////Element by class
            //ul.myForm  // specify element type
            //.myForm.front // do not specify element type
            //.myForm.front.down

            //ul#myUniqueId > li // direct child element
            //ul#myUniqueId  li // sub child element

            // Element by attribute
            //ul[name = "automateName"][style = "style_name"] // ‘ul’ element with attributes name =‘automateName’ and style= ‘style name’
            //ul[id = "myId"] // 'ul' element with id='myId'
            //ul[@id] // elements with @attribute
            //*[name='N'][value='v’] //elements with name N & specified value ‘v’
            ////Element by pattern matching
            //ul[id ^= "my"] // selects all elements with an attribute beginning with ‘my’
            //ul[id$= "Id"] // selects all elements with an attribute ending with ‘Id’
            //ul[id *= "Unique"] // selects all elements with an attribute containing the substring ‘Unique’
            //ul[id ~= "Unique"] // selects all elements with an attribute containing the word ‘Unique’

            //ul#myUniqueId  li:first-child //select first child element
            //ul#myUniqueId  li:nth-of-type(1) //select first child element

            //ul#myUniqueId  li:last-child //select last child element
            //ul#myUniqueId  li:nth-of-type(3) //select last child element

            //div > p //selects all <p> elements that are a direct descendant of a <div> element
            //div + p //selects all <p> elements that are the next sibling of a <div> element (i.e.placed directly after)
            //div ~p //selects all <p> elements that follow, and are siblings of <div> elements


            // Pseudo-classes
            //a:link //selects all unvisited links
            //a:visited //selects all visited links
            //a:hover //selects links on mouse hover
            //input:active //selects every active <input> element
            //input:disabled //selects every disabled<input> element
            //input:enabled //selects every enabled<input> element
            //input:focus //selects the<input> element which has focus
            //p:lang(language) //selects all <p> elements with a lang attribute equal to ‘language’
            //input:read-only //selects < input > elements with the ‘readonly’ attribute specified
            //input:required //selects <input> elements with the ‘required’ attribute specified
            //input:checked //selects checkbox (or radio button) that is checked

            //form myForm.front + ul   // next Sibling
            //a:contains('Log Out')   // anchor with inner text containing 'Log Out'
            //a[href='url'] // anchor with target link 'url'
            // #TestTable tr:nth-child(3) td:nth-child(2) //cell by row and column (e.g. 3rd row, 2nd column)
            // td:contains('t') ~td //cell immediately following cell containing 't'
        }
    }
}
