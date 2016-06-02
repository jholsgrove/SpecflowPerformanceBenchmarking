using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SpecflowPerformanceBenchmark.Helpers;

namespace SpecflowPerformanceBenchmark.Hooks
{
    public static class TestRunContext
    {
        public static string Location
        {
            get
            {
#if TEST_LOCAL
                return TestRunLocation.Local;
#else
                return TestRunLocation.Remote;
#endif
            }
        }

        public static IWebDriver Driver { get; private set; }

        public static void WindowSetup()
        {
            Driver.Manage().Window.Maximize();
        }

        #region Browser Types

        public static void Initialise()
        {
            var appSettings = ConfigurationManager.AppSettings;

            var environment = appSettings.Get("environment");
            var browser = appSettings.Get("browser");

            if (environment == "local")
            {
                if (browser == "Firefox")
                {
                    Initialise(BrowserType.Firefox);
                }
                else
                {
                    throw new ApplicationException("Unsupported browser!");
                }
            }
            else
            {
                throw new ApplicationException("Invalid environment specified!");
            }
        }

        public static void Initialise(int browserType)
        {
            SetupFirefoxDriver();
        }

        #endregion // Browser Types

        private static void SetupFirefoxDriver()
        {
            string profileDir;

            profileDir = @"YourProfileWithHarExportTrigger";

            FirefoxProfile profile = new FirefoxProfile(profileDir);
            Driver = new FirefoxDriver(profile);
        }
    }
}