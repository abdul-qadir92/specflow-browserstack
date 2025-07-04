using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SpecFlowBrowserStack
{
    [Binding]
    public class DemoSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait wait;

        public DemoSteps()
        {
            // Fix for CS8601: Ensure ThreadLocalDriver.Value is not null before assignment
            if (BrowserStackSpecFlowTest.ThreadLocalDriver?.Value == null)
            {
                throw new InvalidOperationException("ThreadLocalDriver.Value is null. WebDriver was not initialized.");
            }

            _driver = BrowserStackSpecFlowTest.ThreadLocalDriver.Value;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        [Given(@"I navigate to demo website")]
        public void GivenINavigateToWebsite()
        {
            Assert.That(_driver, Is.Not.Null, "_driver is null. WebDriver was not initialized.");
            _driver.Navigate().GoToUrl("https://bstackdemo.com/");
        }

        [Then(@"I Click on Orders")]
        public void ThenIClickOnOrders()
        {
            // Wait for Orders link/button to be visible and click it
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(),'Orders')]")));
            _driver.FindElement(By.XPath("//*[contains(text(),'Orders')]"))?.Click();
        }

        [Then(@"I should see orders page")]
        public void ThenIShouldSeeOrdersPage()
        {
            // Wait for an element unique to the Orders page
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(text(),'Orders')]")));
            var header = _driver.FindElement(By.XPath("//h1[contains(text(),'Orders')]"));
            Assert.That(header.Displayed, Is.True);
        }
    }
}
