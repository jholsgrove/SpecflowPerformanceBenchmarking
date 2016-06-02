using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SpecflowPerformanceBenchmark.Pages
{
    public class PageContext
    {
        public PageContext(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; set; }

        public Actions Actions { get; set; }
    }
}