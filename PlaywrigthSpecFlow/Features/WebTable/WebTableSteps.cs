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
            _WebTablesPage = new DemoQAWebTablesPage(Page);
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
                await _WebTablesPage.IsTableVisible();
                await _WebTablesPage.VerifyTableHeadersContent(headerName);
            }
        }

        [When(@"I type ""([^""]*)"" in the Search")]
        public async Task WhenITypeEmailInTheSearch(string email)
        {
            await _WebTablesPage.FillSearchValue(email);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(email);
        }

        [Then(@"I see ""([^""]*)"" in the table")]
        public async Task ThenISeeDataInTheRow(string firstName)
        {
            string headerName = "First Name";
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.IsTableRowVisible();
            await _WebTablesPage.VerifyTableContent(headerName, firstName);
        }

        [When(@"I click Add Button")]
        public async Task WhenIClickAddButton() => await _WebTablesPage.AddButtonClick();

        [When(@"I see Registration Form")]
        public async Task WhenISeeRegistrationForm() => await _WebTablesPage.VerifyAddPopupOpened();

        [When(@"I set FirstName to ""([^""]*)""")]
        public async Task WhenISetFirstNameTo(string firstName) => await _WebTablesPage.FillFirstName(firstName);

        [When(@"I set LastName to ""([^""]*)""")]
        public async Task WhenISetLastNameTo(string lastName) => await _WebTablesPage.FillLastName(lastName);

        [When(@"I set Email to ""([^""]*)""")]
        public async Task WhenISetEmailTo(string email) => await _WebTablesPage.FillEmail(email);

        [When(@"I set Age to ""([^""]*)""")]
        public async void WhenISetAgeTo(string age) => await _WebTablesPage.FillAge(age);

        [When(@"I set Salary to ""([^""]*)""")]
        public async Task WhenISetSalaryTo(string salary)
        {
            await Task.Delay(100); // Wait for 0.1 seconds
            await _WebTablesPage.FillSalary(salary);
        }

        [When(@"I set Department to ""([^""]*)""")]
        public async Task WhenISetDepartmentTo(string department) => await _WebTablesPage.FillDepartment(department);

        [When(@"I click Submit Button")]
        public async Task WhenIClickSubmitButton() => await _WebTablesPage.SubmitButtonCLick();

        [Then(@"I see data in the row ""([^""]*)"" , ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", ""([^""]*)""")]
        public async Task ThenISeeDataInTheRow(string firstName, string lastName, string age, string email, string salary, string department)
        {
            List<string> checkList = new List<string> { firstName, lastName, age, email, salary, department };

            var row = Page.Locator(".rt-tr-group").First;
            var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
            var cellList = cells.ToList();

            foreach (var checkValue in checkList)
            {
                Assert.That(cellList, Does.Contain(checkValue), $"The search value '{checkValue}' was not found in the table.");
            }
        }
    }
}