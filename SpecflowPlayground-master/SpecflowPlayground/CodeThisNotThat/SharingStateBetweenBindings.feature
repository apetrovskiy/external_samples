Feature: SharingStateBetweenBindings

Scenario: Sharing state with ScenarioContext
	Given the new customer
		| First Name | Last Name | Salutation |
		| Emilia     | Buschmann | Miss       |
	And the customer's address
		| Line 1                | Line 2 | City    | State | Zipcode |
		| 905 West Dakin Street | Apt. 1 | Chicago | IL    | 60613   |
	When I save the customer
	Then a successful response should be returned
