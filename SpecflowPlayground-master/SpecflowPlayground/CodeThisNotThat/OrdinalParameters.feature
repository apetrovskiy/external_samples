Feature: OrdinalParameters

Scenario: Passing ordinal parameters (bad)
	When I remove the item at index 5


Scenario: Passing ordinal parameters (good)
	When I remove the 5th item
