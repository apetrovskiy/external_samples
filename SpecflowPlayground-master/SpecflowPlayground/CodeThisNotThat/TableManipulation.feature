Feature: TableManipulation

Scenario: Creating an address
	Given the following address
		| Line 1                | Line 2 | City    | State | Zipcode |
		| 905 West Dakin Street | Apt. 1 | Chicago | IL    | 60613   |
	Then the following address should be returned by the service
		| Line 1                | Line 2 | City    | State | Zipcode |
		| 905 West Dakin Street | Apt. 1 | Chicago | IL    | 60613   |