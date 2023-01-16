Feature: WalletServicePostEndpoint


@RegisteredActiveUser
Scenario Outline: Change user balance - Ok
	When Change user balance with amount '<amount>'
	Then Change balance status code is '200'
Examples:
	| amount     |
	| 300      |
	| 9999999.99 |
	| 10000000   |

@RegisteredActiveUser
Scenario Outline: Validate change user balance body - Body is Id in Guid format
	When Change user balance with amount '<amount>'
	Then Change balance body is in guid formate
Examples:
	| amount     |
	| 0.01       |
	| 9999999.99 |
	| 10000000   |

@RegisteredActiveUser
Scenario: Change balance of unexisted user - Internal server error
	Given Unexisted user
	When Change unexisted user balance with '0.01'
	Then Change balance status code is '500'
	And Change balance error message is 'not active user'

@RegisteredActiveUser
Scenario Outline: Change balance with negative amount when balance is 0 - Internal Server Error
	When Change user balance with amount '<amount>'
	Then Change balance with negative amount error message is 'User have '0', you try to charge '<amount>''
Examples:
	| amount       |
	| -0.01        |
	| -10000000.01 |


	
@RegisteredActiveUser
Scenario: Change balance with amount of zero - Internal Server Error
	When Change user balance with amount '0'
	Then Change balance status code is '500'
	And Change balance error message is 'Amount cannot be '0''

@RegisteredActiveUser
Scenario: Balance plus amount bigger than 10m - Internal Server Error
	Given Change user balance with amount '5000000'
	When Change user balance with amount '5000001'
	Then Change balance status code is '500'
	And Change balance error message is 'After this charge balance could be '10000001.0', maximum user balance is '10000000''

@RegisteredActiveUser
Scenario Outline: Balance + Amount smaller or equal to 10m - Ok
	Given Change user balance with amount '<balance>'
	When Change user balance with amount '<amount>'
	Then Change balance status code is '200'
Examples:
	| balance | amount |
	| 500000  | 500000 |
	| 499999  | 500000 |


@RegisteredActiveUser
Scenario Outline: Change balance with negative amount, which bigger than balance - Internal Server Error
	Given Change user balance with amount '<balance>'
	When Change user balance with amount '<amount>'
	Then Change balance status code is '500'
	And Change balance with negative amount error message is 'User have '<balance>.0', you try to charge '<amount>.0''
Examples:
	| balance | amount |
	| 50      | -1000  |
	

@RegisteredActiveUser
Scenario Outline: Wrong precision of amount - Internal Server Error
	Given Change user balance with amount '<balance>'
	When Change user balance with amount '<amount>'
	Then Change balance status code is '500'
	And Change balance error message is 'Amount value must have precision 2 numbers after dot'
Examples:
	| balance | amount |
	| 0.1      | 0.001  |
	| 5.78     | 6.325  |
	