using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrigthUITests.Tests
{
    internal class WebTableTests : UITestFixture
    {
        private WebTablesPage _WebTablesPage;

        [SetUp]
        public void SetupDemoQAPage() => _WebTablesPage = new WebTablesPage(page);

        [Test, Retry(2)]
        [Description("'Web Tables' H1 and table should be visible")]
        public async Task VerifyWebTablePage()
        {
            string pageH1Value = "Web Tables";

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.IsPageH1Visible(pageH1Value);
            await _WebTablesPage.IsTableVisible();
        }

        [Test, Retry(2), Description("Verifing header value {headerName} is present in the table}")]
        public async Task VerifyTableHeaders()
        {
            string headerName = "Action";

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.VerifyTableHeadersContent(headerName);
        }

        [Test, Retry(2), Description("Verifing search by {searchValue}")]
        public async Task VerifySearch()
        {
            var searchValue = "alden@example.com";

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.FillSearchValue(searchValue);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(searchValue);
        }

        [Test, Retry(2), Description("Verifing cell value {cellValue} is present under the header {headerName}")]
        public async Task VerifyTableRow()
        {
            string headerName = "Email";
            string cellValue = "alden@example.com";

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.IsTableRowVisible();
            await _WebTablesPage.VerifyTableContent(headerName, cellValue);
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
            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.AddButtonClick();
            await _WebTablesPage.VerifyAddPopupOpened();
            await _WebTablesPage.FillFirstName(firstName);
            await _WebTablesPage.FillLastName(lastName);
            await _WebTablesPage.FillEmail(email);
            await _WebTablesPage.FillAge(age);
            await _WebTablesPage.FillSalary(salary);
            await _WebTablesPage.FillDepartment(department);
            await _WebTablesPage.SubmitButtonCLick();
            await _WebTablesPage.FillSearchValue(email);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(lastName);
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

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.AddButtonClick();
            await _WebTablesPage.VerifyAddPopupOpened();
            await _WebTablesPage.SubmitButtonCLick();

            await _WebTablesPage.VerifyFirstNameCssOption(cssOption, failColor);
            await _WebTablesPage.VerifyLastNameCssOption(cssOption, failColor);
            await _WebTablesPage.VerifyEmailCssOption(cssOption, failColor);
            await _WebTablesPage.VerifyAgeCssOption(cssOption, failColor);
            await _WebTablesPage.VerifySalaryCssOption(cssOption, failColor);
            await _WebTablesPage.VerifyDepartmentCssOption(cssOption, failColor);

            await _WebTablesPage.FillFirstName(firstName);
            await _WebTablesPage.FillLastName(lastName);
            await _WebTablesPage.FillEmail(email);
            await _WebTablesPage.FillAge(age);
            await _WebTablesPage.FillSalary(salary);
            await _WebTablesPage.FillDepartment(department);

            await _WebTablesPage.VerifyFirstNameCssOption(cssOption, passColor);
            await _WebTablesPage.VerifyLastNameCssOption(cssOption, passColor);
            await _WebTablesPage.VerifyEmailCssOption(cssOption, passColor);
            await _WebTablesPage.VerifyAgeCssOption(cssOption, passColor);
            await _WebTablesPage.VerifySalaryCssOption(cssOption, passColor);
            await _WebTablesPage.VerifyDepartmentCssOption(cssOption, passColor);

        }

        [Test, Retry(2), Description("Verify row editing")]
        public async Task VerifyEditRow()
        {
            string newEmail = "newMail@email.com";
            string newAge = "37";
            string searchValue = "newM";

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.ClickEdit();
            await _WebTablesPage.VerifyAddPopupOpened();
            await _WebTablesPage.FillEmail(newEmail);
            await _WebTablesPage.FillAge(newAge);
            await _WebTablesPage.SubmitButtonCLick();
            await _WebTablesPage.FillSearchValue(searchValue);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(newEmail);
            await _WebTablesPage.VerifyFirstRowContentIsPresent(newAge);
        }

        [Test, Retry(2), Description("Verify row is Deleted from the table")]
        public async Task VerifyDeleteRow()
        {
            string searchValue = "alden@example.com";

            await _WebTablesPage.GoToTestPageURL();
            await _WebTablesPage.ClickDelete();
            await _WebTablesPage.FillSearchValue(searchValue);
            await _WebTablesPage.VerifyFirstRowContentIsNotPresent(searchValue);
        }
    }
}
