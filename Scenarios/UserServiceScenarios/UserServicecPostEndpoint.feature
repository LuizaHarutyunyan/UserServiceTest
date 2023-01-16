
Feature: UserServicecPostEndpoint


Scenario Outline: Register for new created user - OK
	When Create User with <firstname> and <lastname>
	Then Register new user status code is '200'
Examples: 
        | firstname |  | lastname     |
        | luiza     |  | harutyunyan  |
        | $         |  | $            |
        | @luiza    |  | @harutyunyan |
        | 012345    |  | 543210       |
        | LUIZA     |  | HARUTYUNYAN  |
        

Scenario: Register new user fields equal to null-Internal Server Error
     When Create User with null values
     Then Register new user status code is '500'

Scenario: Validate sequence of Id - Current Id should be bigger by one than previous
     Given User is created
     When Delete user by Id
     And User is created

Scenario: Validate Id sequence - Ids are ordered
   Given User is created
   When Delete user by Id
   And Create User with luiza and harutyunyan
   Then Created user ids are ordered
