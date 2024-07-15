using Microsoft.Playwright;


internal class WebTablesPage(IPage page)
{
    private readonly IPage page = page;
    private readonly string testPageUrl = "https://demoqa.com/webtables";
    //locators:
    private readonly string tableLocator = ".ReactTable";
    private readonly string rowLocator = ".rt-tr-group";
    private readonly string headerLocator = ".rt-th";
    private readonly string cellLocator = ".rt-td";
    private readonly string addPopUpLocator = ".modal-content";

    public async Task GoToTestPageURL() => await page.GotoAsync(testPageUrl);

    public async Task IsPageH1Visible(string pageH1)
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = pageH1 })).ToBeVisibleAsync();
    }

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

    public async Task FillSearchValue(string searchPlaceholder, string searchValue)
    {
        var searchInput = page.GetByPlaceholder(searchPlaceholder);

        await searchInput.ClickAsync();
        await searchInput.FillAsync(searchValue);
        await searchInput.FocusAsync();
        //await searchInput.PressAsync("Enter");
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

    public async Task ButtonCLick(string buttonName)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = buttonName }).ClickAsync();
    }

    public async Task VerifyAddPopupOpened()
    {
        await Assertions.Expect(page.Locator(addPopUpLocator)).ToBeVisibleAsync();
    }

    public async Task InputFill(string placeholder, string fillText)
    {
        await page.GetByPlaceholder(placeholder).FillAsync(fillText);
    }

    public async Task VerifyInputCssOption(string placeholder, string cssOption, string cssValue)
    {
        await Assertions.Expect(page.GetByPlaceholder(placeholder)).ToHaveCSSAsync(cssOption, cssValue);
    }

    public async Task ClickEdit(string editButtonID)
    {
        await page.Locator(editButtonID).GetByRole(AriaRole.Img).ClickAsync();
    }

    public async Task ClickDelete(string deleteButtonID)
    {
        await page.Locator(deleteButtonID).GetByRole(AriaRole.Img).ClickAsync();
    }
}
