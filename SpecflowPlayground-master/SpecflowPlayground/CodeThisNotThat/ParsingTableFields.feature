Feature: ParsingTableFields

Scenario: Build a customer (bad)
	Given the customer
		| First Name | Last Name | Salutation | Address                                           |
		| Emilia     | Buschmann | Miss       | 905 West Dakin Street; Apt. 1; Chicago; IL; 60413 |

Scenario: Build a customer (good)
	Given the customer (fixed)
		| First Name | Last Name | Salutation |
		| Emilia     | Buschmann | Miss       |
	And the address
		| Line 1                | Line 2 | City    | State | Zipcode |
		| 905 West Dakin Street | Apt. 1 | Chicago | IL    | 60613   |