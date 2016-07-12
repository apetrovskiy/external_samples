Feature: ScopedBinding
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@scope1
Scenario: Scope 1
	Given a binding scoped by tag
	Then scope1 should be saved

@scope2
Scenario: Scope 2
	Given a binding scoped by tag
	Then scope2 should be saved
