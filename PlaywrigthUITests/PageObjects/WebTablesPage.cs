using Microsoft.Playwright;

internal class WebTablesPage(IPage page)
{
    #region TEST DATA:
    //page:
    private readonly IPage page = page;
    private readonly string testPageUrl = "https://demoqa.com/webtables";
    //locators:
    private readonly string tableLocator = ".ReactTable";
    private readonly string rowLocator = ".rt-tr-group";
    private readonly string headerLocator = ".rt-th";
    private readonly string cellLocator = ".rt-td";
    private readonly string addPopUpLocator = ".modal-content";
    //input placeholders:
    private readonly string searchPlaceholder = "Type to search";
    private readonly string firstNamePlaceholder = "First Name";
    private readonly string lastNamePlaceholder = "Last Name";
    private readonly string emailPlaceholder = "name@example.com";
    private readonly string agePlaceholder = "Age";
    private readonly string salaryPlaceholder = "Salary";
    private readonly string departmentPlaceholder = "Department";
    #endregion

    public async Task GoToTestPageURL() => await page.GotoAsync(testPageUrl);
    public async Task IsPageH1Visible(string pageH1)
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = pageH1 })).ToBeVisibleAsync();
    }

    #region Table:
    public async Task IsTableVisible()
    {
        await Assertions.Expect(page.Locator(tableLocator)).ToBeVisibleAsync();
    }
    public async Task IsTableRowVisible()
    {
        await Assertions.Expect(page.Locator(rowLocator).First).ToBeVisibleAsync();
    }
    public async Task VerifyTableHeadersContent(string headerName)
    {
        var headers = await page.Locator(headerLocator).AllInnerTextsAsync();
        var headerList = headers.ToList();
        Assert.That(headerList, Does.Contain(headerName), $"The header '{headerName}' was not found in the table headers.");
    }
    public async Task VerifyFirstRowContentIsPresent(string contentValue)
    {
        var row = page.Locator(rowLocator).First;
        var cells = await row.Locator(cellLocator).AllInnerTextsAsync();
        var cellList = cells.ToList();
        Assert.That(cellList, Does.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
    }
    public async Task VerifyFirstRowContentIsNotPresent(string contentValue)
    {
        var row = page.Locator(rowLocator).First;
        var cells = await row.Locator(cellLocator).AllInnerTextsAsync();
        var cellList = cells.ToList();
        Assert.That(cellList, Does.Not.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
    }
    public async Task VerifyTableContent(string headerName, string cellValue)
    {
        var table = page.Locator(tableLocator);
        var headers = await table.Locator(headerLocator).AllInnerTextsAsync();
        var headerList = headers.ToList();
        int headerIndex = headerList.IndexOf(headerName);

        if (headerIndex == -1)
        {
            throw new Exception($"Header '{headerName}' not found.");
        }

        var rows = table.Locator(rowLocator);
        var rowCount = await rows.CountAsync();

        bool isCellContentPresent = false;
        for (int i = 0; i < rowCount; i++)
        {
            var row = rows.Nth(i);
            var cells = await row.Locator(cellLocator).AllInnerTextsAsync();
            var cellList = cells.ToList();

            if (headerIndex < cellList.Count && cellList[headerIndex] == cellValue)
            {
                isCellContentPresent = true;
                break;
            }
        }
        Assert.That(isCellContentPresent, Is.True, $"The cell value '{cellValue}' is not present under the header '{headerName}'.");
    }
    #endregion

    #region Inputs:
    public async Task FillSearchValue(string searchValue)
    {
        var searchInput = page.GetByPlaceholder(searchPlaceholder);

        await searchInput.ClickAsync();
        await searchInput.FillAsync(searchValue);
        await searchInput.FocusAsync();
        //await searchInput.PressAsync("Enter");
    }
    public async Task FillFirstName(string fillText)
    {
        await page.GetByPlaceholder(firstNamePlaceholder).FillAsync(fillText);
    }
    public async Task FillLastName(string fillText)
    {
        await page.GetByPlaceholder(lastNamePlaceholder).FillAsync(fillText);
    }
    public async Task FillEmail(string fillText)
    {
        await page.GetByPlaceholder(emailPlaceholder).FillAsync(fillText);
    }
    public async Task FillAge(string fillText)
    {
        await page.GetByPlaceholder(agePlaceholder).FillAsync(fillText);
    }
    public async Task FillSalary(string fillText)
    {
        await page.GetByPlaceholder(salaryPlaceholder).FillAsync(fillText);
    }
    public async Task FillDepartment(string fillText)
    {
        await page.GetByPlaceholder(departmentPlaceholder).FillAsync(fillText);
    }
    #endregion

    #region Buttons:
    public async Task AddButtonClick()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();
    }
    public async Task SubmitButtonCLick()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
    }
    public async Task ClickEdit()
    {
        await page.Locator("#edit-record-2").GetByRole(AriaRole.Img).ClickAsync();
    }
    public async Task ClickDelete()
    {
        await page.Locator("#delete-record-2").GetByRole(AriaRole.Img).ClickAsync();
    }
    #endregion

    #region AddPopupVerifications:
    public async Task VerifyAddPopupOpened()
    {
        await Assertions.Expect(page.Locator(addPopUpLocator)).ToBeVisibleAsync();
    }
    public async Task VerifyFirstNameCssOption(string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(firstNamePlaceholder)).ToHaveCSSAsync(cssOption, cssValue);
    }
    public async Task VerifyLastNameCssOption(string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(lastNamePlaceholder)).ToHaveCSSAsync(cssOption, cssValue);
    }
    public async Task VerifyEmailCssOption(string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(emailPlaceholder)).ToHaveCSSAsync(cssOption, cssValue);
    }
    public async Task VerifyAgeCssOption(string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(agePlaceholder)).ToHaveCSSAsync(cssOption, cssValue);
    }
    public async Task VerifySalaryCssOption(string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(salaryPlaceholder)).ToHaveCSSAsync(cssOption, cssValue);
    }
    public async Task VerifyDepartmentCssOption(string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(departmentPlaceholder)).ToHaveCSSAsync(cssOption, cssValue);
    }
    #endregion
}
