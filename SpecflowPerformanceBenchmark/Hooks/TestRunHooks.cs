// Setup and tear down around a test run

using TechTalk.SpecFlow;

namespace SpecflowPerformanceBenchmark.Hooks
{
    [Binding]
    public static class TestRunHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            HookHelper.KillBrowsers();
            HookHelper.RemoveHars();
            TestRunContext.Initialise();
            TestRunContext.WindowSetup();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestRunContext.Driver.Quit();
        }
    }
}