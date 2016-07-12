Feature: ReferenceExistingEntities

Background:
	Given I have the following products
		| Id | Name       |
		| 45 | Galaxy S5  |
		| 46 | iPhone 6   |
		| 47 | Nokia Icon |

Scenario: Reference existing entities (bad)
	When I choose the product with ID 47
	And update the price to $199.00
	Then the price should be saved

Scenario: Reference existing entities (good)
	When I select the Nokia Icon
	And update the price to $199.00
	Then the price should be saved

