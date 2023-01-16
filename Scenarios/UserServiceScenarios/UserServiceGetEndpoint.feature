Feature: UserServiceGetEndpoint


Scenario: Get user status by Id - Ok
	Given User is created
	When Get user status by Id
	Then Get user status status code is '200'

Scenario: Get user status body 
	Given User is created
	When Get user status by Id
	Then User status body is 'false'