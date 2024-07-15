using Microsoft.Playwright;

namespace PlaywrigthUITests.Tests
{
    //[Category("WebTables")]
    internal class WebTableTests : UITestFixture
    {
        private WebTablesPage _WebTablesPage;

        [SetUp]
        public void SetupDemoQAPage() => _WebTablesPage = new WebTablesPage(page);

        #region TEST DATA:
        private readonly string testPageUrl = "https://demoqa.com/webtables";

        //input placeholders:
        private readonly string searchPlaceholder = "Type to search";

        private readonly string firstNamePlaceholder = "First Name";
        private readonly string lastNamePlaceholder = "Last Name";
        private readonly string emailPlaceholder = "name@example.com";
        private readonly string agePlaceholder = "Age";
        private readonly string salaryPlaceholder = "Salary";
        private readonly string departmentPlaceholder = "Department";
        //locators:
        private readonly string tableLocator = ".ReactTable";
        private readonly string addPopUpLocator = ".modal-content";
        private readonly string rowLocator = ".rt-tr-group";
        
        #endregion

        [Test, Retry(2)]
        [Description("'Web Tables' H1 and table should be visible")]
        public async Task VerifyWebTablePage()
        {
            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsPageH1Visible("Web Tables");
            await _WebTablesPage.IsTableVisible(tableLocator);
        }

        [Test, Retry(2), Description("Verifing header value {headerName} is present in the table}")]
        public async Task VerifyTableHeaders()
        {
            string headerName = "Action";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsTableVisible(tableLocator);
            await _WebTablesPage.VerifyTableHeadersContent(headerName);
        }

        [Test, Retry(2), Description("Verifing search by {searchValue}")]
        public async Task VerifySearch()
        {
            var searchValue = "alden@example.com";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.FillSearchValue(searchPlaceholder, searchValue);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(rowLocator, searchValue);
        }

        [Test, Retry(2), Description("Verifing cell value {cellValue} is present under the header {headerName}")]
        public async Task VerifyTableRow()
        {
            string headerName = "Email";
            string cellValue = "alden@example.com";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsTableVisible(tableLocator);
            await _WebTablesPage.IsTableRowVisible(rowLocator);
            await _WebTablesPage.VerifyTableContent(tableLocator, rowLocator, headerName, cellValue);
        }

        [Test, Retry(2), Description("Add new row and verify is it present in the table")]
        public async Task VerifyAddNewRow()
        {
            //testData:
            string firstName = "TestName123";
            string lastName = "LastName 321";
            string email = "test123@email.com";
            string age = "99";
            string salary = "7890";
            string department = "testDep";
            //-------------------------------
            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.ButtonCLick("Add");
            await _WebTablesPage.VerifyAddPopupOpened(addPopUpLocator);
            await _WebTablesPage.InputFill(firstNamePlaceholder, firstName);
            await _WebTablesPage.InputFill(lastNamePlaceholder, lastName);
            await _WebTablesPage.InputFill(emailPlaceholder, email);
            await _WebTablesPage.InputFill(agePlaceholder, age);
            await _WebTablesPage.InputFill(salaryPlaceholder, salary);
            await _WebTablesPage.InputFill(departmentPlaceholder, department);
            await _WebTablesPage.ButtonCLick("Submit");
            await _WebTablesPage.FillSearchValue(searchPlaceholder, email);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(rowLocator, lastName);
        }

        [Test, Retry(2), Description("Verify highlighted required fields")]
        public async Task VerifyRequiredFields()
        {
            #region TestDATA:
            string firstName = "TestName123";
            string lastName = "LastName 321";
            string email = "test123@email.com";
            string age = "99";
            string salary = "7890";
            string department = "testDep";

            string cssOption = "border-color";
            string passColor = "rgb(40, 167, 69)";
            string failColor = "rgb(220, 53, 69)";
            #endregion

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.ButtonCLick("Add");
            await _WebTablesPage.VerifyAddPopupOpened(addPopUpLocator);
            await _WebTablesPage.ButtonCLick("Submit");

            await _WebTablesPage.VerifyInputCssOption(firstNamePlaceholder, cssOption, failColor);
            await _WebTablesPage.VerifyInputCssOption(lastNamePlaceholder, cssOption, failColor);
            await _WebTablesPage.VerifyInputCssOption(emailPlaceholder, cssOption, failColor);
            await _WebTablesPage.VerifyInputCssOption(agePlaceholder, cssOption, failColor);
            await _WebTablesPage.VerifyInputCssOption(salaryPlaceholder, cssOption, failColor);
            await _WebTablesPage.VerifyInputCssOption(departmentPlaceholder, cssOption, failColor);

            await _WebTablesPage.InputFill(firstNamePlaceholder, firstName);
            await _WebTablesPage.InputFill(lastNamePlaceholder, lastName);
            await _WebTablesPage.InputFill(emailPlaceholder, email);
            await _WebTablesPage.InputFill(agePlaceholder, age);
            await _WebTablesPage.InputFill(salaryPlaceholder, salary);
            await _WebTablesPage.InputFill(departmentPlaceholder, department);

            await _WebTablesPage.VerifyInputCssOption(firstNamePlaceholder, cssOption, passColor);
            await _WebTablesPage.VerifyInputCssOption(lastNamePlaceholder, cssOption, passColor);
            await _WebTablesPage.VerifyInputCssOption(emailPlaceholder, cssOption, passColor);
            await _WebTablesPage.VerifyInputCssOption(agePlaceholder, cssOption, passColor);
            await _WebTablesPage.VerifyInputCssOption(salaryPlaceholder, cssOption, passColor);
            await _WebTablesPage.VerifyInputCssOption(departmentPlaceholder, cssOption, passColor);
        }

        [Test, Retry(2), Description("Verify row editing")]
        public async Task VerifyEditRow()
        {
            string newEmail = "newMail@email.com";
            string newAge = "37";
            string searchValue = "newM";
            string editButtonID = "#edit-record-2";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.ClickEdit(editButtonID);
            await _WebTablesPage.VerifyAddPopupOpened(addPopUpLocator);
            await _WebTablesPage.InputFill(emailPlaceholder, newEmail);
            await _WebTablesPage.InputFill(agePlaceholder, newAge);
            await _WebTablesPage.ButtonCLick("Submit");
            await _WebTablesPage.FillSearchValue(searchPlaceholder, searchValue);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(rowLocator, newEmail);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(rowLocator, newAge);
        }

        [Test, Retry(2), Description("Verify row is Deleted from the table")]
        public async Task VerifyDeleteRow()
        {
            string searchValue = "alden@example.com";
            string editButtonID = "#delete-record-2";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.ClickEdit(editButtonID);
            await _WebTablesPage.FillSearchValue(searchPlaceholder, searchValue);
            await _WebTablesPage.VerifyFirstRowContentIsNotPresent(rowLocator, searchValue);
        }
    }
}
