using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SpecflowPerformanceBenchmark.Hooks
{
    internal class HookHelper
    {
        public static void KillBrowsers()
        {
            CloseAll("IEDriverServer");
            KillAll("IEDriverServer");
            CloseAllMainWindows("iexplore");
            KillAll("iexplore");
            KillAll("chromedriver");
            CloseAll("firefox");
            KillAll("firefox");
        }

        private static void CloseAllMainWindows(string processName)
        {
            Process.GetProcesses()
                   .Where(p => string.CompareOrdinal(p.ProcessName, processName) == 0)
                   .ToList()
                   .ForEach(p =>
                   {
                       try
                       {
                           p.CloseMainWindow();
                       }
                       catch
                       {
                       }
                   });
        }

        private static void CloseAll(string processName)
        {
            Process.GetProcesses()
                   .Where(p => string.CompareOrdinal(p.ProcessName, processName) == 0)
                   .ToList()
                   .ForEach(p =>
                   {
                       try
                       {
                           p.Close();
                       }
                       catch
                       {
                       }
                   });
        }

        private static void KillAll(string processName)
        {
            Process.GetProcesses()
                   .Where(p => string.CompareOrdinal(p.ProcessName, processName) == 0)
                   .ToList()
                   .ForEach(p =>
                   {
                       try
                       {
                           p.Kill();
                       }
                       catch
                       {
                       }
                   });
        }

        public static void RemoveHars()
        {
            var directoryLocation = new DirectoryInfo(@"C:\Har\");

            foreach (var file in directoryLocation.GetFiles())
            {
                file.Delete();
            }
            foreach (var dir in directoryLocation.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}