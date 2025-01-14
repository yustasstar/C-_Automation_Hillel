﻿using PlaywrigthUITests.PageObjects;

namespace PlaywrigthUITests.Tests
{
    //[Category("TextBoxTests")]
    internal class TextBoxTests : UITestFixture
    {
        private TextBoxPage _TextBoxPage;

        #region TEST DATA:
        //Page:
        public string testPageUrl = "https://demoqa.com/text-box";
        private string testPageH1 = "Text Box";
        //Labels:
        private string fullNamePlaceholder = "Full Name";
        private string fullNameLabel = "Full Name";
        //Inputs:
        private string fullName = "Test Name 123";
        //private string Email = "TestEmail@test.net";
        //private string CurrentAddress = "Test Current Adress 123";
        //private string PermanentAddress = "Test Permanent Address 123";
        private string submitBtn = "Submit";
        #endregion

        [SetUp]
        public void SetupTextBoxPage()
        {
            _TextBoxPage = new TextBoxPage(page);
        }

        [Test]
        [Retry(2)]
        [Description("H1 'Text Box' should be visible")]
        public async Task VerifyTestPageH1()
        {
            await _TextBoxPage.GoToURL(testPageUrl);
            await _TextBoxPage.IsPageH1Visible(testPageH1);
        }

        [Test]
        [Retry(2)]
        [Description("Label 'Full Name' should be visible")]
        public async Task VerifyFullNameLabel()
        {
            await _TextBoxPage.GoToURL(testPageUrl);
            await _TextBoxPage.IsInputLabelVisible(fullNameLabel);
        }

        [Test]
        [Retry(2)]
        [Description("Enter {fullName} in 'Full Name' input, press 'Submit' btn, text should be 'Name:{fullName}'")]
        public async Task VerifyFullNameInput()
        {
            await _TextBoxPage.GoToURL(testPageUrl);
            await _TextBoxPage.FillInput(fullName, fullNamePlaceholder);
            await _TextBoxPage.IsInputFocused(fullNamePlaceholder);
            await _TextBoxPage.ClickButton(submitBtn);
            await _TextBoxPage.IsButtonFocused(submitBtn);
            await _TextBoxPage.VerifyOutputValue(fullName);
        }

        [Test]
        [Retry(2)]
        [Description("Clear 'Full Name' input, click Submit, output should be cleared")]
        public async Task VerifyFullNameOutput()
        {
            await _TextBoxPage.GoToURL(testPageUrl);
            await _TextBoxPage.FillInput(fullName, fullNamePlaceholder);
            await _TextBoxPage.IsInputFocused(fullNamePlaceholder);
            await _TextBoxPage.ClickButton(submitBtn);
            await _TextBoxPage.IsButtonFocused(submitBtn);
            await _TextBoxPage.VerifyOutputValue(fullName);
            await _TextBoxPage.ClearInput(fullNamePlaceholder);
            await _TextBoxPage.ClickButton(submitBtn);
            await _TextBoxPage.VerifyOutputCleared();
        }
    }
}
