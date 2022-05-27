@WebDriver
Feature: UI_Automation_Test

Scenario Outline: Fulfill form with correct data
	Given the user opens Chrome browser and navigates to demoQA webpage
	When the user fulfills Full Name field with '<FullName>'
	And the user fulfills Email field with '<eMail>'
	And the user fulfills Current Address field with '<CurrentAddress>'
	And the user fulfills Permanent Address field with '<PermanentAddress>'
	And the user clicks on submit button
	Then the output form FullName is '<FullName>'
	And the output form Email is '<eMail>'
	And the output form Current Address is '<CurrentAddress>'
	And the output form Permanent Address is '<PermanentAddress>'

Examples: 
	| TestName | FullName   | eMail                     | CurrentAddress                    | PermanentAddress              |
	| Test1    | test user  | test@blabla.com           | C. Dobla, 5, 28054, Madrid, Spain | Street X, 28013 Madrid, Spain |
	| Test2    | John Smith | john.smith@mailinator.com | Street Smith 3, London, UK        | Street Smith 6, London, UK    |

Scenario: Fulfill form with incorrect data
	Given the user opens Chrome browser and navigates to demoQA webpage
	When the user fulfills Full Name field with ''
	And the user fulfills Email field with 'thisisnotanemail'
	And the user fulfills Current Address field with ''
	And the user fulfills Permanent Address field with ''
	And the user clicks on submit button
	Then format error in email is detected
	And no output information is appearing in the screen
	


