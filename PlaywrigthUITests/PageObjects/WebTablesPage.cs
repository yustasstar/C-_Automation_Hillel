using Microsoft.Playwright;


internal class WebTablesPage(IPage page)
{
    private readonly IPage page = page;

    public async Task GoToURL(string testPageUrl) => await page.GotoAsync(testPageUrl);

    public async Task IsPageH1Visible(string pageH1)
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = pageH1 })).ToBeVisibleAsync();
    }

    public async Task IsTableVisible(string tableLocator)
    {
        await Assertions.Expect(page.Locator(tableLocator)).ToBeVisibleAsync();
    }

    public async Task IsTableRowVisible(string rowLocator)
    {
        await Assertions.Expect(page.Locator(rowLocator).First).ToBeVisibleAsync();
    }

    public async Task VerifyTableHeadersContent(string headerName)
    {
        var headers = await page.Locator(".rt-th").AllInnerTextsAsync();
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

    public async Task VerifyFirstRowContentIsPresent(string rowLocator, string contentValue)
    {
        var row = page.Locator(rowLocator).First;
        var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
        var cellList = cells.ToList();
        Assert.That(cellList, Does.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
    }

    public async Task VerifyFirstRowContentIsNotPresent(string rowLocator, string contentValue)
    {
        var row = page.Locator(rowLocator).First;
        var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
        var cellList = cells.ToList();
        Assert.That(cellList, Does.Not.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
    }

    public async Task VerifyTableContent(string tableLocator, string rowLocator, string headerName, string cellValue)
    {
        var table = page.Locator(tableLocator);
        var headers = await table.Locator(".rt-th").AllInnerTextsAsync();
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
            var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
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

    public async Task VerifyAddPopupOpened(string popupLocator)
    {
        await Assertions.Expect(page.Locator(popupLocator)).ToBeVisibleAsync();
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
