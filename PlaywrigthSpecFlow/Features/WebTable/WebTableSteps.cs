using Microsoft.Playwright;
using TechTalk.SpecFlow;
using PlaywrightSpecFlow.PageObjects;
using PlaywrigthSpecFlow.Bindings;

namespace PlaywrigthSpecFlow.Features.WebTable
{
    [Binding]
    internal sealed class WebTableSteps : UITestFixture
    {
        public static DemoQAWebTablesPage _WebTablesPage;

        [BeforeFeature("@WebPageLogin")]
        public static async Task FirstBeforeScenario()
        {
            _WebTablesPage = new DemoQAWebTablesPage(page);
        }

        [Given(@"I am on WebTable Page")]
        public async Task WhenIOpenWebTablePage() => await _WebTablesPage.GoToTestPageURL();

        [When(@"I see the WebTable")]
        public async Task WhenISeeTheWebTable() => await _WebTablesPage.IsTableVisible();

        [When(@"I see the Headers")]
        public async Task WhenISeeTheHeaders()
        {
            List<string> headersList = new List<string>
            { "First Name", "Last Name", "Age", "Email", "Salary", "Department", "Action" };

            foreach (var headerName in headersList)
            {
                await _WebTablesPage.GoToTestPageURL();
                await _WebTablesPage.IsTableVisible();
                await _WebTablesPage.VerifyTableHeadersContent(headerName);
            }
        }

        [When(@"I type ""([^""]*)"" in the Search")]
        public async Task WhenITypeEmailInTheSearch(string email)
        {
            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.FillSearchValue(email);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(email);
        }

        [Then(@"I see ""([^""]*)"" in the table")]
        public async Task ThenISeeDataInTheRow(string firstName)
        {
            string headerName = "First Name";
            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.IsTableRowVisible();
            await _WebTablesPage.VerifyTableContent(headerName, firstName);
        }



        [When(@"I click Add Button")]
        public async Task WhenIKlickAddButton() => await _WebTablesPage.AddButtonClick();

        [When(@"I set FirstName to ""(.*)""")]
        public async Task WhenISetFirstName(string firstName) => await _WebTablesPage.FillFirstName(firstName);

        [When(@"I set LastName to ""(.*)""")]
        public async Task WhenISetLastName(string lastName) => await _WebTablesPage.FillLastName(lastName);

        [Then(@"I see FirstName ""(.*)"" in a table")]
        public async Task ThenISeeFirstName(string firstName) => await Assertions.Expect(page.GetByRole(AriaRole.Gridcell, new() { Name = firstName, Exact = true })).ToBeVisibleAsync();

        [When(@"I set Email ""(.*)"" in a table")]
        public async Task ThenISetEmail(string email)
        {
            await page.GetByPlaceholder("name@example.com").FillAsync(email);
            await page.GetByPlaceholder("name@example.com").PressAsync("Enter");
        }

        [Then(@"I see LastName ""(.*)"" in a table")]
        public async Task ThenISeeLastName(string lastName) => await Assertions.Expect(page.GetByRole(AriaRole.Gridcell, new() { Name = lastName, Exact = true })).ToBeVisibleAsync();
    }
}