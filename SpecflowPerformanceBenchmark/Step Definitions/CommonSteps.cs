using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecflowPerformanceBenchmark.Constants;
using SpecflowPerformanceBenchmark.Pages;
using TechTalk.SpecFlow;

namespace SpecflowPerformanceBenchmark
{
    [Binding]
    internal class CommonSteps
    {
        public CommonSteps(MainPage page)
        {
            Page = page;
        }

        private MainPage Page { get; set; }

        [Given(@"I navigate to Google")]
        public void GivenINavigateToGoogle()
        {
            Page.NavigateTo("http://www.google.co.uk");
        }

        [When(@"I trigger a HAR Export")]
        public void WhenITriggerAHARExport()
        {
            Page.TriggerHarExport();
        }

        [When(@"I pass a HAR file to NodeJS YSlow")]
        public void WhenIPassAHARFileToNodeJSYSlow()
        {
            Page.ProcessHar();
        }

        [Then(@"I can get some value to assert against")]
        public void ThenICanGetSomeValueToAssertAgainst()
        {
            var rating = Page.GetLoadRating();
            Assert.IsTrue(rating >= YSlowRating.Percent95);
        }

        [Then(@"Perform archiving")]
        public void ThenPerformArchiving()
        {
            Page.ArchiveHarFile();
        }
    }
}