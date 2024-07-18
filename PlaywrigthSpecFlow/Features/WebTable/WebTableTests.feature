@ReusesFeatureDriver
@WebPagePresetup
@WebPageLogin
@RefreshPage

Feature: WebTableTest

As a User i want to add new item to web table, 
so that i can see the new item in the table
and i can delete and edit item.

Scenario Outline: I see item in the table
	Given I am on WebTable Page
	When I see the WebTable
	And I see the Headers
	And I type "<Email>" in the Search
	Then I see "<FirstName>" in the table

Examples:
	| FirstName | LastName | Age | Email              | Salary | Department |
	| Cierra    | Vega     | 39  | cierra@example.com | 10000  | Insurance  |
	| Alden     | Cantrell | 45  | alden@example.com  | 12000  | Compliance |
	| Kierra    | Gentry   | 29  | kierra@example.com | 2000   | Legal      |


Scenario Outline: I add item to the table
	Given I am on WebTable Page
	When I see the WebTable
	And I click Add Button
	And I see Registration Form
	And I set FirstName to "<FirstName>"
	And I set LastName to "<LastName>"
	And I set Email to "<Email>"
	And I set Age to "<Age>"
	And I set Salary to "<Salary>"
	And I set Department to "<Department>"
	And I click Submit Button
	And I see the WebTable
	And I type "<Email>" in the Search
	Then I see data in the row "<FirstName>" , "<LastName>", "<Email>", "<Age>", "<Salary>", "<Department>"
Examples:
	| FirstName | LastName   | Email                        | Age | Salary | Department  |
	| Ruby      | Frank      | ruby.frank@example.com       | 34  | 11000  | Marketing   |
	| John      | Doe        | john.doe@example.com         | 28  | 9000   | Sales       |
	| Mary      | Smith      | mary.smith@example.com       | 47  | 13000  | HR          |
	| David     | Johnson    | david.johnson@example.com    | 30  | 10500  | IT          |
	| Sara      | Brown      | sara.brown@example.com       | 37  | 15000  | Finance     |
	| James     | Wilson     | james.wilson@example.com     | 40  | 11500  | Operations  |
	| Linda     | Lee        | linda.lee@example.com        | 42  | 12500  | Development |
	| Michael   | Clark      | michael.clark@example.com    | 35  | 10000  | QA          |
	| Laura     | Lewis      | laura.lewis@example.com      | 32  | 9000   | Design      |
	| Daniel    | Hall       | daniel.hall@example.com      | 38  | 12000  | Support     |
	| Olivia    | King       | olivia.king@example.com      | 29  | 8000   | Legal       |
	| William   | Adams      | william.adams@example.com    | 43  | 13000  | Insurance   |
	| Emma      | Scott      | emma.scott@example.com       | 36  | 9500   | Compliance  |
	| Robert    | Harris     | robert.harris@example.com    | 41  | 11000  | Marketing   |
	| Emily     | Young      | emily.young@example.com      | 33  | 8500   | Sales       |
	| Richard   | Nelson     | richard.nelson@example.com   | 44  | 12500  | HR          |
	| Amanda    | Baker      | amanda.baker@example.com     | 31  | 10500  | IT          |
	| Charles   | Carter     | charles.carter@example.com   | 46  | 15000  | Finance     |
	| Jessica   | Mitchell   | jessica.mitchell@example.com | 28  | 8500   | Operations  |
	| Matthew   | Perez      | matthew.perez@example.com    | 39  | 9000   | Development |
	| Jennifer  | Roberts    | jennifer.roberts@example.com | 34  | 11000  | QA          |
	| Joshua    | Turner     | joshua.turner@example.com    | 45  | 12000  | Design      |
	| Sarah     | Phillips   | sarah.phillips@example.com   | 30  | 10000  | Support     |
	| Joseph    | Campbell   | joseph.campbell@example.com  | 37  | 13000  | Legal       |
	| Ashley    | Parker     | ashley.parker@example.com    | 42  | 9500   | Insurance   |
	| Mark      | Evans      | mark.evans@example.com       | 31  | 8000   | Compliance  |
	| Megan     | Edwards    | megan.edwards@example.com    | 35  | 11500  | Marketing   |
	| Andrew    | Collins    | andrew.collins@example.com   | 43  | 12500  | Sales       |
	| Samantha  | Stewart    | samantha.stewart@example.com | 32  | 8500   | HR          |
	| Benjamin  | Sanchez    | benjamin.sanchez@example.com | 40  | 10500  | IT          |
	| Hannah    | Morris     | hannah.morris@example.com    | 28  | 9000   | Finance     |
	| Lucas     | Rogers     | lucas.rogers@example.com     | 38  | 9500   | Operations  |
	| Sophia    | Reed       | sophia.reed@example.com      | 34  | 12000  | Development |
	| Nathan    | Cook       | nathan.cook@example.com      | 29  | 8000   | QA          |
	| Mia       | Morgan     | mia.morgan@example.com       | 46  | 11000  | Design      |
	| Ethan     | Bell       | ethan.bell@example.com       | 36  | 10000  | Support     |
	| Isabella  | Murphy     | isabella.murphy@example.com  | 33  | 13000  | Legal       |
	| Logan     | Bailey     | logan.bailey@example.com     | 41  | 10500  | Insurance   |
	| Abigail   | Rivera     | abigail.rivera@example.com   | 39  | 9000   | Compliance  |
	| Ryan      | Cooper     | ryan.cooper@example.com      | 32  | 8500   | Marketing   |
	| Grace     | Richardson | grace.richardson@example.com | 28  | 11500  | Sales       |
	| Tyler     | Cox        | tyler.cox@example.com        | 37  | 10000  | HR          |
	| Zoey      | Howard     | zoey.howard@example.com      | 40  | 12000  | IT          |
	| Dylan     | Ward       | dylan.ward@example.com       | 29  | 9500   | Finance     |
	| Avery     | Torres     | avery.torres@example.com     | 34  | 13000  | Operations  |
	| Jack      | Peterson   | jack.peterson@example.com    | 44  | 10500  | Development |
	| Lily      | Gray       | lily.gray@example.com        | 31  | 8500   | QA          |
