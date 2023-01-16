Feature: UserServiceDeleteEndpoint

Scenario: Delete user by Id-Ok
	Given User is created
	When Delete user by Id
	Then Deleted user status code is '200'


Scenario: Delete user by Id-body is empty
      Given User is created
	  When Delete user by Id
	  Then Deleted user body is empty

Scenario: Delete not active user-Ok
      Given User is created
	  When Delete user by Id
	  Then Deleted user status code is '200'

Scenario: Delete not active user-body is empty
      Given User is created
	  When Delete user by Id
	  Then Deleted user body is empty

Scenario: Validate user status code after repeated deleting-Internal Server Error
     Given User is created
	 When Delete user by Id
	 And Delete user by Id
	 Then Deleted user status code is '500'
	 And Deleted user error message is 'Sequence contains no elements'

Scenario: Validate not existing user delete status code-Not Found
     Given User is created
	 And Unexisted user
	 When Delete user by not existing Id
	 Then Deleted user status code is '404'

          