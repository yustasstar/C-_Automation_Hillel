using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Atata;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V122.Tracing;
using TechTalk.SpecFlow;

internal class WebTablesPage(IPage page)
{
    private readonly IPage page = page;

    public async Task GoToURL(string testPageUrl) => await page.GotoAsync(testPageUrl);

    public async Task IsPageH1Visible(string pageH1)
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = pageH1 })).ToBeVisibleAsync();
    }

    public async Task IsTableVisible()
    {
        await Assertions.Expect(page.Locator(".ReactTable")).ToBeVisibleAsync();
    }

    public async Task IsTableRowVisible()
    {
        await Assertions.Expect(page.Locator(".rt-tr-group").First).ToBeVisibleAsync();
    }

    public async Task VerifyTableHeadersContent(string headerName)
    {
        var headers = await page.Locator(".rt-th").AllInnerTextsAsync();
        var headerList = headers.ToList();
        Assert.That(headerList, Does.Contain(headerName), $"The header '{headerName}' was not found in the table headers.");
    }

    public async Task FillSearchValue(string searchValue)
    {
        var searchInput = page.GetByPlaceholder("Type to search");

        await searchInput.ClickAsync();
        await searchInput.FillAsync(searchValue);
        await searchInput.FocusAsync();
        //await searchInput.PressAsync("Enter");
    }

    public async Task VerifyFirstRowContent(string contentValue)
    {
        var row = page.Locator(".rt-tr-group").First;
        var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
        var cellList = cells.ToList();
        Assert.That(cellList, Does.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
    }

    public async Task VerifyTableContent(string headerName, string cellValue)
    {
        var table = page.Locator(".ReactTable");
        var headers = await table.Locator(".rt-th").AllInnerTextsAsync();
        var headerList = headers.ToList();
        int headerIndex = headerList.IndexOf(headerName);

        if (headerIndex == -1)
        {
            throw new Exception($"Header '{headerName}' not found.");
        }

        var rows = table.Locator(".rt-tr-group");
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
}
