using Atata;
using Microsoft.Playwright;
using PlaywrigthUITests.PageObjects;

namespace PlaywrigthUITests.Tests
{
    //[Category("WebTables")]
    internal class WebTableTests : UITestFixture
    {
        private WebTablesPage _WebTablesPage;

        #region TEST DATA:
        //Page:
        private readonly string testPageUrl = "https://demoqa.com/webtables";
        #endregion

        [SetUp]
        public void SetupDemoQAPage() => _WebTablesPage = new WebTablesPage(page);

        [Test, Retry(2)]
        [Description("H1 'Web Tables' should be visible")]
        public async Task VerifyWebTablePage()
        {
            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsPageH1Visible("Web Tables");
            await _WebTablesPage.IsTableVisible();
        }

        [Test, Retry(2), Description("Verifing cell value {cellValue} is present under the header {headerName}")]
        public async Task VerifyTableRow()
        {
            string headerName = "Email";
            string cellValue = "alden@example.com";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.IsTableRowVisible();
            await _WebTablesPage.VerifyTableCellContent(headerName, cellValue);
        }

        //public void VerifyTableHeaders()
        //public void VerifySearch()
        //public void VerifyTableRow()
        //public void VerifyAddNewRow()
        //public void VerifyAddPopupRequiredFields()
        //public void VerifyEditRow()
        //public void VerifyDeleteRow()

    }
}
