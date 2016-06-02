using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace SpecflowPerformanceBenchmark.Pages
{
    public class MainPage : PageBase
    {
        public MainPage(PageContext context)
            : base(context)
        {
        }

        public string yslowRating;

        public void TriggerHarExport()
        {
            // You must have your FF profile setup correctly with HAR Export Trigger and settings
            IJavaScriptExecutor setParams = Context.Driver as IJavaScriptExecutor;
            setParams.ExecuteScript("var options = {token: 'test', getData: true, title: 'YourHarFile Har', jsonp: false, fileName: 'YourHarFile'}; HAR.triggerExport(options).then(result => {console.log(result.data);});");
        }

        public void ProcessHar()
        {
            var cmdStartInfo = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\cmd.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            var cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_DataReceived;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();

            // Execute YSlow command
            cmdProcess.StandardInput.WriteLine("yslow --info basic --format json \"C:\\Har\\YourHarFile.har\"");
            cmdProcess.WaitForExit(1000);
        }

        public int GetLoadRating()
        {
            // Do some magic regex
            // Expression: "o":[0-9]+

            var extractedValue = Regex.Match(yslowRating, @"""o"":[0-9]+");
            var extractedValueString = extractedValue.ToString();
            var numericRating = Regex.Match(extractedValueString, @"\d+");
            var numericRatingString = numericRating.ToString();
            var yslowNumRating = Int32.Parse(numericRatingString);

            return yslowNumRating;
        }

        private void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine("Output from other process");
            Trace.WriteLine(e.Data);
            if (e.Data.Contains("ydefault"))
            {
                yslowRating = e.Data;
            }
        }

        private void cmd_Error(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine("Error from other process");
            Trace.WriteLine(e.Data);
        }

        public void ArchiveHarFile()
        {
            string archiveFile = "YourHarFile_" + DateTime.Now.ToFileTime() + ".har";

            try
            {
                File.Move(@"C:\Har\YourHarFile.har", @"C:\HarArchive\" + archiveFile); // Try to move
                Trace.WriteLine("Moved to Archive"); // Success
            }
            catch (IOException ex)
            {
                Trace.WriteLine(ex); // Write error
            }
        }
    }
}