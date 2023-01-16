Feature: WalletServicePutEndpoint


@RegisteredActiveUser
Scenario Outline: Revert transaction with positive amounts - Ok
	Given Change user balance with amount '<amount>'
	When Cancel transaction
	Then Canceled transaction response status code is '200'
Examples:
	| amount    |
	| 0.01      |
	| 10000000  |
	| 999999.99 |

@RegisteredActiveUser
Scenario Outline: Validate reverted transaction body - Body is in guid formate
	Given Change user balance with amount '<amount>'
	When Cancel transaction
	Then Canceled transaction body is in guid formate
Examples:
	| amount    |
	| 0.01      |
	| 10000000  |
	| 999999.99 |

@RegisteredActiveUser
Scenario Outline: Revert transaction with negative amount - Bad Request
         Given Change user balance with amount '<amount>'
		 When Cancel transaction
		 Then Canceled transaction response status code is '400'
Examples:
	| amount    |
	| -0.01  |
	| -10000000.01 |

Scenario: Revert transaction with wrong transaction id - Not Found
      Given Wrong transaction id
	  When Cancel transaction with wrong id
	  Then Canceled transaction response status code is '404'
	   And Canceled transaction error message is 'The given key was not present in the dictionary.'

@RegisteredActiveUser
Scenario: Revert transaction if transaction is already reverted - Internal Server Error
       Given Change user balance with amount '300'
	   When Cancel transaction
	   And Cancel transaction
	   Then Canceled transaction response status code is '500'
	  