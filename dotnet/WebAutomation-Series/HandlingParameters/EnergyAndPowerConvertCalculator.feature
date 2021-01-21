@firefox
Feature: Convert Metrics for Nuclear Science
	To do my nuclear-related job
	As a Nuclear Engineer 
	I want to be able to convert different metrics.

Scenario: Successfully Convert Kilowatt-hours to Newton-meters
	When I navigate to Metric Conversions
	And navigate to Energy and power section
	And navigate to Kilowatt-hours
	And choose conversions to Newton-meters
	And type "30" kWh
	Then assert that 1.080000e+8 Nm are displayed as answer

Scenario: Successfully Convert Kilowatt-hours to Newton-meters in Fractions format
	When I navigate to Metric Conversions
	And navigate to Energy and power section
	And navigate to Kilowatt-hours
	And choose conversions to Newton-meters
	And type 30 kWh in Fractions format
	Then assert that 1079999999⁄64 Nm are displayed as answer

Scenario: Successfully Convert Seconds to Minutes
	When I navigate to Seconds to Minutes Page
	And type seconds for 1 day, 1 hour, 1 minute, 1 second
	Then assert that 1501 minutes are displayed as answer

Scenario: Successfully Convert Seconds to Minutes No Minutes
	When I navigate to Seconds to Minutes Page
	And type seconds for 1 day, 1 hour, 1 second
	Then assert that 1500 minutes are displayed as answer

Scenario Outline: Successfully Convert Seconds to Minutes Table
	When I navigate to Seconds to Minutes Page
	And type seconds for <seconds>
	Then assert that <minutes> minutes are displayed as answer
Examples:
| seconds						| minutes   | 
| 1 day, 1 hour, 1 second       | 1500		| 
| 5 days, 3 minutes 			| 7203		| 
| 4 hours						| 240		| 
| 180 seconds     				| 3			| 


Scenario: Add Amazon Products with Affiliate Codes
	When add products
	| Url                                      | AffilicateCode |
	| /dp/B00TSUGXKE/ref=ods_gw_d_h1_tab_fd_c3 | affiliate3     |
	| /dp/B00KC6I06S/ref=fs_ods_fs_tab_al      | affiliate4     |
	| /dp/B0189XYY0Q/ref=fs_ods_fs_tab_ts      | affiliate5     |
	| /dp/B018Y22C2Y/ref=fs_ods_fs_tab_fk      | affiliate6     |