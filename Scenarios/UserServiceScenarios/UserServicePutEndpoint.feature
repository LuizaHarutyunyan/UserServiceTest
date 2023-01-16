Feature: UserServicePutEndpoint


Scenario: Change Status of not existing user-Not Found
	Given User is created
	And Unexisted user
	When Change user status of not existing user
	Then User status change status code is '404'

Scenario: Validate response status code of user status change from false to true-Ok
   Given User is created
   When Change user status to 'true'
   Then User status change status code is '200'

Scenario: Validate response status code of user status change from false to false-Ok
   Given User is created
   When Change user status to 'false'
   Then User status change status code is '200'

Scenario: Change user status from false to true - Response body is true
   Given User is created
   When Change user status to 'true'
   And Get user status by Id
   Then User status body is 'true'

Scenario: Change user status from true to false - Response body is false
   Given User is created
   And Change user status to 'true'
   When Change user status to 'false'
   And Get user status by Id
   Then User status body is 'false'

Scenario: Change user status from false-true-false - Response body is false
   Given User is created
   And Change user status to 'false'
   When Change user status to 'true'
   And Change user status to 'false'
   And Get user status by Id
   Then User status body is 'false'

Scenario:Change user status from false-true-false-true - Response body is true
   Given User is created
   And Change user status to 'false'
   When Change user status to 'true'
   And Change user status to 'false'
   And Change user status to 'true'
   And Get user status by Id
   Then User status body is 'true'

Scenario: Change user status true to true
   Given User is created
   When Change user status to 'true'
   And Change user status to 'true'
   And Get user status by Id
   Then User status body is 'true'