Feature: Signup
	Test if the signup functionality is working as expected for users and administrators

Scenario: 001: Create a new user profile (happyflow)
	Given I use password secretpassword and username usernamee
	When I sign up as a user
	Then I expect that the profile is created succesfully

Scenario: 002: Create an administrator profile (happyflow)
	Given I have an administrator profile with password secretpassword and username firstadmin
	And I am logged in as administrator
	And I use password secretpassword and username secondadmin
	When I create the administrator profile
	Then I expect that the profile is created succesfully

Scenario: 003: Signup as a user with a username that already exists
	Given I use a username that already exists
	When I sign up as a user
	Then I expect a status code in the create user response: 400
	And I expect an error message in the create user response: User: username already exists