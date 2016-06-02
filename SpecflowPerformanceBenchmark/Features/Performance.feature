Feature: Performance
	In order to perform performance benchmarking
	As a tester
	I want to be able to trigger a HAR export and feed it to YSlow

@HarExport
Scenario: Export HAR and Validate YSLow rating
	Given I navigate to Google
	When I trigger a HAR Export
	And I pass a HAR file to NodeJS YSlow
	Then I can get some value to assert against
	And Perform archiving