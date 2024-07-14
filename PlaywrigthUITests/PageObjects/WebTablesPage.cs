using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V122.Tracing;

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

    public async Task VerifyTableCellContent(string headerName, string cellValue)
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

    #region PopUp
    //public async Task VerifyPopupVisible()
    //{
    //    var popup = page.Locator(".modal-content");
    //    await Assertions.Expect(popup).ToBeVisibleAsync();
    //}

    //public async Task VerifyFirstNameVisible()
    //{
    //    var popup = page.Locator(".modal-content");
    //    var firstName = popup.GetByPlaceholder("First Name");
    //    await Assertions.Expect(firstName).ToBeVisibleAsync();
    //}
    #endregion

}
