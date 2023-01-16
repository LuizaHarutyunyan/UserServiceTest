Feature: WalletServiceGetEndpoint


@RegisteredActiveUser
Scenario: Get user balance-Ok
	When Get user balance
	Then Get balance status code is '200'

@RegisteredActiveUser
Scenario: Validate newly created user balance body-0
	When Get user balance
	Then User balance body is '0'

@RegisteredActiveUser
Scenario: Get User Balance of unexisted user-Internal Server Error
	Given Unexisted user
	When Get balance of unexisted user
	Then Get balance status code is '500'
	And Get balance error message is 'not active user'

@RegisteredActiveUser
Scenario: Get user balance of not active user-Internal Server Error
	Given Changed status 'false'
	When Get user balance
	Then Get balance status code is '500'
	And Get balance error message is 'not active user'


@RegisteredActiveUser
Scenario: Get balance after revert-Ok
	Given Change user balance with amount '300'
	When Cancel transaction
	And Get user balance
	Then Get balance status code is '200'

@RegisteredActiveUser
Scenario: Get balance body after revert- Balance decreased with changed amount
	Given Change user balance with amount '300'
	When Cancel transaction
	And Get user balance
	Then User balance body is '0.0'

