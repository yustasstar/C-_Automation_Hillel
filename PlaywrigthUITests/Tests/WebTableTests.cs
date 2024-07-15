﻿using Atata;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using PlaywrigthUITests.PageObjects;
using System.Buffers;
using System.Drawing;
using System.Xml.Linq;

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

        [Test, Retry(2), Description("Verifing search by {searchValue}")]
        public async Task VerifySearch()
        {
            var searchValue = "alden@example.com";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.FillSearchValue(searchValue);
            await _WebTablesPage.VerifyFirstRowContent(searchValue);
        }

        [Test, Retry(2), Description("Verifing cell value {cellValue} is present under the header {headerName}")]
        public async Task VerifyTableRow()
        {
            string headerName = "Email";
            string cellValue = "alden@example.com";

            await _WebTablesPage.GoToURL(testPageUrl);
            await _WebTablesPage.IsTableVisible();
            await _WebTablesPage.IsTableRowVisible();
            await _WebTablesPage.VerifyTableContent(headerName, cellValue);
        }

        [Test, Retry(2), Description("Add new row and verify is it in the table")]
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
            await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();
            await Assertions.Expect(page.Locator(".modal-content")).ToBeVisibleAsync();
            await page.GetByPlaceholder("First Name").FillAsync(firstName);
            await page.GetByPlaceholder("Last Name").FillAsync(lastName);
            await page.GetByPlaceholder("name@example.com").FillAsync(email);
            await page.GetByPlaceholder("Age").FillAsync(age);
            await page.GetByPlaceholder("Salary").FillAsync(salary);
            await page.GetByPlaceholder("Department").FillAsync(department);
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await _WebTablesPage.FillSearchValue(email);
            await _WebTablesPage.VerifyFirstRowContent(lastName);
        }

        [Test, Retry(2), Description("Verify highlighted required fields")]
        public async Task VerifyRequiredFields()
        {
            //testData:
            string firstName = "TestName123";
            string lastName = "LastName 321";
            string email = "test123@email.com";
            string age = "99";
            string salary = "7890";
            string department = "testDep";

            string cssOption = "border-color";
            string passColor = "rgb(40, 167, 69)";
            string failColor = "rgb(220, 53, 69)";
            //-------------------------------
            await _WebTablesPage.GoToURL(testPageUrl);
            await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();
            await Assertions.Expect(page.GetByText("Registration Form×CloseFirst")).ToBeVisibleAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            
            await Assertions.Expect(page.GetByPlaceholder("First Name")).ToHaveCSSAsync(cssOption, failColor);
            await Assertions.Expect(page.GetByPlaceholder("Last Name")).ToHaveCSSAsync(cssOption, failColor);
            await Assertions.Expect(page.GetByPlaceholder("Age")).ToHaveCSSAsync(cssOption, failColor);
            await Assertions.Expect(page.GetByPlaceholder("Salary")).ToHaveCSSAsync(cssOption, failColor);
            await Assertions.Expect(page.GetByPlaceholder("Department")).ToHaveCSSAsync(cssOption, failColor);

            await page.GetByPlaceholder("First Name").FillAsync(firstName);
            await page.GetByPlaceholder("Last Name").FillAsync(lastName);
            await page.GetByPlaceholder("name@example.com").FillAsync(email);
            await page.GetByPlaceholder("Age").FillAsync(age);
            await page.GetByPlaceholder("Salary").FillAsync(salary);
            await page.GetByPlaceholder("Department").FillAsync(department);

            await Assertions.Expect(page.GetByPlaceholder("First Name")).ToHaveCSSAsync(cssOption, passColor);
            await Assertions.Expect(page.GetByPlaceholder("Last Name")).ToHaveCSSAsync(cssOption, passColor);
            await Assertions.Expect(page.GetByPlaceholder("Age")).ToHaveCSSAsync(cssOption, passColor);
            await Assertions.Expect(page.GetByPlaceholder("Salary")).ToHaveCSSAsync(cssOption, passColor);
            await Assertions.Expect(page.GetByPlaceholder("Department")).ToHaveCSSAsync(cssOption, passColor);
        }

        [Test, Retry(2), Description("Verify highlighted required fields")]
        public async Task VerifyEditRow()
        {

        }
        //public void VerifyDeleteRow()
    }
}
