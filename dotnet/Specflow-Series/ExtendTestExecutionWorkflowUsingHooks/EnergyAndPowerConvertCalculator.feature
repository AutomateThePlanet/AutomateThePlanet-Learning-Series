@firefox
Feature: Convert Metrics for Nuclear Science
	To do my nuclear-related job
	As a Nuclear Engineer 
	I want to be able to convert different metrics.

@hooksExample @firefox
Scenario: Successfully Convert Kilowatt-hours to Newton-meters
	
	When I navigate to Metric Conversions
	And navigate to Energy and power section
	And navigate to Kilowatt-hours
	And choose conversions to Newton-meters
	And type 30 kWh
	Then assert that 1.080000e+8 Nm are displayed as answer