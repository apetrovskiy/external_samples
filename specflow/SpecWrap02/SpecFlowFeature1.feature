Feature: Migration to Windows 2012
	According to practices 
	new windows realeses should be supported by QMM

@mytag
Scenario: Migrate simple user with password
	Given I have created user in any source Directory
	And I have Domain Pair in QMM with Windows 2012 as Target Domain Controller
	When I Create new migration session with
	| SessionProperty | checked |
	| Password    | true    |
	Then the user appears in target directory with all supported attrubites
	 And  The use able to loginin via LDAP with source password

