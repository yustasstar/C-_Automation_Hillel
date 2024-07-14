﻿using Atata;
using Microsoft.Playwright;
using PlaywrigthUITests.PageObjects;
using System.Buffers;

namespace PlaywrigthUITests.Tests
{
    //[Category("WebTables")]
    internal class WebTableTests : UITestFixture
    {
        private WebTablesPage _WebTablesPage;

        #region TEST DATA:
        private readonly string testPageUrl = "https://demoqa.com/webtables";
        #endregion

        [SetUp]
        public void SetupDemoQAPage() => _WebTablesPage = new WebTablesPage(page);

        [Test, Retry(2)]
        [Description("'Web Tables' H1 and table should be visible")]
        public async Task VerifyWebTablePage()
        {
            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsPageH1Visible("Web Tables");
            await _WebTablesPage.IsTableVisible();
        }

        [Test, Retry(2), Description("Verifing header value {headerName} is present in the table}")]
        public async Task VerifyTableHeaders()
        {
            string headerName = "Action";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.VerifyTableHeadersContent(headerName);
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

        [Test, Retry(2), Description("Verifing cell value {cellValue} is present under the header {headerName}")]
        public async Task VerifySearch()
        {
            var searchValue = "alden@example.com";
            var searchInput = page.GetByPlaceholder("Type to search");

            await _WebTablesPage.GoToURL(testPageUrl);
            await searchInput.ClickAsync();
            await searchInput.FillAsync(searchValue);
            await searchInput.PressAsync("Enter");
            await _WebTablesPage.VerifyFirstRowCellContent(searchValue);
        }

        //public void VerifyAddNewRow()
        //public void VerifyAddPopupRequiredFields()
        //public void VerifyEditRow()
        //public void VerifyDeleteRow()

    }
}
