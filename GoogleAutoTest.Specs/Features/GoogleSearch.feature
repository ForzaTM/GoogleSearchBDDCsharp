@GoogleSearch
Feature: GoogleSearch
	Search for “automation”.	

@nunit
Scenario Outline: 01 Open Google search and verify that title contains searched word
	Given I have entered searched word into the Google search
	| GivenBrowser | GivenWord  |
	| <Browser>    | automation |	
	When I press search button
	Then the title containss expected word
	| Word           |
	| <ExpectedWord> |
	Scenarios:
	| ScenarioName                  | Browser | ExpectedWord |
	| 1. Search using Chrome driver | Chrome  | automation   |
	| 2. Search using IE drive      | IE      | automation   |
	| 3. Search using Firefox drive | Firefox | automation   |	
	
Scenario Outline: 02 Verify that there is expected domain on search results pages
	Given I have entered searched word into the Google search
	| GivenBrowser | GivenWord  |
	| <Browser>    | automation |	
	When I press search button
	Then results on one of the result pages contain expected domain
	| Domain           | Pages          |
	| <ExpectedDomain> | <PagesToCheck> |		
	Scenarios:
	| ScenarioName                  | Browser | ExpectedDomain        | PagesToCheck |
	| 1. Search using Chrome driver | Chrome  | testautomationday.com | 5            |
	| 2. Search using IE drive      | IE      | testautomationday.com | 5            |
	| 3. Search using Firefox drive | Firefox | testautomationday.com | 5            |
	
