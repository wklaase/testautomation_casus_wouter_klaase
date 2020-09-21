Feature: Login
	Test if the login functionality is working as expected for users and administrators.

Scenario: 001: Login as a user (happyflow)
	Given I have a user profile
	When I login to the IMTDb 
	Then I expect a valid access token

Scenario: 002: Login as an administrator (happyflow)
	Given I have an administrator profile
	When I login to the IMTDb
	Then I expect a valid access token

Scenario: 003: Login without a profile
	When I login to the IMTDb 
	Then I expect a status code in the get token response: 400
	Then I expect an error message in the get token response: "User and id do not match"

Scenario: 004: Login with an inactive user
	Given I have a user profile that is inactive
	When I login to the IMTDb
	Then I expect a status code in the get token response: 400
	And I expect an error message in the get token response: User: username is not active