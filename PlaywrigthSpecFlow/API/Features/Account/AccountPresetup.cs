﻿using Gherkin;
using PlaywrigthSpecFlow.API.Models;

namespace PlaywrigthSpecFlow.API.Features.Account
{
    internal class AccountPresetup
    {
        internal static string UserName = "Usr" + GetCurrentTimestamp();
        internal static string Password = "Pa$$word1";
        internal string? UserId;
        internal string? Token;

        internal UserModel MainUser = new UserModel()
        {
            userName = UserName,
            password = Password
        };

        #region Setup
        internal async Task AccountApiPresetup()
        {
            AccountsApi account = new AccountsApi("https://demoqa.com/");
            UserId = await account.AddUserGetId(MainUser);
            Token = await account.GenerateToken(MainUser);
            await account.GetUserById(UserId, Token);
        }

        internal async Task AccountApiCleanup()
        {
            AccountsApi account = new AccountsApi("https://demoqa.com/");
            await account.DeleteAccountByID(UserId, Token);
        }
        #endregion

        #region HelperMethods
        internal static string GetCurrentTimestamp()
        {
            DateTime currentTimestamp = DateTime.UtcNow;

            string timestamp = currentTimestamp.ToString("u");

            timestamp = Regex.Replace(timestamp, @"\D", "");

            return timestamp;
        }
        #endregion
    }
}