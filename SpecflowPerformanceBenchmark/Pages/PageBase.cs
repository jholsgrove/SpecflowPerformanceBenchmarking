using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SpecflowPerformanceBenchmark.Pages
{
    public class PageBase
    {
        /// <summary>
        /// The max wait seconds.
        /// </summary>
        protected const int MaxWaitSeconds = 30;
        protected readonly PageContext Context;

        public PageBase(PageContext context)
        {
            Context = context;
            PageFactory.InitElements(Context.Driver, this);
        }
  
        public void NavigateTo(string url)
        {
            Context.Driver.Navigate()
                   .GoToUrl(url);
        }

        public IWebElement WaitForElementByClass(string className)
        {
            return WaitForVisibility(By.ClassName(className));
        }

        public IWebElement WaitForElementById(string id)
        {
            return WaitForVisibility(By.Id(id));
        }

        public IWebElement WaitForElementByName(string name)
        {
            return WaitForVisibility(By.Name(name));
        }

        public IWebElement WaitForElementByXpath(string xpath)
        {
            return WaitForVisibility(By.XPath(xpath));
        }

        // Refactored all the "wait for this & that to be visible" stuff into a couple
        // of tiny methods
        // <param name = "byCriterion"></param>
        // <param name = "maxWaitSeconds"></param>

        public IWebElement WaitForVisibility(By identifier, int maxWaitSeconds = MaxWaitSeconds)
        {
            var wait = new WebDriverWait(Context.Driver, TimeSpan.FromSeconds(maxWaitSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(identifier));
        }
    }
}