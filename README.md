# SpecflowPerformanceBenchmarking
Trigger a HAR export and Validate against NPM YSlow example code

General
Restore NuGet Packages
Install NUnit 3.2.0
Install HAR Trigger Export (FF Plugin)
Install NodeJs & package YSlow (with npm install yslow -g)
Install NUnit Adapter for easy test running in Visual Studio

First you'll need to install Node.js, and then install the YSlow app (with npm install yslow -g).
Create Custom FF Profile - Reference it in TestRunContext. This profile must have HAR Trigger Export installed and configured correctly
http://www.softwareishard.com/blog/har-export-trigger/

* Prefs: extensions.netmonitor.har.contentAPIToken test
* Prefs: extensions.netmonitor.har.autoConnect true
* Prefs: extensions.netmonitor.har.enableAutomation true

--- Add C:\Har & C:\HarArchive directories
