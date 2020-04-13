using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace ElementInteractions
{
    [TestClass]
    [TestCategory("Identification and manipulations")]
    public class ElementIdentification
    {
        static IWebDriver driver;
        private IWebElement element;
        private By locator;

        [TestInitialize]
        public void TestSetup()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(outPutDirectory);
        }

        [TestMethod]
        [TestCategory("Navigation")]
        public void SeleniumNavigation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            driver.Navigate().Forward();
            driver.Navigate().Back();
            driver.Navigate().Refresh();
        }

        [TestMethod]
        [TestCategory("Navigation")]
        public void SeleniumNavigation1()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            Thread.Sleep(3000);
            driver.Navigate().Back();
            Thread.Sleep(3000);
            driver.Navigate().Forward();
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
        }

        [TestMethod]
        [TestCategory("Navigation")]
        public void SeleniumNavigationTest()
        {
            //Go here and assert for title - "https://www.ultimateqa.com"
            driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Assert.AreEqual("Home - Ultimate QA", driver.Title);
            //Go here and assert for title - "https://www.ultimateqa.com/automation"
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);
            //Click link with href - /complicated-page
            driver.FindElement(By.XPath("//*[@href='/complicated-page']")).Click();
            
            //assert page title 'Complicated Page - Ultimate QA'
            Assert.AreEqual("Complicated Page - Ultimate QA", driver.Title);
            //Go back
            driver.Navigate().Back();
            //assert page title equals - 'Automation Practice - Ultimate QA'
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);
        }

        [TestMethod]
        [TestCategory("Navigation")]
        public void SeleniumNavigationTest1()
        {
            //Go here and assert for title - "https://www.ultimateqa.com"
            driver.Navigate().GoToUrl("https://www.ultimateqa.com");

            Assert.AreEqual("Home - Ultimate QA", driver.Title);

            //Go here and assert for title - "https://www.ultimateqa.com/automation"
            driver.Navigate().GoToUrl("https://ultimateqa.com/automation/");
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);

            //Click link with href - /complicated-page
            driver.FindElement(By.XPath("//*[@href='https://ultimateqa.com/sample-application-lifecycle-sprint-1/']")).Click();
            //driver.FindElement(By.XPath("//a[text()='Big page with many elements']")).Click();

            //assert page title 'Complicated Page - Ultimate QA'
            //driver.Navigate().GoToUrl("https://ultimateqa.com/complicated-page");
            //Assert.AreEqual("Complicated Page - Ultimate QA", driver.Title);
            Assert.AreEqual("Sample Application Lifecycle - Sprint 1 - Ultimate QA", driver.Title);



            ////Go back
            driver.Navigate().Back();

            ////assert page title equals - 'Automation Practice - Ultimate QA'
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);

            //driver.FindElement(By.XPath("//*[@href='../complicated-page']")).Click();  //page is broken
            //Assert.AreEqual("Complicated Page - Ultimate QA", driver.Title);

        }

        //        The<Comment> tag contains two text nodes and two<br> nodes as children.

        //Your xpath expression was

        ////*[contains(text(),'ABC')]
        //To break this down,


        //* is a selector that matches any element (i.e.tag) -- it returns a node-set.
        //The[] are a conditional that operates on each individual node in that node set.It matches if any of the individual nodes it operates on match the conditions inside the brackets.
        //text() is a selector that matches all of the text nodes that are children of the context node -- it returns a node set.
        //contains is a function that operates on a string. If it is passed a node set, the node set is converted into a string by returning the string-value of the node in the node-set that is first in document order. Hence, it can match only the first text node in your<Comment> element -- namely BLAH BLAH BLAH. Since that doesn't match, you don't get a<Comment> in your results.
        //You need to change this to

        ////*[text()[contains(.,'ABC')]]
        //* is a selector that matches any element (i.e.tag) -- it returns a node-set.
        //The outer [] are a conditional that operates on each individual node in that node set -- here it operates on each element in the document.
        //text() is a selector that matches all of the text nodes that are children of the context node -- it returns a node set.
        //The inner [] are a conditional that operates on each node in that node set -- here each individual text node.Each individual text node is the starting point for any path in the brackets, and can also be referred to explicitly as . within the brackets.It matches if any of the individual nodes it operates on match the conditions inside the brackets.
        //contains is a function that operates on a string. Here it is passed an individual text node (.). Since it is passed the second text node in the<Comment> tag individually, it will see the 'ABC' string and be able to match it.

        [TestMethod]
        [TestCategory("Manipulation")]
        public void Manipulation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            //find the name field

            var nameField = driver.FindElement(By.Id("et_pb_contact_name_1"));
            nameField.Clear();
            nameField.SendKeys("test");
            //clear the field
            //type into the field

            //find the text field
            var textBox = driver.FindElement(By.Id("et_pb_contact_message_1"));
            //clear the field
            textBox.Clear();
            //type into the field
            textBox.SendKeys("testing");
            //submit
            var submitButton = driver.FindElements(By.ClassName("et_contact_bottom_container"));
            submitButton[0].Submit();
        }

        [TestMethod]
        [TestCategory("Manipulation")]
        public void ManipulationTest()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            var name = driver.FindElements(By.Id("et_pb_contact_name_1"));
            name[1].SendKeys("test");

            var textArea = driver.FindElements(By.Id("et_pb_contact_message_1"));
            textArea[1].SendKeys("test text");

            var captcha = driver.FindElement(By.ClassName("et_pb_contact_captcha_question"));
            //show example of how this will work in Chrome dev tools but not in code
            var table = new DataTable();
            var captchaAnswer = (int)table.Compute(captcha.Text,"");

            var captchaTextBox = driver.FindElement(By.XPath("//*[@class='input et_pb_contact_captcha']"));
            captchaTextBox.SendKeys(captchaAnswer.ToString());

            driver.FindElements(By.XPath("//*[@class='et_pb_contact_submit et_pb_button']"))[1].Submit();
            var successMessage = driver.FindElements(By.ClassName("et-pb-contact-message"))[1].FindElement(By.TagName("p"));
            Assert.IsTrue(successMessage.Text.Equals("Success"));
        }

        [TestMethod]
        [TestCategory("Driver Interrogation")]
        public void DriverLevelInterrogation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            var x = driver.CurrentWindowHandle;
            var y = driver.WindowHandles;
            x = driver.PageSource;
            x = driver.Title;
            x = driver.Url;
        }

        [TestMethod]
        [TestCategory("Element Interrogation")]
        public void ElementInterrogation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation/");
            var myElement = driver.FindElement(By.XPath("//*[@href='https://courses.ultimateqa.com/users/sign_in']"));
        }

        [TestMethod]
        [TestCategory("Element Interrogation")]
        public void ElementInterrogationTest()
        {
            driver.Url = "https://www.ultimateqa.com/simple-html-elements-for-automation/";
            driver.Manage().Window.Maximize();
            //1. find button by Id
            //2. GetAttribute("type") and assert that it equals the right value
            //3. GetCssValue("letter-spacing") and assert that it equals the correct value
            //4. Assert that it's Displayed
            //5. Assert that it's Enabled
            //6. Assert that it's NOT selected
            //7. Assert that the Text is correct
            //8. Assert that the TagName is correct
            //9. Assert that the size height is 21
            //10. Assert that the location is x=190, y=330





            var myElement = driver.FindElement(By.Id("button1"));
            Assert.AreEqual("submit", myElement.GetAttribute("type"));
            Assert.AreEqual("normal", myElement.GetCssValue("letter-spacing"));
            Assert.IsTrue(myElement.Displayed);
            Assert.IsTrue(myElement.Enabled);
            Assert.IsFalse(myElement.Selected);
            Assert.AreEqual(myElement.Text, "Click Me!");
            Assert.AreEqual("button", myElement.TagName);
            Assert.AreEqual(21, myElement.Size.Height);
            Assert.AreEqual(190, myElement.Location.X);
            Assert.AreEqual(330, myElement.Location.Y);
        }




        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
