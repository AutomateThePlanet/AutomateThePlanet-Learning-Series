Feature: Create Purchase in Amazon
	In order to receive a book online
	As a client
	I want to be able to choose it through the browser and pay for it online

@testingFramework
Scenario: Create Successfull Purchase When Billing Country Is United States with American Express Card
	When I navigate to "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743"
	And I click the 'buy now' button
	And then I click the 'proceed to checkout' button
	#When the login page loads
	And I login with email = "g3984159@trbvm.com" and pass = "ASDFG_12345"
	#When the shipping address page loads
	And I type full name = "John Smith", country = "United States", Adress = "950 Avenue of the Americas", city = "New Your City", state = "New Your", zip = "10001-2121" and phone = "00164644885569"
	And I choose to fill different billing, full name = "John Smith", country = "United States", Adress = "950 Avenue of the Americas", city = "New Your City", state = "New Your", zip = "10001-2121" and phone = "00164644885569"
	And click shipping address page 'continue' button
	And click shipping payment top 'continue' button
	Then assert that order total price = "40.49"